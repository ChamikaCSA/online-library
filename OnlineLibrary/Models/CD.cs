using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineLibrary.Models
{
    public class CD
    {
        [Key]
        public int CDID { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [StringLength(255)]
        public string Artist { get; set; }

        [StringLength(100)]
        public string Genre { get; set; }
    }
}
