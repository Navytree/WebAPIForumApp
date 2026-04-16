using System.ComponentModel.DataAnnotations;

namespace WebAPIForumApp.Models
{

    public class User
    {
        public int Id { get; set; }

        [StringLength(20, MinimumLength = 5)]
        [Required]
        public string Login { get; set; }

        [StringLength(20, MinimumLength = 5)]
        [Required]
        public string PasswordHash { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public virtual ICollection<Topic> Topics { get; set; } = new List<Topic>();
        public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}
