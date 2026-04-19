using WebAPIForumApp.Models;

namespace WebAPIForumApp.Tests;
public class PostTest
{
    [Fact]
    public void NewPost_ShouldHaveCurrentDate()
    {
        var post = new Post { CreatedAt = DateTime.Now };

        Assert.Equal(DateTime.Now.Date, post.CreatedAt.Date, TimeSpan.FromSeconds(1));
    }
}
