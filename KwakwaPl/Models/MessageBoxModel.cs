﻿using System.ComponentModel.DataAnnotations;

namespace KwakwaPl.Models
{
    public class MessageBoxModel
    {
        [Required]
        [StringLength(5000, ErrorMessage = "Message too long. (Max 5000 characters.)")]
        public string? Message { get; set; }
    }
}
