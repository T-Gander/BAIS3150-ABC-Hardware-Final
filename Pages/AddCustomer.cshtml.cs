using BAIS3150_ABC_Hardware_Final.Domain;
using BAIS3150_ABC_Hardware_Final.TechnicalServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BAIS3150_ABC_Hardware_Final.Pages
{
    public class AddCustomerModel : PageModel
    {
        [BindProperty]
        public string FirstName { get; set; }

        [BindProperty]
        public string LastName { get; set; }

        [BindProperty]
        public string Address { get; set; }

        [BindProperty]
        public string City { get; set; }

        [BindProperty]
        public string Province { get; set; }

        [BindProperty]
        public string PostalCode { get; set; }

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

            Customer customer = new()
            {
                FirstName = FirstName,
                LastName = LastName,
                Address = Address,
                City = City,
                Province = Province,
                PostalCode = PostalCode
            };

            ABCPOS ABCHardware = new();
            success = ABCHardware.CreateCustomer(customer);

            IActionResult page;

            if (success) 
            {
                HttpContext.Session.SetString("ConfirmationMessage", "New customer was added successfully.");
                page = RedirectToPage("/AddCustomer");
            }
            else
            {
               ConfirmationMessage = "An error occurred, customer was not added successfully.";
               page = Page();
            }

            return page;
        }
    }
}
