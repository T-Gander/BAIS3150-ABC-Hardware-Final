using BAIS3150_ABC_Hardware_Final.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BAIS3150_ABC_Hardware_Final.Pages
{
    public class UpdateCustomerModel : PageModel
    {
        public List<Customer> Customers { get; set; }

        public string ConfirmationMessage { get; set; }

        [BindProperty]
        public string CustomerID { get; set; }

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
                Customer customer = new()
                {
                    CustomerID = int.Parse(CustomerID),
                    FirstName = FirstName,
                    LastName = LastName,
                    Address = Address,
                    City = City,
                    Province = Province,
                    PostalCode = PostalCode
                };

                ABCPOS ABCHardware = new();
                success = ABCHardware.UpdateCustomer(customer);

                if (success)
                {
                    HttpContext.Session.SetString("ConfirmationMessage", "Customer was updated successfully.");
                    page = RedirectToPage("/UpdateCustomer");
                }
                else
                {
                    ConfirmationMessage = "An error occurred, Customer was not updated successfully.";
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
