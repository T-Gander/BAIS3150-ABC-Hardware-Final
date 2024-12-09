using Azure;
using BAIS3150_ABC_Hardware_Final.Domain;
using BAIS3150_ABC_Hardware_Final.TechnicalServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace BAIS3150_ABC_Hardware_Final.Pages
{
    public class ProcessSaleModel : PageModel
    {
        public List<Salesperson> Salespeople { get; set; }
        public List<Item> Items { get; set; }
        public List<Customer> Customers { get; set; }
        public string SaleNumber { get; set; }

        [BindProperty]
        public string SaleItems { get; set; }

        [BindProperty]
        public string ItemNumber { get; set; }

        [BindProperty]
        public string SalespersonID { get; set; }

        [BindProperty]
        public string CustomerID { get; set; }

        [BindProperty]
        public string Quantity { get; set; }

        [BindProperty]
        public string SubTotal { get; set; }

        [BindProperty]
        public string GST { get; set; }

        [BindProperty]
        public string SaleTotal { get; set; }

        public string ConfirmationMessage { get; set; }

        public void OnGet()
        {
            string? confirmationMessage = HttpContext.Session.GetString("ConfirmationMessage");

            if (confirmationMessage != null)
            {
                ConfirmationMessage = confirmationMessage;
                HttpContext.Session.Remove("ConfirmationMessage");
            }

            ABCPOS ABCHardware = new ABCPOS();
            Items = ABCHardware.GetItems();
            Salespeople = ABCHardware.GetSalespeople();
            Customers = ABCHardware.GetCustomers();
        }

        public IActionResult OnPost()
        {
            IActionResult page;

            try
            {
                ABCPOS ABCHardware = new ABCPOS();

                List<SaleItem> saleItems = new();
                string unescapedJson = SaleItems.Replace("\\\"", "\"");
                saleItems = JsonSerializer.Deserialize<List<SaleItem>>(unescapedJson);

                // Likely unnecessary, I should calculate these as I go and pass to Razor.
                
                decimal subtotal = 0;

                foreach (SaleItem saleItem in saleItems) 
                {
                    subtotal += saleItem.ItemTotal;
                }

                decimal GST = subtotal * 0.05m;
                decimal saleTotal = subtotal + GST;

                //

                Sale ABCSale = new()
                {
                    SaleDate = DateTime.Now,
                    SalespersonID = int.Parse(SalespersonID),
                    CustomerID = int.Parse(CustomerID),
                    Subtotal = subtotal,
                    GST = GST,
                    SaleTotal = saleTotal
                };

                int saleNumber = ABCHardware.CreateSale(ABCSale);

                foreach (SaleItem item in saleItems)
                {
                    item.SaleNumber = saleNumber;
                    ABCHardware.CreateSaleItem(item);

                    Item updateItem = ABCHardware.GetItem(item.ItemNumber);
                    updateItem.QuantityOnHand -= item.Quantity;

                    ABCHardware.UpdateItem(updateItem);
                }

                HttpContext.Session.SetString("ConfirmationMessage", "Sale was processed successfully.");
                page = RedirectToPage("/ProcessSale");
            }
            catch (Exception ex)
            {
                ConfirmationMessage = "Sale was processed unsuccessfully.";
                page = Page();
            }
            return page;
        }
    }
}
