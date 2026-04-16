using Microsoft.EntityFrameworkCore;
using WebAPIForumApp.Models;

namespace WebAPIForumApp.Data
{
    public class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new WebAPIForumAppContext(
                serviceProvider.GetRequiredService<DbContextOptions<WebAPIForumAppContext>>()))

            {
                if (!context.Users.Any())
                {
                    var hasher = serviceProvider.GetRequiredService<IPasswordHasher>();
                    context.Users.AddRange(
                    new User
                    {
                        Login = "TestUser1",
                        PasswordHash = hasher.Hash("Password123")
                    },
                    new User
                    {
                        Login = "Qwerty",
                        PasswordHash = hasher.Hash("qwerty")
                    },
                    new User
                    {
                        Login = "Abcde",
                        PasswordHash = hasher.Hash("qwerty")
                    });
                    await context.SaveChangesAsync();
                }
                var testUser1 = await context.Users.FirstAsync(u => u.Login == "TestUser1");
                var testUser2 = await context.Users.FirstAsync(u => u.Login == "Qwerty");
                var testUser3 = await context.Users.FirstAsync(u => u.Login == "Abcde");

                if (!context.Topics.Any()) 
                {

                    context.Topics.AddRange(
                        new Topic
                        {
                            Title = "Welcome to the Forum!",
                            Description = "Have a good time with other users!",
                            CreatedAt = DateTime.Now,
                            UserId = testUser1.Id
                        },
                        new Topic
                        {
                            Title = "Lorem Ipsum",
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                            "Etiam dictum mauris ac fringilla iaculis. Vivamus dapibus ipsum eu iaculis tincidunt. " +
                            "Nullam volutpat nisi ut mollis blandit. Maecenas feugiat eget augue nec dictum. " +
                            "Ut ac arcu eu purus tincidunt pellentesque sit amet vel mi. Vivamus quis mauris diam. " +
                            "Nunc nulla felis, vestibulum sed quam et, laoreet gravida odio. " +
                            "Etiam euismod nulla vel vestibulum facilisis. Sed rhoncus augue ut dui maximus lacinia. " +
                            "Phasellus condimentum venenatis nibh sed auctor. Ut faucibus eros semper nunc venenatis elementum. " +
                            "Vestibulum vel condimentum ex, at blandit velit. " +
                            "Cras augue sapien, ullamcorper nec mi ac, scelerisque ornare mauris.",
                            CreatedAt = DateTime.Now,
                            UserId = testUser1.Id
                        },
                        new Topic
                        {
                            Title = "Lorem Ipsum",
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                            "Etiam dictum mauris ac fringilla iaculis. Vivamus dapibus ipsum eu iaculis tincidunt. " +
                            "Nullam volutpat nisi ut mollis blandit. Maecenas feugiat eget augue nec dictum. " +
                            "Ut ac arcu eu purus tincidunt pellentesque sit amet vel mi. Vivamus quis mauris diam. ",
                            CreatedAt = DateTime.Now,
                            UserId = testUser1.Id
                        },
                        new Topic
                        {
                            Title = "Hello word",
                            Description = "Nice song by Louie Zong! I also like his Ghost Choir series, how about you guys?",
                            CreatedAt = DateTime.Now,
                            UserId = testUser2.Id
                        });
                    await context.SaveChangesAsync();
                }
                var LoremIpsumTopic = await context.Topics.FirstAsync(t => t.Title == "Lorem Ipsum" && t.UserId == testUser1.Id);
                var LikedSongTopic = await context.Topics.FirstAsync(t => t.Title == "Hello world" && t.UserId == testUser2.Id);
                // i'm aware that the user can have many topics with the same topic title,
                // for example "Help! My code doesn't work!!!1!", but in this case i'll use it as my reference point

                if (!context.Posts.Any())
                {

                    context.Posts.AddRange(
                        new Post
                        {
                            Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                            "Etiam dictum mauris ac fringilla iaculis. Vivamus dapibus ipsum eu iaculis tincidunt. ",
                            CreatedAt = DateTime.Now,
                            UserId = testUser1.Id,
                            TopicId = LoremIpsumTopic.Id
                        },
                        new Post
                        {
                            Content = "Lorem ipsum dolor",
                            CreatedAt = DateTime.Now,
                            UserId = testUser2.Id,
                            TopicId = LoremIpsumTopic.Id
                        },
                        new Post
                        {
                            Content = "sit amet",
                            CreatedAt = DateTime.Now,
                            UserId = testUser3.Id,
                            TopicId = LoremIpsumTopic.Id
                        },
                        new Post
                        {
                            Content = "consectetur adipiscing elit",
                            CreatedAt = DateTime.Now,
                            UserId = testUser1.Id,
                            TopicId = LoremIpsumTopic.Id
                        },
                        new Post
                        {
                            Content = "Etiam dictum mauris ac fringilla iaculis",
                            CreatedAt = DateTime.Now,
                            UserId = testUser1.Id,
                            TopicId = LoremIpsumTopic.Id
                        },

                        new Post
                        {
                            Content = "They're great, i liked his collab with Tom Cardy in Don't Touch My Ladder",
                            CreatedAt = DateTime.Now,
                            UserId = testUser1.Id,
                            TopicId = LikedSongTopic.Id
                        },
                        new Post
                        {
                            Content = "Ye Toms songs always make me smile",
                            CreatedAt = DateTime.Now,
                            UserId = testUser2.Id,
                            TopicId = LikedSongTopic.Id
                        },
                        new Post
                        {
                            Content = "I like them to :D",
                            CreatedAt = DateTime.Now,
                            UserId = testUser3.Id,
                            TopicId = LikedSongTopic.Id
                        }
                        );

                    await context.SaveChangesAsync();
                }


            }


        }


    }
}
