using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPIForumApp.Models
{
    public class Topic
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; } = default!;

        public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
        public int RepliesCount { get; set; }
        public void IncrementReplies()
        {
            RepliesCount++;
        }
    }
}
