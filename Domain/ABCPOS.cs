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

        public List<Customer> GetCustomers()
        {
            Customers CustomerManager = new();
            return CustomerManager.GetCustomers();
        }

        public bool RemoveCustomer(int customerID)
        {
            Customers CustomerManager = new();
            return CustomerManager.DeleteCustomer(customerID);
        }

        public List<Item> GetItems() 
        {
            Items ItemManager = new();
            return ItemManager.GetItems();
        }

        public bool RemoveItem(string ItemNumber)
        {
            Items ItemManager = new();
            return ItemManager.DeleteItem(ItemNumber);
        }

        public bool UpdateCustomer(Customer customer)
        {
            Customers CustomerManager = new();
            return CustomerManager.UpdateCustomer(customer);
        }

        public bool UpdateItem(Item item)
        {
            Items ItemManager = new();
            return ItemManager.UpdateItem(item);
        }
    }

    
}
