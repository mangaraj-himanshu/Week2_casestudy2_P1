using Microsoft.AspNetCore.Identity;

namespace casestudy2.Models.Product
{
    public class Order
    {
        public int OrderId { get; set; }
        public string UserId { get; set; }
        public IdentityUser? User { get; set; }
        public DateTime OrderDate { get; set; }
        public double TotalAmount { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }

    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
