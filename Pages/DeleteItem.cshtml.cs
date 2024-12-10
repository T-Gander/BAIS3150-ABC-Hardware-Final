using BAIS3150_ABC_Hardware_Final.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BAIS3150_ABC_Hardware_Final.Pages
{
    public class DeleteItemModel : PageModel
    {
        public List<Item> Items { get; set; }

        public string ConfirmationMessage { get; set; }

        [BindProperty]
        public string ItemNumber { get; set; }

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
                ABCPOS ABCHardware = new();
                success = ABCHardware.RemoveItem(ItemNumber);

                if (success)
                {
                    HttpContext.Session.SetString("ConfirmationMessage", "Item was deleted successfully.");
                    page = RedirectToPage("/DeleteItem");
                }
                else
                {
                    ConfirmationMessage = "An error occurred, Item was not deleted successfully.";
                    page = Page();
                }
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("ConfirmationMessage", ex.Message);
                page = RedirectToPage("/DeleteItem");
            }

            return page;
        }
    }
}
