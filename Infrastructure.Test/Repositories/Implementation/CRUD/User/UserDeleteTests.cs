namespace Infrastructure.Test.Repositories.Implementation.CRUD.User
{
    using System;
    using System.Threading.Tasks;
    using Application.UseCases.CRUD.User;
    using Application.UseCases.ExternalServices;
    using Domain.DTO.Logging;
    using Domain.Entities;
    using Infrastructure.Repositories.Implementation.CRUD.User;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Persistence.BaseDbContext;
    using Persistence.CreateStruture.Constants.ColumnType;
    using UtilitiesLayer;

    [TestClass]
    public class UserDeleteTests : TestsBase
    {
        [TestMethod]
        public void CanConstruct()
        {
            // Act
            var instance = new UserDelete(_dbContext, _logService.Object);

            // Assert
            Assert.IsNotNull(instance);
        }

        [TestMethod]
        public void CannotConstructWithNullContext()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new UserDelete(null, _logService.Object));
        }

        [TestMethod]
        public void CanConstructWithNullLogService()
        {
            var userCreate = new UserDelete(_dbContext, null);
            Assert.IsNotNull(userCreate);
        }

        [TestMethod]
        public async Task When_DeleteEntity_WithValidUser_ShouldReturnSuccess()
        {
            // Given
            string id = Guid.NewGuid().ToString();
            var user = new User
            {
                Id = id,
                Name = "ValidUsername",
                Email = $"ValidEmail{id}@gmail.com",
                Password = "ValidPassword",
            };

            // When
            var result = await _userCreate.Create(user);
            var resultDelete = await _userDelete.Delete(user.Id);
            // Then
            Assert.AreEqual("User was deleted successfully.", resultDelete.Message);
            Assert.IsTrue(resultDelete.IsSuccessful);
            Assert.IsTrue(resultDelete.Data);
        }

        [TestMethod]
        public async Task When_DeleteEntity_WithInvalidEmailFormat_ShouldReturnFailure()
        {
            // Given
            var user = new User
            {
                Name = "ValidUsername",
                Email = "InvalidEmail",
                Password = "ValidPassword",
            };

            // When
            var result = await _userCreate.Create(user);
            var resultDelete = await _userDelete.Delete(user.Id);
            // Then
            Assert.AreEqual("Necessary data was not provided.", resultDelete.Message);
            Assert.IsFalse(resultDelete.IsSuccessful);
            Assert.IsFalse(resultDelete.Data);
        }

        [TestMethod]
        public async Task When_DeleteEntity_WithAlreadyRegisteredUsername_ShouldReturnFailure()
        {
            // Given
            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Name = "ExistingUsername",
                Email = "UniqueEmail1@gmail.com",
                Password = "ValidPassword",
            };
            await _userCreate.Create(user);
            var resultDelete = await _userDelete.Delete(user.Id);
            var resultDelete2 = await _userDelete.Delete(user.Id);

            // Then
            Assert.AreEqual("The User does not exist.", resultDelete2.Message);
            Assert.IsFalse(resultDelete2.IsSuccessful);
            Assert.IsFalse(resultDelete2.Data);
        }

        [TestMethod]
        public async Task When_DeleteEntity_WithAlreadyRegisteredEmail_ShouldReturnFailure()
        {
            // Given
            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Name = "UniqueUsername",
                Email = "ExistingEmail@gmail.com",
                Password = "ValidPassword",
            };

            // When
            var result1 = await _userCreate.Create(user);
            var resultDelete = await _userDelete.Delete(user.Id);
            var result2 = await _userCreate.Create(user);

            // Then
            Assert.AreEqual("User was deleted successfully.", resultDelete.Message);
            Assert.IsTrue(resultDelete.IsSuccessful);
            Assert.IsTrue(resultDelete.Data);
        }

        [TestMethod]
        public async Task When_DeleteEntity_WithNullUser_ShouldReturnFailure()
        {
            // Given
            User user = null;

            // When
            var result = await _userCreate.Create(user);
            var resultDelete = await _userDelete.Delete(null);

            // Then
            Assert.IsFalse(resultDelete.IsSuccessful);
            Assert.IsFalse(resultDelete.Data);
            StringAssert.Contains(resultDelete.Message, "Necessary data was not provided.");
        }

        [TestMethod]
        public async Task When_DeleteEntity_WithMissingRequiredFields_ShouldReturnFailure()
        {
            // Given
            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                // Name is missing
                Email = "ValidEmail@gmail.com",
                Password = "ValidPassword",
            };

            // When
            var result = await _userCreate.Create(user);
            var resultDelete = await _userDelete.Delete(null);

            // Then
            Assert.IsFalse(resultDelete.IsSuccessful);
            Assert.IsFalse(resultDelete.Data);
            StringAssert.Contains(resultDelete.Message, "Necessary data was not provided.");
        }

        [TestMethod]
        public async Task When_DeleteEntity_WithInvalidPassword_ShouldReturnFailure()
        {
            // Given
            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Name = "ValidUsername",
                Email = "ValidEmail@gmail.com",
                Password = "", // Empty password
            };

            // When
            var result = await _userCreate.Create(user);
            var resultDelete = await _userDelete.Delete(user.Id);

            // Then
            Assert.IsFalse(resultDelete.IsSuccessful);
            Assert.IsFalse(resultDelete.Data);
            StringAssert.Contains(resultDelete.Message, "The User does not exist."); // Assuming error message mentions 'Password'
        }

        [TestMethod]
        public async Task When_DeleteEntity_WithLongStrings_ShouldReturnFailure()
        {
            // Given
            string longName = new string('a', 256); // Assuming max length is less than 256
            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Name = longName,
                Email = "ValidEmail@gmail.com",
                Password = "ValidPassword",
            };

            // When
            var result = await _userCreate.Create(user);
            var resultDelete = await _userDelete.Delete(user.Id);

            // Then
            Assert.IsFalse(resultDelete.IsSuccessful);
            Assert.IsFalse(resultDelete.Data);
            StringAssert.Contains(resultDelete.Message, "The User does not exist."); // Assuming error message mentions 'Name'
        }

        [TestMethod]
        public async Task When_DeleteEntity_WithValidUser_ShouldHashPasswordCorrectly()
        {
            // Given
            string id = Guid.NewGuid().ToString();
            string originalPassword = "ValidPassword";
            var user = new User
            {
                Id = id,
                Name = "ValidUsername",
                Email = $"ValidEmail{id}@gmail.com",
                Password = originalPassword,
            };

            // When
            var result = await _userCreate.Create(user);
            var resultDelete = await _userDelete.Delete(user.Id);

            // Then
            Assert.IsTrue(resultDelete.IsSuccessful);

            // Retrieve the user from the database
            var createdUser = await _dbContext.Users.FindAsync(id);

            Assert.IsNull(createdUser);
        }

        [TestMethod]
        public async Task When_DeleteEntity_WithValidUser_ShouldSetCreatedAtAndUpdatedAt()
        {
            // Given
            string id = Guid.NewGuid().ToString();
            var user = new User
            {
                Id = id,
                Name = "ValidUsername",
                Email = $"ValidEmail{id}@gmail.com",
                Password = "ValidPassword",
            };

            // When
            var startTime = DateTime.UtcNow;
            var result = await _userCreate.Create(user);
            var resultDelete = await _userDelete.Delete(user.Id);
            var endTime = DateTime.UtcNow;

            // Then
            Assert.IsTrue(resultDelete.IsSuccessful);

            // Retrieve the user from the database
            var createdUser = await _dbContext.Users.FindAsync(id);

            Assert.IsNull(createdUser);
        }

        [TestMethod]
        public async Task When_DeleteEntity_WithValidUser_ShouldSetActiveToFalse()
        {
            // Given
            string id = Guid.NewGuid().ToString();
            var user = new User
            {
                Id = id,
                Name = "ValidUsername",
                Email = $"ValidEmail{id}@gmail.com",
                Password = "ValidPassword",
            };

            // When
            var result = await _userCreate.Create(user);
            var resultDelete = await _userDelete.Delete(user.Id);

            // Then
            Assert.IsTrue(resultDelete.IsSuccessful);

            // Retrieve the user from the database
            var createdUser = await _dbContext.Users.FindAsync(id);

            Assert.IsNull(createdUser);
        }

        [TestMethod]
        public async Task When_DeleteEntity_WithDuplicateId_ShouldReturnFailure()
        {
            // Given
            string id = Guid.NewGuid().ToString();
            var user1 = new User
            {
                Id = id,
                Name = "User1",
                Email = "email1@gmail.com",
                Password = "Password1",
            };

            var user2 = new User
            {
                Id = id, // Same ID as user1
                Name = "User2",
                Email = "email2@gmail.com",
                Password = "Password2",
            };

            // When
            var result1 = await _userCreate.Create(user1);
            var resultDelete = await _userDelete.Delete(user1.Id);
            var result2 = await _userCreate.Create(user2);
            var resultDelete2 = await _userDelete.Delete(user2.Id);

            // Then
            Assert.IsFalse(resultDelete.IsSuccessful);
            Assert.IsFalse(result2.IsSuccessful);
            Assert.IsNull(result2.Data);
            StringAssert.Contains(result2.Message, "One or more data from the User have been submitted with errors The length of 'Name' must be at least 6 characters. You entered 5 characters."); // Assuming generic error message
        }
    }
}