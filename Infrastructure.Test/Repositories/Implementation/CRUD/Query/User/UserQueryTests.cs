namespace Infrastructure.Test.Repositories.Implementation.CRUD.Query.User
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Application.Result;
    using Application.UseCases.CRUD.Query.User;
    using Domain.Entities;
    using Infrastructure.Repositories.Implementation.CRUD.Query.User;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    /// <summary>
    /// Unit tests for the UserQuery class, which is responsible for CRUD operations related to querying users.
    /// </summary>
    [TestClass]
    public class UserQueryTests
    {
        private UserQuery _testClass;
        private Mock<IUserReadFilter> _userReadFilter;
        private Mock<IUserReadFilterCount> _userReadFilterCount;
        private Mock<IUserReadFilterPage> _userReadFilterPage;
        private Mock<IUserReadId> _userReadId;
        /// <summary>
        /// Sets up the test environment before each test method is run. 
        /// Initializes mocks and the test class instance.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            _userReadFilter = new Mock<IUserReadFilter>();
            _userReadFilterCount = new Mock<IUserReadFilterCount>();
            _userReadFilterPage = new Mock<IUserReadFilterPage>();
            _userReadId = new Mock<IUserReadId>();
            _testClass = new UserQuery(_userReadFilter.Object, _userReadFilterCount.Object, _userReadFilterPage.Object, _userReadId.Object);
        }

        /// <summary>
        /// Tests that the UserQuery class can be constructed successfully.
        /// </summary>
        [TestMethod]
        public void Constructor_ShouldInitializeInstance()
        {
            // Act
            var instance = new UserQuery(_userReadFilter.Object, _userReadFilterCount.Object, _userReadFilterPage.Object, _userReadId.Object);
            // Assert
            Assert.IsNotNull(instance);
        }

        /// <summary>
        /// Tests the ReadFilter method to ensure it interacts with IUserReadFilter as expected.
        /// </summary>
        [TestMethod]
        public async Task ReadFilter_ShouldReturnExpectedResult_WhenCalled()
        {
            // Arrange
            Expression<Func<User, bool>> predicate = user => user.Active;
            var expectedReturnValue = Operation<IQueryable<User>>.Success(new Mock<IQueryable<User>>().Object, "Success");
            _userReadFilter.Setup(mock => mock.ReadFilter(predicate)).ReturnsAsync(expectedReturnValue);
            // Act
            var result = await _testClass.ReadFilter(predicate);
            // Assert
            _userReadFilter.Verify(mock => mock.ReadFilter(predicate), Times.Once);
            Assert.AreEqual(expectedReturnValue, result);
        }

        /// <summary>
        /// Tests the ReadFilterCount method to ensure it returns the correct count.
        /// </summary>
        [TestMethod]
        public async Task ReadFilterCount_ShouldReturnCorrectCount()
        {
            // Arrange
            var filter = "ActiveUsers";
            var expectedReturnValue = Operation<int>.Success(10, "Success");
            _userReadFilterCount.Setup(mock => mock.ReadFilterCount(filter)).ReturnsAsync(expectedReturnValue);
            // Act
            var result = await _testClass.ReadFilterCount(filter);
            // Assert
            _userReadFilterCount.Verify(mock => mock.ReadFilterCount(filter), Times.Once);
            Assert.AreEqual(expectedReturnValue, result);
        }

        /// <summary>
        /// Tests the ReadFilterPage method to ensure pagination works correctly.
        /// </summary>
        [TestMethod]
        public async Task ReadFilterPage_ShouldReturnCorrectPage_WhenCalled()
        {
            // Arrange
            var pageNumber = 1;
            var pageSize = 10;
            var filter = "ActiveUsers";
            var expectedReturnValue = Operation<IQueryable<User>>.Success(new Mock<IQueryable<User>>().Object, "Success");
            _userReadFilterPage.Setup(mock => mock.ReadFilterPage(pageNumber, pageSize, filter)).ReturnsAsync(expectedReturnValue);
            // Act
            var result = await _testClass.ReadFilterPage(pageNumber, pageSize, filter);
            // Assert
            _userReadFilterPage.Verify(mock => mock.ReadFilterPage(pageNumber, pageSize, filter), Times.Once);
            Assert.AreEqual(expectedReturnValue, result);
        }

        /// <summary>
        /// Tests the ReadId method to ensure it fetches the correct user by ID.
        /// </summary>
        [TestMethod]
        public async Task ReadId_ShouldReturnCorrectUser_WhenValidIdIsProvided()
        {
            // Arrange
            var id = "user123";
            var expectedUser = new User
            {
                Id = id,
                Name = "John Doe",
                Active = true
            };
            var expectedReturnValue = Operation<User>.Success(expectedUser, "Success");
            _userReadId.Setup(mock => mock.ReadId(id)).ReturnsAsync(expectedReturnValue);
            // Act
            var result = await _testClass.ReadId(id);
            // Assert
            _userReadId.Verify(mock => mock.ReadId(id), Times.Once);
            Assert.AreEqual(expectedReturnValue, result);
        }

        /// <summary>
        /// Tests the ReadByBearer method to ensure the correct user is retrieved with a valid bearer token.
        /// </summary>
        [TestMethod]
        public async Task ReadByBearer_ShouldReturnCorrectUser_WhenValidTokenIsProvided()
        {
            // Arrange
            var bearerToken = "BearerToken123";
            var expectedUser = new User
            {
                Id = "user123",
                Name = "John Doe",
                Active = true
            };
            var expectedReturnValue = Operation<User>.Success(expectedUser, "Success");
            _userReadId.Setup(mock => mock.ReadByBearer(bearerToken)).ReturnsAsync(expectedReturnValue);
            // Act
            var result = await _testClass.ReadByBearer(bearerToken);
            // Assert
            _userReadId.Verify(mock => mock.ReadByBearer(bearerToken), Times.Once);
            Assert.AreEqual(expectedReturnValue, result);
        }
    }
}