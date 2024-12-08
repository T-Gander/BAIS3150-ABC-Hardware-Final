namespace BAIS3150_ABC_Hardware_Final.Domain
{
    public class Item
    {
        public string ItemNumber { get; set; }

        public string Description { get; set; }

        public decimal UnitPrice { get; set; }

        public bool Deleted { get; set; }

        public int QuantityOnHand { get; set; }
    }
}
