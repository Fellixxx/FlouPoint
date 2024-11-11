using Application.UseCases.CRUD.Query.User;
using Application.UseCases.CRUD.User;
using Application.UseCases.ExternalServices;
using Application.UseCases.Repository;
using Application.UseCases.Repository.CRUD;
using Application.UseCases.Repository.Status.Status;
using Infrastructure.Message;
using Infrastructure.Repositories.Abstract.CRUD;
using Infrastructure.Repositories.Implementation.CRUD.Query.User;
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
        protected IUserReadFilter _userReadFilter;
        protected IUserCreate _userCreate;
        protected IUserUpdate _userUpdate;
        protected IUserDelete _userDelete;
        protected IUserStatus _userStatus;
        protected IUserReadFilterCount _userReadFilterCount;
        protected Mock<IResourceProvider> _resourceProvider;
        protected IResourceHandler _resourceHandler;
        protected List<string> _resourceKeys;
        protected Mock<IUtilEntity<Domain.Entities.User>> _utilEntity;

        [TestInitialize]
        public void SetUp()
        {
            _options = new DbContextOptionsBuilder<CommonDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging(true)
                .Options;
            IColumnTypes _columnTypes = new ColumnTypesPosgresql();
            _dbContext = new CommonDbContext(_options, _columnTypes);
            _logService = new Mock<ILogService>();
            _resourceProvider = new Mock<IResourceProvider>();
            _utilEntity = new Mock<IUtilEntity<Domain.Entities.User>>();

            _userCreate = new UserCreate(_dbContext, _logService.Object, _utilEntity.Object);
            _userDelete = new UserDelete(_dbContext, _logService.Object, _resourceProvider.Object);
            _userUpdate = new UserUpdate(_dbContext, _logService.Object, _utilEntity.Object, _resourceProvider.Object);
            _userStatus = new UserStatus(_dbContext, _logService.Object, _resourceProvider.Object);
            
            _userReadFilter = new UserReadFilter(_dbContext, _logService.Object, _resourceProvider.Object);
            _userReadFilterCount = new UserReadFilterCount(_dbContext, _logService.Object, _resourceProvider.Object);
        }
    }
}