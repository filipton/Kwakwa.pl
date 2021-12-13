using System.ComponentModel.DataAnnotations;

namespace KwakwaPl.Models
{
    public class PasswordModel
    {
        [Required]
        public string? Password { get; set; }
    }
}