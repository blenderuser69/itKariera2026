namespace ShopMVC.Models
{
    public class OrderItem
    {
        //id of the item
        public int Id { get; set; }
        //id of the order the item is in
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;
        //id for the item that is ordered
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public int Quantity { get; set; }
        // the price of the product when it was ordered, the initial price can be changed
        public decimal UnitPrice { get; set; }
    }
}