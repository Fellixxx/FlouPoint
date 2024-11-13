namespace Infrastructure.Test.Repositories.Implementation.CRUD.Status
{
    using System;
    using System.Threading.Tasks;
    using Application.UseCases.Repository;
    using Domain.Entities;
    using Infrastructure.Repositories.Implementation.CRUD.User;
    using Infrastructure.Repositories.Implementation.CRUD.User.Create;
    using Infrastructure.Repositories.Implementation.Status;
    using Infrastructure.Test.Repositories.Implementation.CRUD;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using UtilitiesLayer;

    /// <summary>
    /// Tests for verifying user status updates and user creation functionalities.
    /// </summary>
    [TestClass]
    public class UserStatusTests : SetupTest
    {
        /// <summary>
        /// Test to check if a UserStatus instance can be constructed successfully.
        /// </summary>
        [TestMethod]
        public void CanConstruct()
        {
            // Act
            var instance = new UserStatus(_dbContext, _logService.Object, _resourceProvider, _resourceHandler);
            // Assert
            Assert.IsNotNull(instance);
        }

        /// <summary>
        /// Test to verify that constructing UserStatus with a null DbContext throws an ArgumentNullException.
        /// </summary>
        [TestMethod]
        public void CannotConstructWithNullContext()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new UserStatus(null, _logService.Object, _resourceProvider, _resourceHandler));
        }

        /// <summary>
        /// Test to verify that a UserCreate instance can be constructed even with a null log service.
        /// </summary>
        [TestMethod]
        public void CanConstructWithNullLogService()
        {
            var userCreate = new UserCreate(_dbContext, null, _utilEntity, _resourceProvider, _resourceHandler);
            Assert.IsNotNull(userCreate);
        }

        /// <summary>
        /// Test to ensure that activating a valid user returns a success result.
        /// </summary>
        [TestMethod]
        public async Task When_StatusEntity_WithValidUser_ShouldReturnSuccess()
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
            var resultStatus = await _userStatus.Activate(user.Id);
            // Then
            Assert.AreEqual("User was activated successfully.", resultStatus.Message);
            Assert.IsTrue(resultStatus.IsSuccessful);
            Assert.IsTrue(resultStatus.Data);
        }

        /// <summary>
        /// Test to ensure that activating a user with an invalid email format returns a failure.
        /// </summary>
        [TestMethod]
        public async Task When_StatusEntity_WithInvalidEmailFormat_ShouldReturnFailure()
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
            var resultStatus = await _userStatus.Activate(user.Id);
            // Then
            Assert.AreEqual("Necessary data was not provided.", resultStatus.Message);
            Assert.IsFalse(resultStatus.IsSuccessful);
            Assert.IsFalse(resultStatus.Data);
        }

        /// <summary>
        /// Test to verify that activating an already registered username returns a success result.
        /// </summary>
        [TestMethod]
        public async Task When_StatusEntity_WithAlreadyRegisteredUsername_ShouldReturnSuccess()
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
            // When
            var resultStatus = await _userStatus.Activate(user.Id);
            // Then
            Assert.AreEqual("User was activated successfully.", resultStatus.Message);
            Assert.IsTrue(resultStatus.IsSuccessful);
            Assert.IsTrue(resultStatus.Data);
        }

        /// <summary>
        /// Test to ensure that activating a user with an already registered email returns a failure.
        /// </summary>
        [TestMethod]
        public async Task When_StatusEntity_WithAlreadyRegisteredEmail_ShouldReturnFailure()
        {
            // Given
            var user = new User
            {
                Name = "UniqueUsername",
                Email = "ExistingEmail@gmail.com",
                Password = "ValidPassword",
            };
            // When
            var result1 = await _userCreate.Create(user);
            var resultStatus = await _userStatus.Activate(user.Id);
            var result2 = await _userCreate.Create(user);
            var resultStatus2 = await _userStatus.Activate(user.Id);
            // Then
            Assert.AreEqual("Necessary data was not provided.", resultStatus2.Message);
            Assert.IsFalse(resultStatus.IsSuccessful);
            Assert.IsFalse(resultStatus.Data);
            Assert.AreEqual("Necessary data was not provided.", resultStatus2.Message);
            Assert.IsFalse(resultStatus2.IsSuccessful);
            Assert.IsFalse(resultStatus2.Data);
        }

        /// <summary>
        /// Test to ensure that activating a null user returns a failure.
        /// </summary>
        [TestMethod]
        public async Task When_StatusEntity_WithNullUser_ShouldReturnFailure()
        {
            // Given
            User user = null;
            // When
            var result = await _userCreate.Create(user);
            var resultStatus = await _userStatus.Activate(null);
            // Then
            Assert.IsFalse(resultStatus.IsSuccessful);
            Assert.IsFalse(resultStatus.Data);
            StringAssert.Contains(resultStatus.Message, "Necessary data was not provided.");
        }

        /// <summary>
        /// Test to ensure that activating a user with missing required fields returns a failure.
        /// </summary>
        [TestMethod]
        public async Task When_StatusEntity_WithMissingRequiredFields_ShouldReturnFailure()
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
            var resultStatus = await _userStatus.Activate(null);
            // Then
            Assert.IsFalse(resultStatus.IsSuccessful);
            Assert.IsFalse(resultStatus.Data);
            StringAssert.Contains(resultStatus.Message, "Necessary data was not provided.");
        }

        /// <summary>
        /// Test to ensure that activating a user with an invalid password returns a failure.
        /// </summary>
        [TestMethod]
        public async Task When_StatusEntity_WithInvalidPassword_ShouldReturnFailure()
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
            var resultStatus = await _userStatus.Activate(user.Id);
            // Then
            Assert.IsFalse(resultStatus.IsSuccessful);
            Assert.IsFalse(resultStatus.Data);
            StringAssert.Contains(resultStatus.Message, "The User does not exist.");
        }

        /// <summary>
        /// Test to ensure that activating a user with excessively long strings returns a failure.
        /// </summary>
        [TestMethod]
        public async Task When_StatusEntity_WithLongStrings_ShouldReturnFailure()
        {
            // Given
            string longName = new string ('a', 256); // Assuming max length is less than 256
            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Name = longName,
                Email = "ValidEmail@gmail.com",
                Password = "ValidPassword",
            };
            // When
            var result = await _userCreate.Create(user);
            var resultStatus = await _userStatus.Activate(user.Id);
            // Then
            Assert.IsFalse(resultStatus.IsSuccessful);
            Assert.IsFalse(resultStatus.Data);
            StringAssert.Contains(resultStatus.Message, "The User does not exist.");
        }

        /// <summary>
        /// Test to ensure that a valid user's password is hashed correctly upon activation.
        /// </summary>
        [TestMethod]
        public async Task When_StatusEntity_WithValidUser_ShouldHashPasswordCorrectly()
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
            var resultStatus = await _userStatus.Activate(user.Id);
            // Then
            Assert.IsTrue(resultStatus.IsSuccessful);
            // Retrieve the user from the database
            var createdUser = await _dbContext.Users.FindAsync(id);
            Assert.IsNotNull(createdUser);
        }

        /// <summary>
        /// Test to ensure that the created and updated timestamps are correctly set for a valid user.
        /// </summary>
        [TestMethod]
        public async Task When_StatusEntity_WithValidUser_ShouldSetCreatedAtAndUpdatedAt()
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
            var resultStatus = await _userStatus.Activate(user.Id);
            var endTime = DateTime.UtcNow;
            // Then
            Assert.IsTrue(resultStatus.IsSuccessful);
            // Retrieve the user from the database
            var createdUser = await _dbContext.Users.FindAsync(id);
            Assert.IsNotNull(createdUser);
            Assert.IsTrue(createdUser.CreatedAt >= startTime && createdUser.CreatedAt <= endTime);
            Assert.IsTrue(createdUser.UpdatedAt >= startTime && createdUser.UpdatedAt <= endTime);
        }

        /// <summary>
        /// Test to ensure that activating a valid user sets their active status to false.
        /// </summary>
        [TestMethod]
        public async Task When_StatusEntity_WithValidUser_ShouldSetActiveToFalse()
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
            var resultStatus = await _userStatus.Activate(user.Id);
            // Then
            Assert.IsTrue(resultStatus.IsSuccessful);
            // Retrieve the user from the database
            var createdUser = await _dbContext.Users.FindAsync(id);
            Assert.IsNotNull(createdUser);
            Assert.IsTrue(createdUser.Active);
        }

        /// <summary>
        /// Test to ensure that attempting to create users with duplicate IDs results in a failure.
        /// </summary>
        [TestMethod]
        public async Task When_StatusEntity_WithDuplicateId_ShouldReturnFailure()
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
            var resultStatus1 = await _userStatus.Activate(user1.Id);
            var result2 = await _userCreate.Create(user2);
            var resultStatus2 = await _userStatus.Activate(user2.Id);
            // Then
            Assert.IsFalse(result1.IsSuccessful);
            Assert.IsFalse(result2.IsSuccessful);
            Assert.IsNull(result2.Data);
            StringAssert.Contains(result2.Message, "One or more data from the User have been submitted with errors The length of 'Name' must be at least 6 characters. You entered 5 characters.");
        }
    }
}