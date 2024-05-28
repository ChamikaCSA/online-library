using System.ComponentModel.DataAnnotations;

namespace OnlineLibrary.Models
{
    public class Guest
    {
        [Key]
        public int GuestID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }
    }
}
