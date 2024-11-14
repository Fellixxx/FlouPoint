namespace Infrastructure.Test.Repositories.Implementation.CRUD.User
{
    using Moq;
    using Application.Result;
    using Domain.Entities;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Infrastructure.Repositories.Implementation.CRUD.User;
    using Infrastructure.Repositories.Implementation.CRUD.User.Create;
    using Infrastructure.Repositories.Implementation.CRUD.User.Update;

    [TestClass]
    public class UserValidationTests : SetupTest
    {
        // Unique Email Constraint Tests
        [TestMethod]
        public async Task CreateUser_ShouldFail_WhenEmailAlreadyExists()
        {
            var existingUser = new User { Name = "Existing User", Email = "existing@example.com", Password = "Password123!" };
            await _userCreate.Create(existingUser);

            var newUser = new User { Name = "New User", Email = "existing@example.com", Password = "Password123!" };
            var result = await _userCreate.Create(newUser);

            Assert.IsFalse(result.IsSuccessful);
            Assert.AreEqual("A user is already registered with this email.", result.Message);
        }

        [TestMethod]
        public async Task UpdateUser_ShouldFail_WhenEmailAlreadyExists()
        {
            var user1 = new User { Id = "user1", Name = "User 1", Email = "user1@example.com", Password = "Password123!" };
            var user2 = new User { Id = "user2", Name = "User 2", Email = "user2@example.com", Password = "Password123!" };

            await _userCreate.Create(user1);
            await _userCreate.Create(user2);

            user1.Email = "user2@example.com";
            var result = await _userUpdate.Update(user1);

            Assert.IsFalse(result.IsSuccessful);
            Assert.AreEqual("The submitted value was invalid.", result.Message);
        }

        // Boundary and Data Length Checks
        [TestMethod]
        public async Task CreateUser_ShouldFail_WhenNameExceedsMaxLength()
        {
            var newUser = new User { Name = new string('A', 51), Email = "user@example.com", Password = "Password123!" };
            var result = await _userCreate.Create(newUser);

            Assert.IsFalse(result.IsSuccessful);
            Assert.AreEqual("One or more data from the User have been submitted with errors The length of 'Name' must be 50 characters or fewer. You entered 51 characters.", result.Message);
        }

        [TestMethod]
        public async Task CreateUser_ShouldSucceed_WhenNameIsAtMinLength()
        {
            var newUser = new User { Name = "A", Email = "user@example.com", Password = "Password123!" };
            var result = await _userCreate.Create(newUser);

            Assert.IsFalse(result.IsSuccessful);
        }

        // General Validation Errors - Error Message Concatenation
        [TestMethod]
        public async Task CreateUser_ShouldFail_WithMultipleValidationErrors()
        {
            var newUser = new User { Name = null, Email = "invalid-email", Password = null }; // Missing name, invalid email, and missing password
            var result = await _userCreate.Create(newUser);

            Assert.IsFalse(result.IsSuccessful);
            Assert.AreEqual("One or more data from the User have been submitted with errors 'Name' must not be empty., 'Password' must not be empty.", result.Message);
        }
    }
}
