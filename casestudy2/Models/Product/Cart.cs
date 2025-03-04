using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace casestudy2.Models.Product
{
    public class Cart
    {
        public int CartId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string UserId { get; set; }
        public IdentityUser? User { get; set; }

        [Required]
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Cart item quantity should not be 0")]
        public int Quantity { get; set; }
    }
}
