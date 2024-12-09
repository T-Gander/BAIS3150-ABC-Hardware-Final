using BAIS3150_ABC_Hardware_Final.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BAIS3150_ABC_Hardware_Final.Pages
{
    public class UpdateItemModel : PageModel
    {
        public List<Item> Items { get; set; }

        public string ConfirmationMessage { get; set; }

        [BindProperty]
        public string ItemNumber { get; set; }

        [BindProperty]
        public string Description { get; set; }

        [BindProperty]
        public string UnitPrice { get; set; }

        [BindProperty]
        public string Deleted { get; set; }

        [BindProperty]
        public string QuantityOnHand { get; set; }

        public void OnGet()
        {
            string? confirmationMessage = HttpContext.Session.GetString("ConfirmationMessage");

            if (confirmationMessage != null)
            {
                ConfirmationMessage = confirmationMessage;
                HttpContext.Session.Remove("ConfirmationMessage");
            }

            ABCPOS ABCHardware = new();
            Items = ABCHardware.GetItems();
        }

        public IActionResult OnPost()
        {
            bool success = false;

            IActionResult page;

            try
            {
                Item item = new()
                {
                    ItemNumber = ItemNumber,
                    Description = Description,
                    Deleted = bool.Parse(Deleted),
                    UnitPrice = decimal.Parse(UnitPrice),
                    QuantityOnHand = int.Parse(QuantityOnHand)
                };

                ABCPOS ABCHardware = new();
                success = ABCHardware.UpdateItem(item);

                if (success)
                {
                    HttpContext.Session.SetString("ConfirmationMessage", "Item was updated successfully.");
                    page = RedirectToPage("/UpdateItem");
                }
                else
                {
                    ConfirmationMessage = "An error occurred, Item was not updated successfully.";
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
