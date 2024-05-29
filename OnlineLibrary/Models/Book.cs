using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineLibrary.Models
{
    public class Book
    {
        [Key]
        public int BookID { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [StringLength(255)]
        public string Author { get; set; }

        [StringLength(13)]
        public string ISBN { get; set; }

        [StringLength(100)]
        public string Genre { get; set; }

        [StringLength(10)]
        public string Status { get; set; }

        public static List<string> StatusOptions = new List<string> { "Available", "Reserved", "Issued" };
    }
}
