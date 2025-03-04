using System.ComponentModel.DataAnnotations;

namespace casestudy2.Models.Product
{
    public class Product
    {
        [Required(AllowEmptyStrings = false)]
        public int ProductId { get; set; }


        [Required(AllowEmptyStrings = false)]
        public string? ProductName { get; set; }


        [Required(AllowEmptyStrings = false)]
        [StringLength(200, ErrorMessage = "Description cannot be longer than 200 word")]
        public string? ProductDesc { get; set; }

        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Product price must be greater than 0")]
        public double ProductPrice { get; set; }


        [Required]
        public string? ProductImageUrl { get; set; }


        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Stock cannot be Negative")]
        public int ProductStock { get; set; }

        [Required]
        [Range(1, 8, ErrorMessage = "SubCategoryId shouble be between 1-8")]
        public int SubCategoryID { get; set; }
        public SubCategory? SubCategory { get; set; }

    }
}
