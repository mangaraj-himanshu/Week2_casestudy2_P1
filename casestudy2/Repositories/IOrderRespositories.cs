using casestudy2.Models.Product;

namespace casestudy2.Repositories
{
    public interface IOrderRespositories
    {
        public void CreateOrder(Order order);
        public Order GetOrderById(int orderId);
        public List<Order> GetOrdersByUserId(string userId);
        public void UpdateOrder(Order order);
        public void DeleteOrder(int orderId);
    }
}
