using BAIS3150_ABC_Hardware_Final.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;

namespace BAIS3150_ABC_Hardware_Final.Pages
{
    public class AddItemModel : PageModel
    {
        [BindProperty]
        public string ItemNumber { get; set; }

        [BindProperty]
        public string Description { get; set; }

        [BindProperty]
        public string UnitPrice { get; set; }

        [BindProperty]
        public string QuantityOnHand { get; set; }

        public string ConfirmationMessage { get; set; }

        public void OnGet()
        {
            string? confirmationMessage = HttpContext.Session.GetString("ConfirmationMessage");

            if (confirmationMessage != null)
            {
                ConfirmationMessage = confirmationMessage;
                HttpContext.Session.Remove("ConfirmationMessage");
            }
        }

        public IActionResult OnPost()
        {
            bool success = false;

            IActionResult page;

            try
            {
                if (!decimal.TryParse(UnitPrice, out decimal unitPrice) || !int.TryParse(QuantityOnHand, out int quantityOnHand))
                {
                    throw new Exception("Parsing of UnitPrice or QuantityOnHand failed.");
                }

                Item item = new()
                {
                    ItemNumber = ItemNumber,
                    Description = Description,
                    UnitPrice = unitPrice,
                    QuantityOnHand = quantityOnHand
                };

                ABCPOS ABCHardware = new();
                success = ABCHardware.CreateItem(item);

                if (success)
                {
                    HttpContext.Session.SetString("ConfirmationMessage", "New item was added successfully.");
                    page = RedirectToPage("/AddItem");
                }
                else
                {
                    ConfirmationMessage = "An error occurred, item was not added successfully.";
                    page = Page();
                }
            }
            catch (Exception ex) 
            {
                ConfirmationMessage = ex.Message;
                page = Page();
            }

            return page;
        }
    }
}
