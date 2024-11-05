using Application.UseCases.CRUD.User;
using Application.UseCases.ExternalServices;
using Infrastructure.Repositories.Implementation.CRUD.User;
using Infrastructure.Repositories.Implementation.Status;
using Microsoft.EntityFrameworkCore;
using Moq;
using Persistence.BaseDbContext;
using Persistence.CreateStruture.Constants.ColumnType;

namespace Infrastructure.Test.Repositories.Implementation.CRUD
{
    [TestClass]
    public class TestsBase
    {
        protected DbContextOptions<CommonDbContext> _options;
        protected CommonDbContext _dbContext;
        protected Mock<ILogService> _logService;
        protected IUserCreate _userCreate;
        protected IUserUpdate _userUpdate;
        protected IUserDelete _userDelete;
        protected UserStatus _userStatus;

        [TestInitialize]
        public void SetUp()
        {
            _options = new DbContextOptionsBuilder<CommonDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Use unique DB for each test
                .EnableSensitiveDataLogging(true)
                .Options;
            IColumnTypes _columnTypes = new ColumnTypesPosgresql();
            _dbContext = new CommonDbContext(_options, _columnTypes);
            _logService = new Mock<ILogService>();
            _userCreate = new UserCreate(_dbContext, _logService.Object);
            _userDelete = new UserDelete(_dbContext, _logService.Object);
            _userUpdate = new UserUpdate(_dbContext, _logService.Object);
            _userStatus = new UserStatus(_dbContext, _logService.Object);
        }
    }
}