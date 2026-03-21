namespace ShopMVC.Models
{
    public class OrderItem
    {
        //id na itema
        public int Id { get; set; }
        //id na poruchkata
        public int OrderId { get; set; }
        //poruchkata kum koqto prinadleji
        public Order Order { get; set; } = null!;
        //id na samiq artikul
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}