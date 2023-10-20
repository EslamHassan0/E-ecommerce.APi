namespace Assignment.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string ProductCode { get; set; }
        public string Image { get; set; }
        public decimal  Price{ get; set; }
        public int MinimumQuantity { get; set; }
        public int DiscountRate { get; set; }

    }
}
