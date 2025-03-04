using casestudy2.Data;
using casestudy2.Models.Product;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace casestudy2.Repositories
{
    public class CartRespositories : ICartRespositories
    {
        private ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public CartRespositories(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IEnumerable<Cart> GetAllCartitem(string userId)
        {

            return _context.Cart.Include(c => c.Product).Where(c => c.UserId == userId).ToList();
        }
        public void AddtoCart(Cart cart)
        {
            _context.Cart.Add(cart);
            _context.SaveChanges();
        }

        public void ClearCart()
        {
            _context.Cart.RemoveRange();
            _context.SaveChanges();
        }

        public void RemoveFromCart(int cartId)
        {
            var cart = _context.Cart.Find(cartId);
            _context.Cart.Remove(cart);
            _context.SaveChanges();
        }
        public void DeleteCartItem(int cartItemId)
        {
            var cartItem = _context.Cart.Find(cartItemId);
            if (cartItem != null)
            {
                _context.Cart.Remove(cartItem);
                _context.SaveChanges();
            }
        }
        public void UpdateCartItemQuantity(int cartItemId, int quantity)
        {
            var cartItem = _context.Cart.Find(cartItemId);
            if (cartItem != null)
            {
                cartItem.Quantity = quantity;
                _context.SaveChanges();
            }
        }

    }
}
