namespace ShopMVC.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string PriceFormatted => $"{Price:F2} $";
        public int Stock { get; set; }
        public string StockFormatted => $"{Stock} pcs";
        public string Category { get; set; } = string.Empty;
    }
}