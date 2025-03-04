using System.ComponentModel.DataAnnotations;

namespace casestudy2.Models.Product
{
    public class SubCategory
    {
        [Required]
        [Range(1, 8, ErrorMessage = "SubCategoryId shouble be between 1-8")]
        public int SubCategoryId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string? SubCategoryName { get; set; }

        [Required]
        [Range(1, 3, ErrorMessage = "CategoryId shouble be between 1-3")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

    }
}
