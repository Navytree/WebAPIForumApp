using System.ComponentModel.DataAnnotations;

namespace WebAPIForumApp.DTOs.User
{
    public class RegisterDTO
    {
        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string Login { get; set; } = null!;

        [Required]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Password length must be between 5 and 20 characters")]
        public string Password { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
