using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public class UserCreateDto
    {
        [Required]
        [MaxLength(50)]

        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public bool IsActive { get; set; }
    }
}