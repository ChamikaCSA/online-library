using System.ComponentModel.DataAnnotations;

namespace OnlineLibrary.Models
{
	public class Admin
	{
		[Key]
		public int AdminID { get; set; }

		[Required]
		[StringLength(50)]
		public string Username { get; set; }

		[Required]
		[StringLength(50)]
		public string Password { get; set; }
	}
}
