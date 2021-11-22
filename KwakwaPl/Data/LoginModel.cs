using System.ComponentModel.DataAnnotations;

namespace KwakwaPl.Data
{
	public class LoginModel
	{
		[Required]
		[StringLength(16, ErrorMessage = "Name too long. (Max 16 characters.)")]
		public string? UserName { get; set; }
	}
}
