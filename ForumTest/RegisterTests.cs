using System.ComponentModel.DataAnnotations;
using WebAPIForumApp.Controllers;
using WebAPIForumApp.DTOs;
using WebAPIForumApp.DTOs.User;

namespace WebAPIForumApp.Tests
{
    public class RegisterTests
    {
        private IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }

        [Fact]
        public void User_WithTooShortLogin_ShouldHaveValidationErrors()
        {
            var user = new RegisterDTO { Login = "abc", Password = "Password123", Email = "a@gmail.com" };
            var errors = ValidateModel(user);

            Assert.NotEmpty(errors);
            Assert.Contains(errors, v => v.MemberNames.Contains("Login"));
        }

        [Fact]
        public void User_WithCorrectLogin_ShouldNotHaveValidationErrors()
        {
            var user = new RegisterDTO { Login = "Admin123", Password = "password", Email = "a@a.com" };
            var errors = ValidateModel(user);

            Assert.Empty(errors);
        }

        [Fact]
        public void User_WithBadEmail_ShouldHaveValidationErrors()
        {
            var user = new RegisterDTO { Login = "aaaaa", Password = "password", Email = "aaa" };
            var errors = ValidateModel(user);

            Assert.NotEmpty(errors);
            Assert.Contains(errors, v => v.MemberNames.Contains("Email"));
        }

    }
}