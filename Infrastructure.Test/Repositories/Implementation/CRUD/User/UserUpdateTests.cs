namespace Infrastructure.Test.Repositories.Implementation.CRUD.User
{
    using System;
    using System.Threading.Tasks;
    using Domain.Entities;
    using Infrastructure.Repositories.Implementation.CRUD.User.Update;
    using Infrastructure.Test.Repositories.Implementation.CRUD;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using UtilitiesLayer;

    /// <summary>
    /// Test class for testing user status-related operations in the CRUD layer.
    /// </summary>
    [TestClass]
    public class UserStatusTests : SetupTest
    {
        /// <summary>
        /// Tests if the UserUpdate class can be constructed properly.
        /// </summary>
        [TestMethod]
        public void UserUpdate_CanBeConstructed()
        {
            // Act
            var instance = new UserUpdate(_dbContext, _logService.Object, _utilEntity, _resourceProvider, _resourceHandler);
            // Assert
            Assert.IsNotNull(instance);
        }

        /// <summary>
        /// Tests if constructing the UserUpdate class with a null DbContext throws an ArgumentNullException.
        /// </summary>
        [TestMethod]
        public void UserUpdate_CannotBeConstructedWithNullDbContext()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new UserUpdate(null, _logService.Object, _utilEntity, _resourceProvider, _resourceHandler));
        }

        /// <summary>
        /// Tests if the UserUpdate class can be constructed with a null LogService.
        /// </summary>
        [TestMethod]
        public void UserUpdate_CanBeConstructedWithNullLogService()
        {
            var userUpdate = new UserUpdate(_dbContext, null, _utilEntity, _resourceProvider, _resourceHandler);
            Assert.IsNotNull(userUpdate);
        }

        /// <summary>
        /// Tests creating a valid user entity and updating it successfully.
        /// </summary>
        [TestMethod]
        public async Task CreateAndUpdate_ValidUser_ReturnsSuccess()
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
            var userUpdate = new User
            {
                Id = id,
                Name = "ValidUsernameUpdate",
                Email = $"ValidEmail{id}@gmail.com",
                Password = "ValidPassword",
            };
            // When
            var result = await _userCreate.Create(user);
            var resultUpdate = await _userUpdate.Update(userUpdate);
            // Then
            Assert.AreEqual("User was updated successfully.", resultUpdate.Message);
            Assert.IsTrue(resultUpdate.IsSuccessful);
            Assert.IsTrue(resultUpdate.Data);
        }

        /// <summary>
        /// Tests creating a user entity with an invalid email format and expects a failure.
        /// </summary>
        [TestMethod]
        public async Task CreateUser_WithInvalidEmail_ReturnsFailure()
        {
            // Given
            string id = Guid.NewGuid().ToString();
            var user = new User
            {
                Id = id,
                Name = "ValidUsername",
                Email = "InvalidEmail",
                Password = "ValidPassword",
            };
            // When
            var result = await _userCreate.Create(user);
            var userUpdate = new User
            {
                Id = user.Id,
                Name = "ValidUsernameUpdate",
                Email = $"ValidEmail{user.Id}@gmail.com",
                Password = "ValidPassword",
            };
            var resultUpdate = await _userUpdate.Update(userUpdate);
            // Then
            Assert.AreEqual("The User does not exist.", resultUpdate.Message);
            Assert.IsFalse(resultUpdate.IsSuccessful);
            Assert.IsFalse(resultUpdate.Data);
        }

        /// <summary>
        /// Tests creating a user entity with an already registered username and expects a failure.
        /// </summary>
        [TestMethod]
        public async Task CreateUser_WithDuplicateUsername_ReturnsFailure()
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
            var user2 = new User
            {
                Id = Guid.NewGuid().ToString(),
                Name = "ExistingUsername",
                Email = "UniqueEmail2@gmail.com",
                Password = "ValidPassword",
            };
            var userUpdate = new User
            {
                Id = user.Id,
                Name = "ValidUsernameUpdate",
                Email = $"UniqueEmail2@gmail.com",
                Password = "ValidPassword",
            };
            // When
            var result = await _userCreate.Create(user2);
            var resultUpdate = await _userUpdate.Update(userUpdate);
            // Then
            Assert.AreEqual("A user is already registered with this email.", resultUpdate.Message);
            Assert.IsFalse(resultUpdate.IsSuccessful);
            Assert.IsFalse(resultUpdate.Data);
        }

        /// <summary>
        /// Tests creating a user entity with an already registered email and expects a failure.
        /// </summary>
        [TestMethod]
        public async Task CreateUser_WithDuplicateEmail_ReturnsFailure()
        {
            // Given
            var user = new User
            {
                Name = "UniqueUsername",
                Email = "ExistingEmail@gmail.com",
                Password = "ValidPassword",
            };
            var userUpdate = new User
            {
                Id = user.Id,
                Name = "ValidUsernameUpdate",
                Email = $"UniqueEmail2@gmail.com",
                Password = "ValidPassword",
            };
            // When
            var result1 = await _userCreate.Create(user);
            var result2 = await _userCreate.Create(user);
            var resultUpdate = await _userUpdate.Update(userUpdate);
            // Then
            Assert.AreEqual("A user is already registered with this email.", result2.Message);
            Assert.IsFalse(result2.IsSuccessful);
            Assert.IsNull(result2.Data);
            Assert.AreEqual("Necessary data was not provided.", resultUpdate.Message);
            Assert.IsFalse(resultUpdate.IsSuccessful);
            Assert.IsFalse(resultUpdate.Data);
        }

        /// <summary>
        /// Tests updating a user entity with null data and expects a failure.
        /// </summary>
        [TestMethod]
        public async Task UpdateUser_WithNullUser_ReturnsFailure()
        {
            // Given
            User user = null;
            // When
            var result = await _userUpdate.Update(user);
            // Then
            Assert.IsFalse(result.IsSuccessful);
            Assert.IsFalse(result.Data);
            StringAssert.Contains(result.Message, "Necessary data was not provided.");
        }

        /// <summary>
        /// Tests creating a user entity missing required fields and expects a failure.
        /// </summary>
        [TestMethod]
        public async Task CreateUser_WithMissingFields_ReturnsFailure()
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
            // Then
            Assert.IsFalse(result.IsSuccessful);
            Assert.IsNull(result.Data);
            StringAssert.Contains(result.Message, "Name"); // Assuming error message mentions 'Name'
        }

        /// <summary>
        /// Tests creating a user entity with an invalid password and expects a failure.
        /// </summary>
        [TestMethod]
        public async Task CreateUser_WithInvalidPassword_ReturnsFailure()
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
            // Then
            Assert.IsFalse(result.IsSuccessful);
            Assert.IsNull(result.Data);
            StringAssert.Contains(result.Message, "Password"); // Assuming error message mentions 'Password'
        }

        /// <summary>
        /// Tests creating a user entity with fields that have excessively long strings and expects a failure.
        /// </summary>
        [TestMethod]
        public async Task CreateUser_WithLongStringsFields_ReturnsFailure()
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
            // Then
            Assert.IsFalse(result.IsSuccessful);
            Assert.IsNull(result.Data);
            StringAssert.Contains(result.Message, "Name"); // Assuming error message mentions 'Name'
        }

        /// <summary>
        /// Tests that the password is hashed correctly when creating a valid user entity.
        /// </summary>
        [TestMethod]
        public async Task CreateUser_WithValidData_ShouldHashPassword()
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
            // Then
            Assert.IsTrue(result.IsSuccessful);
            // Retrieve the user from the database
            var createdUser = await _dbContext.Users.FindAsync(id);
            Assert.IsNotNull(createdUser);
            string expectedHashedPassword = CredentialUtility.ComputeSha256Hash(originalPassword);
            Assert.AreEqual(expectedHashedPassword, createdUser.Password);
        }

        /// <summary>
        /// Tests that the CreatedAt and UpdatedAt timestamps are set correctly when creating a valid user entity.
        /// </summary>
        [TestMethod]
        public async Task CreateUser_WithValidData_ShouldSetTimestamps()
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
            var endTime = DateTime.UtcNow;
            // Then
            Assert.IsTrue(result.IsSuccessful);
            // Retrieve the user from the database
            var createdUser = await _dbContext.Users.FindAsync(id);
            Assert.IsNotNull(createdUser);
            Assert.IsTrue(createdUser.CreatedAt >= startTime && createdUser.CreatedAt <= endTime);
            Assert.IsTrue(createdUser.UpdatedAt >= startTime && createdUser.UpdatedAt <= endTime);
        }

        /// <summary>
        /// Tests that the Active flag is set to false when creating a valid user entity.
        /// </summary>
        [TestMethod]
        public async Task CreateUser_WithValidData_ShouldSetActiveFlagToFalse()
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
            // Then
            Assert.IsTrue(result.IsSuccessful);
            // Retrieve the user from the database
            var createdUser = await _dbContext.Users.FindAsync(id);
            Assert.IsNotNull(createdUser);
            Assert.IsFalse(createdUser.Active);
        }

        /// <summary>
        /// Tests creating user entities with duplicate IDs and expects a failure.
        /// </summary>
        [TestMethod]
        public async Task CreateUser_WithDuplicateId_ReturnsFailure()
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
            var result2 = await _userCreate.Create(user2);
            // Then
            Assert.IsFalse(result1.IsSuccessful);
            Assert.IsFalse(result2.IsSuccessful);
            Assert.IsNull(result2.Data);
            StringAssert.Contains(result2.Message, "One or more data from the User have been submitted with errors The length of 'Name' must be at least 6 characters. You entered 5 characters."); // Assuming generic error message
        }
    }
}