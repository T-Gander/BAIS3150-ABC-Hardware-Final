using BAIS3150_ABC_Hardware_Final.TechnicalServices;

namespace BAIS3150_ABC_Hardware_Final.Domain
{
    public class ABCPOS
    {
        public bool CreateCustomer(Customer customer)
        {
            Customers CustomerManager = new();
            return CustomerManager.AddCustomer(customer);
        }

        public bool CreateItem(Item item)
        {
            Items ItemManager = new();
            return ItemManager.AddItem(item);
        }
    }

    
}
