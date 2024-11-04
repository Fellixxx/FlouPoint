namespace Infrastructure.Test.Repositories.Implementation.CRUD.User
{
    using System;
    using System.Threading.Tasks;
    using Application.Result;
    using Application.UseCases.CRUD.User;
    using Application.UseCases.ExternalServices;
    using Domain.Entities;
    using Infrastructure.Repositories.Implementation.CRUD.User;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using MySqlX.XDevAPI.Common;
    using Persistence.BaseDbContext;
    using Persistence.CreateStruture.Constants.ColumnType;
    using TContext = System.String;

    [TestClass]
    public class UserCreateTests
    {
        private DbContextOptions<CommonDbContext> _options;
        private CommonDbContext _dbContext;
        private Mock<ILogService> _logService;
        private IUserCreate _userCreate;

        [TestInitialize]
        public void SetUp()
        {
            _options = new DbContextOptionsBuilder<CommonDbContext>()
                .UseInMemoryDatabase(databaseName: "testdb")
                .EnableSensitiveDataLogging(true)
                .Options;
            IColumnTypes _columnTypes = new ColumnTypesPosgresql();
            _dbContext = new CommonDbContext(_options, _columnTypes);
            _logService = new Mock<ILogService>();
            _userCreate = new UserCreate(_dbContext,_logService.Object);
        }

        [TestMethod]
        public void CanConstruct()
        {
            // Act
            var instance = new UserCreate(_dbContext, _logService.Object);

            // Assert
            Assert.IsNotNull(instance);
        }

        [TestMethod]
        public void CannotConstructWithNullContext()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new UserCreate(default(CommonDbContext), _logService.Object));
        }

        [TestMethod]
        public void CanConstructWithNullLogService()
        {
            var userCreate = new UserCreate(_dbContext, default(ILogService));
            Assert.IsNotNull(userCreate);
        }


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
            Assert.AreEqual(result.Message, "User was created successfully.");
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual(id, result.Data);
        }

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
            Assert.AreEqual(result.Message,"The given email is not in a valid format");
            Assert.IsFalse(result.IsSuccessful);
            Assert.IsNull(result.Data);
        }

        [TestMethod]
        public async Task When_CreateEntity_WithAlreadyRegisteredUsername_ShouldReturnFailure()
        {
            // Given
            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Name = "ExistingUsername",
                Email = "UniqueEmail@gmail.com",
                Password = "ValidPassword",
            };
            var result1 = await _userCreate.Create(user);
            var user2 = new User
            {
                Id = Guid.NewGuid().ToString(),
                Name = "ExistingUsername",
                Email = "UniqueEmail@gmail.com",
                Password = "ValidPassword",
            };

            // When
            var result = await _userCreate.Create(user2);

            // Then
            Assert.AreEqual(result.Message,"A user is already registered with this email.");
            Assert.IsFalse(result.IsSuccessful);
            Assert.IsNull(result.Data);
        }

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
            var result = await _userCreate.Create(user);
            result = await _userCreate.Create(user);

            // Then
            Assert.AreEqual(result.Message,"A user is already registered with this email.");
            Assert.IsFalse(result.IsSuccessful);
            Assert.IsNull(result.Data);
        }
    }
}