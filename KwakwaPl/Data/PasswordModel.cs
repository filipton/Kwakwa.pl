using System.ComponentModel.DataAnnotations;

namespace KwakwaPl.Data
{
    public class PasswordModel
    {
        [Required]
        public string? Password { get; set; }
    }
}