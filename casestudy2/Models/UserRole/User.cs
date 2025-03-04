using System.ComponentModel.DataAnnotations;

namespace casestudy2.Models.useritem
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }
        public string? UserPassword { get; set; }
        public long UserphoneNumber { get; set; }


    }
}
