using System.ComponentModel.DataAnnotations;

namespace WebAPIForumApp.DTOs
{
    public class LoginDTO
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
