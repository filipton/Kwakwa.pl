using System.ComponentModel.DataAnnotations;

namespace KwakwaPl.Data
{
    public class CreateGroupModel
    {
        [Required]
        [StringLength(16, ErrorMessage = "Name too long. (Max 16 characters.)")]
        public string? GroupName { get; set; }

        [StringLength(16, ErrorMessage = "Password too long. (Max 16 characters.)")]
        public string? Password { get; set; }
        public bool PasswordSecured { get; set; }
        public bool OneTimePassword { get; set; }
    }
}