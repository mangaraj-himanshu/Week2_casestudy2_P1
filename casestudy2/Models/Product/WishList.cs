using casestudy2.Models.useritem;

namespace casestudy2.Models.Product
{
    public class WishList
    {
        public int WishListId { get; set; }
        public virtual Product? ProductID { get; set; }
        public virtual User? UserId { get; set; }

    }
}
