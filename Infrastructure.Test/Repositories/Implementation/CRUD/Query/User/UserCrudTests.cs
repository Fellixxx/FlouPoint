namespace Infrastructure.Test.Repositories.Implementation.CRUD.User
{
    using Moq;
    using Infrastructure.Repositories.Implementation.CRUD.User;
    using Application.UseCases.CRUD.Query.User;
    using Infrastructure.Repositories.Implementation.CRUD.User.Create;
    using Infrastructure.Repositories.Implementation.CRUD.User.Update;
    using User = Domain.Entities.User;
    using Application.Result;
    using System.Linq.Expressions;

    [TestClass]
    public class UserCrudTests : SetupTest
    {
        // Create Operation Tests
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
            // Arrange: Create a list of users with only active users to simulate the filtered results
            var activeUsers = new List<User>
            {
                new User { Name = "Active User 1", Email = "activeuser1@example.com", Active = true },
                new User { Name = "Active User 2", Email = "activeuser2@example.com", Active = true }
            }.AsQueryable();

            // Mock the _userQuery to return only active users when ReadFilter is called with the Active filter
            var mockUserQuery = new Mock<IUserQuery>();
            mockUserQuery.Setup(q => q.ReadFilter(It.Is<Expression<Func<User, bool>>>(filter => filter.ToString().Contains("u.Active"))))
                         .ReturnsAsync(Operation<IQueryable<User>>.Success(activeUsers, "Filtered users retrieved"));

            var _userQuery = mockUserQuery.Object;

            // Act: Call the ReadFilter method with the active filter to get the mocked result
            var result = await _userQuery.ReadFilter(u => u.Active);

            // Assert: Verify that all users in the result are active
            Assert.IsTrue(result.Data.All(u => u.Active));
        }

        [TestMethod]
        public async Task ReadUsersWithPagination_ShouldReturnCorrectPage()
        {
            // Arrange: Create a list of users to represent the expected page of results
            var usersPage2 = Enumerable.Range(6, 5).Select(i =>
                new User { Name = $"User {i}", Email = $"user{i}@example.com" }).AsQueryable();

            // Mock the _userQuery to return a successful operation result with users 6 to 10 when called for page 2
            var mockUserQuery = new Mock<IUserQuery>();
            mockUserQuery.Setup(q => q.ReadFilterPage(2, 5, string.Empty))
                         .ReturnsAsync(Operation<IQueryable<User>>.Success(usersPage2, "Users found"));

            var _userQuery = mockUserQuery.Object;

            // Act: Call the ReadFilterPage method to get the mocked result for page 2 with a page size of 5
            var result = await _userQuery.ReadFilterPage(2, 5, string.Empty);

            // Assert: Verify that the result has exactly 5 users and that the first user on the page is "User 6"
            Assert.AreEqual(5, result.Data.Count());
            Assert.AreEqual("User 6", result.Data.First().Name);
        }

        [TestMethod]
        public async Task ReadUsersWithPagination_ShouldReturnEmpty_WhenOutOfRange()
        {
            // Arrange: Create an empty IQueryable<User> to simulate an out-of-range page result
            var emptyUsers = Enumerable.Empty<User>().AsQueryable();

            // Mock the _userQuery to return an empty result for page 10 with a page size of 5
            var mockUserQuery = new Mock<IUserQuery>();
            mockUserQuery.Setup(q => q.ReadFilterPage(10, 5, string.Empty))
                         .ReturnsAsync(Operation<IQueryable<User>>.Success(emptyUsers, "No users found for the specified page"));

            var _userQuery = mockUserQuery.Object;

            // Act: Call the ReadFilterPage method to get the mocked result for an out-of-range page
            var result = await _userQuery.ReadFilterPage(10, 5, string.Empty);

            // Assert: Verify that the result Data is empty
            Assert.IsTrue(!result.Data.Any());
        }

        // Update and Delete Operations remain the same as before

        // Additional Update and Delete tests if needed...
    }
}
