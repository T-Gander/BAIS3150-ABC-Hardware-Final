namespace BAIS3150_ABC_Hardware_Final.Domain
{
    public class Sale
    {

        public int SaleNumber { get; set; }

        public DateTime SaleDate { get ; set; }

        public int SalespersonID { get; set; }

        public int CustomerID { get; set; }

        public decimal Subtotal { get; set; }

        public decimal GST { get; set; }

        public decimal SaleTotal { get; set; }
    }
}
