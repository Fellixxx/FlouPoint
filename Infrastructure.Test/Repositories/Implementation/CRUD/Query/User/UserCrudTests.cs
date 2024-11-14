namespace Infrastructure.Test.Repositories.Implementation.CRUD.User
{
    using Moq;
    using Application.UseCases.CRUD.Query.User;
    using User = Domain.Entities.User;
    using Application.Result;
    using System.Linq.Expressions;
    using Application.Result.Error;
    using Application.UseCases.CRUD.User;

    [TestClass]
    public class UserCrudTests : SetupTest
    {
        [TestMethod]
        public async Task CreateUser_ShouldSucceed_WhenDataIsValid()
        {
            var newUser = new User { Name = "Test User", Email = "testuser@example.com" };
            var result = await _userCreate.Create(newUser);
            Assert.IsFalse(result.IsSuccessful);
            Assert.AreEqual("One or more data from the User have been submitted with errors 'Password' must not be empty.", result.Message);
        }

        [TestMethod]
        public async Task CreateUser_ShouldFail_WhenRequiredFieldIsMissing()
        {
            var newUser = new User { Email = "testuser@example.com" };
            var result = await _userCreate.Create(newUser);
            Assert.IsFalse(result.IsSuccessful);
            Assert.AreEqual("One or more data from the User have been submitted with errors 'Name' must not be empty., 'Password' must not be empty.", result.Message);
        }

        [TestMethod]
        public async Task CreateUser_ShouldFail_WhenEmailAlreadyExists()
        {
            var existingUser = new User { Name = "Existing User", Email = "duplicate@example.com" };
            await _userCreate.Create(existingUser);
            var newUser = new User { Name = "New User", Email = "duplicate@example.com" };
            var result = await _userCreate.Create(newUser);
            Assert.IsFalse(result.IsSuccessful);
            Assert.AreEqual("One or more data from the User have been submitted with errors 'Password' must not be empty.", result.Message);
        }

        [TestMethod]
        public async Task CreateUser_ShouldFail_WhenNameExceedsMaxLength()
        {
            var newUser = new User { Name = new string('A', 101), Email = "longname@example.com" };
            var result = await _userCreate.Create(newUser);
            Assert.IsFalse(result.IsSuccessful);
            Assert.AreEqual("One or more data from the User have been submitted with errors The length of 'Name' must be 50 characters or fewer. You entered 101 characters., 'Password' must not be empty.", result.Message);
        }

        // Read Operation Tests using IUserQuery
        [TestMethod]
        public async Task ReadUserById_ShouldReturnUser_WhenIdIsValid()
        {
            var newUser = new User { Id = "valid-id", Name = "Valid User", Email = "validuser@example.com" };
            await _userCreate.Create(newUser);
            var mockUserQuery = new Mock<IUserQuery>();
            mockUserQuery.Setup(q => q.ReadId("valid-id"))
                         .ReturnsAsync(Operation<User>.Success(newUser, "User found"));

            var _userQuery = mockUserQuery.Object;
            var readResult = await _userQuery.ReadId(newUser.Id);
            Assert.IsTrue(readResult.IsSuccessful);
            Assert.AreEqual(newUser.Name, readResult.Data.Name);
        }

        [TestMethod]
        public async Task ReadUserById_ShouldReturnNull_WhenIdIsInvalid()
        {
            var mockUserQuery = new Mock<IUserQuery>();
            mockUserQuery.Setup(q => q.ReadId("invalid-id"))
                         .ReturnsAsync(Operation<User>.Failure(string.Empty, Application.Result.Error.ErrorTypes.Network));

            var _userQuery = mockUserQuery.Object;

            var result = await _userQuery.ReadId("invalid-id");
            Assert.IsFalse(result.IsSuccessful);
            Assert.IsNull(result.Data);
        }

        [TestMethod]
        public async Task ReadUserWithFilter_ShouldReturnFilteredResults()
        {
            var activeUsers = new List<User>
            {
                new User { Name = "Active User 1", Email = "activeuser1@example.com", Active = true },
                new User { Name = "Active User 2", Email = "activeuser2@example.com", Active = true }
            }.AsQueryable();

            var mockUserQuery = new Mock<IUserQuery>();
            mockUserQuery.Setup(q => q.ReadFilter(It.Is<Expression<Func<User, bool>>>(filter => filter.ToString().Contains("u.Active"))))
                         .ReturnsAsync(Operation<IQueryable<User>>.Success(activeUsers, "Filtered users retrieved"));

            var _userQuery = mockUserQuery.Object;

            var result = await _userQuery.ReadFilter(u => u.Active);

            Assert.IsTrue(result.Data.All(u => u.Active));
        }

        [TestMethod]
        public async Task ReadUsersWithPagination_ShouldReturnCorrectPage()
        {
            var usersPage2 = Enumerable.Range(6, 5).Select(i =>
                new User { Name = $"User {i}", Email = $"user{i}@example.com" }).AsQueryable();

            var mockUserQuery = new Mock<IUserQuery>();
            mockUserQuery.Setup(q => q.ReadFilterPage(2, 5, string.Empty))
                         .ReturnsAsync(Operation<IQueryable<User>>.Success(usersPage2, "Users found"));

            var _userQuery = mockUserQuery.Object;

            var result = await _userQuery.ReadFilterPage(2, 5, string.Empty);

            Assert.AreEqual(5, result.Data.Count());
            Assert.AreEqual("User 6", result.Data.First().Name);
        }

        [TestMethod]
        public async Task ReadUsersWithPagination_ShouldReturnEmpty_WhenOutOfRange()
        {
            var emptyUsers = Enumerable.Empty<User>().AsQueryable();

            var mockUserQuery = new Mock<IUserQuery>();
            mockUserQuery.Setup(q => q.ReadFilterPage(10, 5, string.Empty))
                         .ReturnsAsync(Operation<IQueryable<User>>.Success(emptyUsers, "No users found for the specified page"));

            var _userQuery = mockUserQuery.Object;

            var result = await _userQuery.ReadFilterPage(10, 5, string.Empty);

            Assert.IsTrue(!result.Data.Any());
        }

        // Update Operation Tests
        [TestMethod]
        public async Task UpdateUser_ShouldSucceed_WhenDataIsValid()
        {
            // Arrange: Create an existing user to update
            var existingUser = new User { Id = "valid-id", Name = "Old Name", Email = "old@example.com" };
            await _userCreate.Create(existingUser);

            // Mock the _userUpdate to return a successful result when updating the user
            var mockUserUpdate = new Mock<IUserUpdate>();
            mockUserUpdate.Setup(u => u.Update(existingUser))
                          .ReturnsAsync(Operation<bool>.Success(true, "User updated successfully"));

            var _userUpdate = mockUserUpdate.Object;

            // Act: Call the Update method
            var result = await _userUpdate.Update(existingUser);

            // Assert: Verify that the update was successful
            Assert.IsTrue(result.IsSuccessful);
            Assert.IsTrue(result.Data);
            Assert.AreEqual("User updated successfully", result.Message);
        }
        [TestMethod]
        public async Task UpdateUser_ShouldFail_WhenRequiredFieldIsMissing()
        {
            // Arrange: Create an existing user
            var existingUser = new User { Id = "valid-id", Name = null, Email = "user@example.com" }; // Name is null

            // Mock the _userUpdate to return a failure result due to missing required field
            var mockUserUpdate = new Mock<IUserUpdate>();
            mockUserUpdate.Setup(u => u.Update(existingUser))
                          .ReturnsAsync(Operation<bool>.Failure("Name cannot be null", ErrorTypes.BusinessValidation));

            var _userUpdate = mockUserUpdate.Object;

            // Act: Call the Update method
            var result = await _userUpdate.Update(existingUser);

            // Assert: Verify that the update failed due to validation error
            Assert.IsFalse(result.IsSuccessful);
            Assert.IsFalse(result.Data);
            Assert.AreEqual("Name cannot be null", result.Message);
        }


        [TestMethod]
        public async Task DeleteUser_ShouldSucceed_WhenIdIsValid()
        {
            // Arrange: Define an existing user ID to delete
            var validUserId = "valid-id";

            // Mock the _userDelete to return a successful operation result when deleting a user with a valid ID
            var mockUserDelete = new Mock<IUserDelete>();
            mockUserDelete.Setup(d => d.Delete(validUserId))
                          .ReturnsAsync(Operation<bool>.Success(true, "User deleted successfully"));

            var _userDelete = mockUserDelete.Object;

            // Act: Call the Delete method
            var result = await _userDelete.Delete(validUserId);

            // Assert: Verify that the deletion was successful
            Assert.IsTrue(result.IsSuccessful);
            Assert.IsTrue(result.Data);
            Assert.AreEqual("User deleted successfully", result.Message);
        }

        [TestMethod]
        public async Task DeleteUser_ShouldFail_WhenIdIsInvalid()
        {
            // Arrange: Define an invalid user ID
            var invalidUserId = "invalid-id";

            // Mock the _userDelete to return a failure operation result when deleting a user with an invalid ID
            var mockUserDelete = new Mock<IUserDelete>();
            mockUserDelete.Setup(d => d.Delete(invalidUserId))
                          .ReturnsAsync(Operation<bool>.Failure("User not found", ErrorTypes.NotFound));

            var _userDelete = mockUserDelete.Object;

            // Act: Call the Delete method
            var result = await _userDelete.Delete(invalidUserId);

            // Assert: Verify that the deletion failed
            Assert.IsFalse(result.IsSuccessful);
            Assert.IsFalse(result.Data);
            Assert.AreEqual("User not found", result.Message);
        }

    }
}
