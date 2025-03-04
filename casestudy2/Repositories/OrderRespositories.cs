using casestudy2.Data;
using casestudy2.Models.Product;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace casestudy2.Repositories
{
    public class OrderRespositories : IOrderRespositories
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public OrderRespositories(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public void CreateOrder(Order order)
        {
            _context.Orders.Add(order);

            try
            {
                FileStream file = new FileStream("C:\\Users\\nikhipal\\source\\repos\\CaseStudy2\\ordersDetails.txt", FileMode.Append, FileAccess.Write);
                string content = $"Order id - {order.OrderId} \n" +
                                 $"Date - {DateTime.Now}\n" +
                                 $"User id -{order.UserId}\n" +
                                 $"TotalAMount - {order.TotalAmount}\n";
                StreamWriter data = new StreamWriter(file);
                data.WriteLine(content);
                data.Close();
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                FileStream file = new FileStream("C:\\Users\\nikhipal\\source\\repos\\CaseStudy2\\ErrorLog.txt", FileMode.Append, FileAccess.Write);
                StreamWriter write = new StreamWriter(file);
                string errorMessage = $"Error:{e.Message}\n" +
                                    $"Date and Time:{DateTime.Now}\n" +
                                    $"Class ,Method,Line:{e.StackTrace}\n" +
                                    $"Source:{e.Source}\n";
                write.WriteLine(errorMessage);
                write.Close();
            }
        }

        public Order GetOrderById(int orderId)
        {
            return _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(o => o.Product)
                .FirstOrDefault(o => o.OrderId == orderId);
        }

        public List<Order> GetOrdersByUserId(string userId)
        {
            return _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.OrderItems)
                .ThenInclude(o => o.Product)
                .ToList();
        }

        public void UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
            _context.SaveChanges();
        }

        public void DeleteOrder(int orderId)
        {
            var order = _context.Orders.Find(orderId);
            if (order != null)
            {
                _context.Orders.Remove(order);
                _context.SaveChanges();
            }
        }
    }
}
