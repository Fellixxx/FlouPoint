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

    [TestClass]
    public class UserQueryTests
    {
        private UserQuery _testClass;
        private Mock<IUserReadFilter> _userReadFilter;
        private Mock<IUserReadFilterCount> _userReadFilterCount;
        private Mock<IUserReadFilterPage> _userReadFilterPage;
        private Mock<IUserReadId> _userReadId;

        [TestInitialize]
        public void SetUp()
        {
            _userReadFilter = new Mock<IUserReadFilter>();
            _userReadFilterCount = new Mock<IUserReadFilterCount>();
            _userReadFilterPage = new Mock<IUserReadFilterPage>();
            _userReadId = new Mock<IUserReadId>();
            _testClass = new UserQuery(_userReadFilter.Object, _userReadFilterCount.Object, _userReadFilterPage.Object, _userReadId.Object);
        }

        [TestMethod]
        public void CanConstruct()
        {
            // Act
            var instance = new UserQuery(_userReadFilter.Object, _userReadFilterCount.Object, _userReadFilterPage.Object, _userReadId.Object);

            // Assert
            Assert.IsNotNull(instance);
        }

        [TestMethod]
        public async Task CanCallReadFilter()
        {
            // Arrange
            Expression<Func<User, bool>> predicate = user => user.Active;

            var expectedReturnValue = OperationResult<IQueryable<User>>.Success(new Mock<IQueryable<User>>().Object, "Success");
            _userReadFilter.Setup(mock => mock.ReadFilter(predicate)).ReturnsAsync(expectedReturnValue);

            // Act
            var result = await _testClass.ReadFilter(predicate);

            // Assert
            _userReadFilter.Verify(mock => mock.ReadFilter(predicate), Times.Once);
            Assert.AreEqual(expectedReturnValue, result);
        }

        [TestMethod]
        public async Task CanCallReadFilterCount()
        {
            // Arrange
            var filter = "ActiveUsers";

            var expectedReturnValue = OperationResult<int>.Success(10, "Success");
            _userReadFilterCount.Setup(mock => mock.ReadFilterCount(filter)).ReturnsAsync(expectedReturnValue);

            // Act
            var result = await _testClass.ReadFilterCount(filter);

            // Assert
            _userReadFilterCount.Verify(mock => mock.ReadFilterCount(filter), Times.Once);
            Assert.AreEqual(expectedReturnValue, result);
        }

        [TestMethod]
        public async Task CanCallReadFilterPage()
        {
            // Arrange
            var pageNumber = 1;
            var pageSize = 10;
            var filter = "ActiveUsers";

            var expectedReturnValue = OperationResult<IQueryable<User>>.Success(new Mock<IQueryable<User>>().Object, "Success");
            _userReadFilterPage.Setup(mock => mock.ReadFilterPage(pageNumber, pageSize, filter)).ReturnsAsync(expectedReturnValue);

            // Act
            var result = await _testClass.ReadFilterPage(pageNumber, pageSize, filter);

            // Assert
            _userReadFilterPage.Verify(mock => mock.ReadFilterPage(pageNumber, pageSize, filter), Times.Once);
            Assert.AreEqual(expectedReturnValue, result);
        }

        [TestMethod]
        public async Task CanCallReadId()
        {
            // Arrange
            var id = "user123";

            var expectedUser = new User
            {
                Id = id,
                Name = "John Doe",
                Active = true
            };
            var expectedReturnValue = OperationResult<User>.Success(expectedUser, "Success");
            _userReadId.Setup(mock => mock.ReadId(id)).ReturnsAsync(expectedReturnValue);

            // Act
            var result = await _testClass.ReadId(id);

            // Assert
            _userReadId.Verify(mock => mock.ReadId(id), Times.Once);
            Assert.AreEqual(expectedReturnValue, result);
        }

        [TestMethod]
        public async Task CanCallReadByBearer()
        {
            // Arrange
            var bearerToken = "BearerToken123";

            var expectedUser = new User
            {
                Id = "user123",
                Name = "John Doe",
                Active = true
            };
            var expectedReturnValue = OperationResult<User>.Success(expectedUser, "Success");
            _userReadId.Setup(mock => mock.ReadByBearer(bearerToken)).ReturnsAsync(expectedReturnValue);

            // Act
            var result = await _testClass.ReadByBearer(bearerToken);

            // Assert
            _userReadId.Verify(mock => mock.ReadByBearer(bearerToken), Times.Once);
            Assert.AreEqual(expectedReturnValue, result);
        }
    }
}