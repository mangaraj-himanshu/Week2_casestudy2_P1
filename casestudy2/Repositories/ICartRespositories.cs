using casestudy2.Models.Product;

namespace casestudy2.Repositories
{
    public interface ICartRespositories
    {
        public IEnumerable<Cart> GetAllCartitem(string userId);
        public void AddtoCart(Cart cart);
        public void RemoveFromCart(int cardId);
        public void ClearCart();
        public void DeleteCartItem(int cartItemId);
        public void UpdateCartItemQuantity(int cartItemId, int quantity);
    }
}
