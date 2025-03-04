using System.ComponentModel.DataAnnotations;

namespace casestudy2.Models.Product
{
    public class Category
    {

        [Required]
        [Range(1, 3, ErrorMessage = "CategoryId shouble be between 1-3")]
        public int CategoryId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string? CategoryName { get; set; }

    }
}
