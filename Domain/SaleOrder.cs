namespace BAIS3150_ABC_Hardware_Final.Domain
{
    public class SaleOrder
    {
        public int SalesOrderID { get; set; }

        public int SaleNumber { get; set; }

        public string ItemNumber { get; set; }

        public int Quantity { get; set; }

        public decimal ItemTotal { get; set; }
    }
}
