using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineLibrary.Models
{
    public class Reservation
    {
        [Key]
        public int ReservationID { get; set; }

        public DateTime ReservationDate { get; set; }

        [StringLength(10)]
        public string Status { get; set; }

        [ForeignKey("Guest")]
        public int Guest_GuestID { get; set; }
        [ForeignKey("Book")]
        public int Book_BookID { get; set; }
    }
}