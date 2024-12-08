using BAIS3150_ABC_Hardware_Final.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BAIS3150_ABC_Hardware_Final.Pages
{
    public class DeleteCustomerModel : PageModel
    {
        public List<Customer> Customers { get; set; }

        public string ConfirmationMessage { get; set; }

        [BindProperty]
        public string CustomerID { get; set; }

        public void OnGet()
        {
            string? confirmationMessage = HttpContext.Session.GetString("ConfirmationMessage");

            if (confirmationMessage != null)
            {
                ConfirmationMessage = confirmationMessage;
                HttpContext.Session.Remove("ConfirmationMessage");
            }

            ABCPOS ABCHardware = new();
            Customers = ABCHardware.GetCustomers();
        }

        
        public IActionResult OnPost()
        {
            bool success = false;

            IActionResult page;

            try
            {
                ABCPOS ABCHardware = new();
                success = ABCHardware.RemoveCustomer(int.Parse(CustomerID));

                if (success)
                {
                    HttpContext.Session.SetString("ConfirmationMessage", "Customer was deleted successfully.");
                    page = RedirectToPage("/DeleteCustomer");
                }
                else
                {
                    ConfirmationMessage = "An error occurred, Customer was not deleted successfully.";
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
