using System;
using System.ComponentModel.DataAnnotations;

namespace YouCode.Models.Entity
{
    public class Contact
    {
        [Key]
        public int? ContactId { get; set; }

        [Required(ErrorMessage = "Please enter your name")]
        [MaxLength(50, ErrorMessage = "Name can't be more than 50 characters.")]
        [MinLength(3, ErrorMessage = "Name must be at least 3 characters.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Please provide a valid email.")]
        [EmailAddress]
        public string? Email { get; set; }

        [MaxLength(60, ErrorMessage = "Subject can't be more than 60 characters.")]
        [MinLength(4, ErrorMessage = "Subject must be at least 4 characters.")]
        public string? Subject { get; set; }

        [MaxLength(500)]
        [MinLength(50, ErrorMessage = "Message must be at least 50 characters.")]
        public string? Message { get; set; }

        public DateTime MessageDate { get; set; }
    }
}
