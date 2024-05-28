using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineLibrary.Models
{
    public class Message
    {
        [Key]
        public int MessageID { get; set; }

        [MaxLength]
        public string MessageContent { get; set; }

        public DateTime Date { get; set; }

        [ForeignKey("Guest")]
        public int Guest_GuestID { get; set; }
    }
}
