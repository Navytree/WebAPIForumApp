using Microsoft.EntityFrameworkCore;
using WebAPIForumApp.Data;
using WebAPIForumApp.DTOs.User;
using WebAPIForumApp.Models;
namespace WebAPIForumApp.Tests
{
    public class UsersControllerTests
    {
        private WebAPIForumAppContext GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<WebAPIForumAppContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var databaseContext = new WebAPIForumAppContext(options);
            databaseContext.Database.EnsureCreated();
            return databaseContext;
        }


        [Fact]
        public async Task PostUser_StoresHashedPassword()
        {
            var context = GetDatabaseContext();
            var hasher = new PasswordHasher();
            var controller = new AccountController(context, hasher);
            var dto = new RegisterDTO { Login = "Admin123", Password = "Password123!", Email = "Admin@gmail.com" };
            await controller.Register(dto);

            var savedUser = context.Users.First();
            Assert.NotEqual("Password123!", savedUser.PasswordHash);
            Assert.True(savedUser.PasswordHash.Length > 20);
        }

        [Fact]
        public async Task PostUser_StoresHashedPassword_ThatCanBeVerified()
        {
            var context = GetDatabaseContext();
            var hasher = new PasswordHasher();
            var controller = new AccountController(context, hasher);

            var dto = new RegisterDTO { Login = "Tester", Password = "SuperSecret123!", Email = "Tester@gmail.com" };
            await controller.Register(dto);

            var savedUser = await context.Users.FirstOrDefaultAsync(u => u.Login == "Tester");

            Assert.NotNull(savedUser);
            Assert.NotEqual("SuperSecret123!", savedUser.PasswordHash);

            var isPasswordValid = hasher.Verify("SuperSecret123!", savedUser.PasswordHash);
            Assert.True(isPasswordValid, "Password should be correctly verified");
        }



    }
}