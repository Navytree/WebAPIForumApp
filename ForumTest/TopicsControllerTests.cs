using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIForumApp.Controllers;
using WebAPIForumApp.Data;
using WebAPIForumApp.DTOs;
using WebAPIForumApp.Models;
using Xunit;

namespace WebAPIForumApp.Tests
{
    public class TopicsControllerTests
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
        public async Task GetTopics_ReturnsOkResult()
        {
            var context = GetDatabaseContext();
            var user = new User { Id = 1, Login = "Tester", Email = "a@a.com", PasswordHash = "hash" };
            context.Users.Add(user);
            await context.SaveChangesAsync();
            context.Topics.Add(new Topic { Title = "Test Topic 1", Description = "Test", CreatedAt = DateTime.Now, UserId = 1 });
            await context.SaveChangesAsync();

            var controller = new TopicsController(context);

            var result = await controller.GetTopics();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var topics = Assert.IsAssignableFrom<IEnumerable<TopicDTO>>(okResult.Value);
            Assert.Single(topics);
        }

        [Fact]
        public async Task GetTopic_ReturnsNotFound_IdDoesnotExists()
        {
            var context = GetDatabaseContext();
            var controller = new TopicsController(context);
            var result = await controller.GetTopic(999);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PostTopic_ReturnsCreated_WhenModelIsValid()
        {
            var context = GetDatabaseContext();
            var user = new User { Id = 1, Login = "Tester", Email = "a@a.com", PasswordHash = "hash" };
            context.Users.Add(user);
            await context.SaveChangesAsync();
            var controller = new TopicsController(context);
            var newTopic = new CreateTopicDTO { Title = "New Topic", Description = "Created test", UserId = 1 };
            var result = await controller.PostTopic(newTopic);

            Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(1, context.Topics.Count());
        }


    }
}