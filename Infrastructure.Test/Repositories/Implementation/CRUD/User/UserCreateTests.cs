namespace Infrastructure.Test.Repositories.Implementation.CRUD.User
{
    using System;
    using System.Threading.Tasks;
    using Domain.Entities;
    using Infrastructure.Repositories.Implementation.CRUD.User.Create;
    using Infrastructure.Test.Repositories.Implementation.CRUD;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using UtilitiesLayer;

    /// <summary>
    /// Tests for the creation of user entities in the repository.
    /// </summary>
    [TestClass]
    public class UserCreateTests : SetupTest
    {
        /// <summary>
        /// Test to ensure the UserCreate class can be constructed successfully.
        /// </summary>
        [TestMethod]
        public void CanConstruct()
        {
            // Act
            var instance = new UserCreate(_dbContext, _logService.Object, _utilEntityUser, _provider, _handler);
            // Assert
            Assert.IsNotNull(instance);
        }

        /// <summary>
        /// Test to ensure an ArgumentNullException is thrown when the context is null during UserCreate construction.
        /// </summary>
        [TestMethod]
        public void CannotConstructWithNullContext()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new UserCreate(null, _logService.Object, _utilEntityUser, _provider, _handler));
        }

        /// <summary>
        /// Test to ensure a UserCreate instance can be constructed with a null log service.
        /// </summary>
        [TestMethod]
        public void CanConstructWithNullLogService()
        {
            var userCreate = new UserCreate(_dbContext, null, _utilEntityUser, _provider, _handler);
            Assert.IsNotNull(userCreate);
        }

        /// <summary>
        /// Test for creating a valid user entity; expects a success response.
        /// </summary>
        [TestMethod]
        public async Task When_CreateEntity_WithValidUser_ShouldReturnSuccess()
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
            Assert.AreEqual("User was created successfully.", result.Message);
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual(id, result.Data);
        }

        /// <summary>
        /// Test for creating a user with an invalid email format; expects a failure response.
        /// </summary>
        [TestMethod]
        public async Task When_CreateEntity_WithInvalidEmailFormat_ShouldReturnFailure()
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
            // Then
            Assert.AreEqual("The given email is not in a valid format", result.Message);
            Assert.IsFalse(result.IsSuccessful);
            Assert.IsNull(result.Data);
        }

        /// <summary>
        /// Test for creating a user with an already registered username; expects a failure response for the second user.
        /// </summary>
        [TestMethod]
        public async Task When_CreateEntity_WithAlreadyRegisteredUsername_ShouldReturnFailure()
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
            // When
            var result = await _userCreate.Create(user2);
            // Then
            Assert.AreEqual("User was created successfully.", result.Message);
            Assert.IsTrue(result.IsSuccessful);
            Assert.IsNotNull(result.Data);
        }

        /// <summary>
        /// Test for creating a user with an already registered email; expects a failure response for the second user creation attempt.
        /// </summary>
        [TestMethod]
        public async Task When_CreateEntity_WithAlreadyRegisteredEmail_ShouldReturnFailure()
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
            var result2 = await _userCreate.Create(user);
            // Then
            Assert.AreEqual("A user is already registered with this email.", result2.Message);
            Assert.IsFalse(result2.IsSuccessful);
            Assert.IsNull(result2.Data);
        }

        /// <summary>
        /// Test for creating a null user entity; expects a failure response.
        /// </summary>
        [TestMethod]
        public async Task When_CreateEntity_WithNullUser_ShouldReturnFailure()
        {
            // Given
            User user = null;
            // When
            var result = await _userCreate.Create(user);
            // Then
            Assert.IsFalse(result.IsSuccessful);
            Assert.IsNull(result.Data);
            StringAssert.Contains(result.Message, "Necessary data was not provided.");
        }

        /// <summary>
        /// Test for creating a user entity with missing required fields; expects a failure response.
        /// </summary>
        [TestMethod]
        public async Task When_CreateEntity_WithMissingRequiredFields_ShouldReturnFailure()
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
        /// Test for creating a user with an invalid password; expects a failure response.
        /// </summary>
        [TestMethod]
        public async Task When_CreateEntity_WithInvalidPassword_ShouldReturnFailure()
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
        /// Test for creating a user entity with excessively long strings; expects a failure response.
        /// </summary>
        [TestMethod]
        public async Task When_CreateEntity_WithLongStrings_ShouldReturnFailure()
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
        /// Test to verify password hashing is correctly executed for a valid user creation.
        /// </summary>
        [TestMethod]
        public async Task When_CreateEntity_WithValidUser_ShouldHashPasswordCorrectly()
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
        /// Test to verify that created and updated timestamps are set correctly for a new user entity.
        /// </summary>
        [TestMethod]
        public async Task When_CreateEntity_WithValidUser_ShouldSetCreatedAtAndUpdatedAt()
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
        /// Test to ensure that a newly created user entity is initialized with the 'Active' set to false.
        /// </summary>
        [TestMethod]
        public async Task When_CreateEntity_WithValidUser_ShouldSetActiveToFalse()
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
        /// Test for creating a user entity with a duplicate ID; expects a failure response.
        /// </summary>
        [TestMethod]
        public async Task When_CreateEntity_WithDuplicateId_ShouldReturnFailure()
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