using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineLibrary.Models
{
    public class Issue
    {
        [Key]
        public int IssueID { get; set; }

        public DateTime IssueDate { get; set; }

        public DateTime ReturnDate { get; set; }

        [StringLength(10)]
        public string Status { get; set; }

        [ForeignKey("Admin")]
        public int Admin_AdminID { get; set; }
        public virtual Admin Admin { get; set; }

        [ForeignKey("Guest")]
        public int Guest_GuestID { get; set; }
        public virtual Guest Guest { get; set; }

        [ForeignKey("Book")]
        public int Book_BookID { get; set; }
        public virtual Book Book { get; set; }
    }
}
