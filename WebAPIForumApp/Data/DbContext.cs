using WebAPIForumApp.Models;
using Microsoft.EntityFrameworkCore;


namespace WebAPIForumApp.Data
{
    public class WebAPIForumAppContext : DbContext
    {
        public WebAPIForumAppContext(DbContextOptions<WebAPIForumAppContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Topic> Topics { get; set; } = null!;
        public DbSet<Post> Posts { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Post>()
                .HasOne(e => e.User)
                .WithMany(e => e.Posts)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Topic>()
                .HasOne(e => e.User)
                .WithMany(e => e.Topics)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Post>()
                .HasOne(p => p.Topic)
                .WithMany(t => t.Posts)
                .HasForeignKey(p => p.TopicId)
                .OnDelete(DeleteBehavior.Cascade);
        }



    }
}
