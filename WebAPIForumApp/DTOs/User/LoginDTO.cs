using System.ComponentModel.DataAnnotations;

namespace WebAPIForumApp.DTOs.User
{
    public class LoginDTO
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
