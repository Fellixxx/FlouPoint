using Application.UseCases.CRUD.Query.User;
using Application.UseCases.CRUD.User;
using Application.UseCases.ExternalServices;
using Application.UseCases.Repository;
using Application.UseCases.Repository.CRUD;
using Application.UseCases.Repository.Status.Status;
using Infrastructure.Message;
using Infrastructure.Repositories;
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
        protected IResourceProvider _resourceProvider;
        protected IResourceHandler _resourceHandler;
        protected List<string> _resourceKeys;
        protected IUtilEntity<Domain.Entities.User> _utilEntity;

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
            var mockResourceHandler = new Mock<IResourceHandler>();
            mockResourceHandler
                .Setup(rh => rh.GetResource("EntityFailedNecesaryData"))
                .Returns("Necessary data was not provided.");
            mockResourceHandler
                .Setup(rh => rh.GetResource("StatusFailedNecesaryData"))
                .Returns("Necessary data was not provided.");
            mockResourceHandler
               .Setup(rh => rh.GetResource("FailedNecesaryData"))
               .Returns("Necessary data was not provided.");
            mockResourceHandler
               .Setup(rh => rh.GetResource("ValidationGlobalOkMessage"))
               .Returns("Operation completed successfully.");
            mockResourceHandler
               .Setup(rh => rh.GetResource("GenericExistValidation"))
               .Returns("The {0} does not exist.");
            mockResourceHandler
               .Setup(rh => rh.GetResource("SuccessfullyGenericActiveated"))
               .Returns("{0} was activated successfully.");
            mockResourceHandler
               .Setup(rh => rh.GetResource("StatusFailedNecesaryData"))
               .Returns("Necessary data was not provided.");
            mockResourceHandler
               .Setup(rh => rh.GetResource("StatusGlobalOkMessage"))
               .Returns("Necessary data was not provided.");
            var mockResourceProvider = new Mock<IResourceProvider>();
            mockResourceProvider
                .Setup(rp => rp.GetMessageValueOrDefault("EntityFailedNecesaryData", It.IsAny<string>()))
                .Returns(Task.FromResult("Necessary data was not provided."));
            mockResourceProvider
                .Setup(rp => rp.GetMessageValueOrDefault("StatusFailedNecesaryData", It.IsAny<string>()))
                .Returns(Task.FromResult("Necessary data was not provided."));
            mockResourceProvider
                .Setup(rp => rp.GetMessageValueOrDefault("FailedNecesaryData", It.IsAny<string>()))
                .Returns(Task.FromResult("Necessary data was not provided."));
            mockResourceProvider
                .Setup(rp => rp.GetMessageValueOrDefault("ValidationGlobalOkMessage", It.IsAny<string>()))
                .Returns(Task.FromResult("Operation completed successfully."));
            mockResourceProvider
                .Setup(rp => rp.GetMessageValueOrDefault("GenericExistValidation", It.IsAny<string>()))
                .Returns(Task.FromResult("The {0} does not exist."));
            mockResourceProvider
                .Setup(rp => rp.GetMessageValueOrDefault("SuccessfullyGenericActiveated", It.IsAny<string>()))
                .Returns(Task.FromResult("{0} was activated successfully."));
            mockResourceProvider
                .Setup(rp => rp.GetMessageValueOrDefault("StatusFailedNecesaryData", It.IsAny<string>()))
                .Returns(Task.FromResult("Necessary data was not provided."));
            mockResourceProvider
                .Setup(rp => rp.GetMessageValueOrDefault("StatusGlobalOkMessage", It.IsAny<string>()))
                .Returns(Task.FromResult("Ok"));

            _resourceHandler = mockResourceHandler.Object;

            _resourceProvider = mockResourceProvider.Object;
            _utilEntity = new UtilEntity<Domain.Entities.User>(_resourceProvider, _resourceHandler);
            _userCreate = new UserCreate(_dbContext, _logService.Object, _utilEntity);
            _userDelete = new UserDelete(_dbContext, _logService.Object, _resourceProvider, _resourceHandler);
            _userUpdate = new UserUpdate(_dbContext, _logService.Object, _utilEntity, _resourceProvider, _resourceHandler);
            _userStatus = new UserStatus(_dbContext, _logService.Object, _resourceProvider, _resourceHandler);

            _userReadFilter = new UserReadFilter(_dbContext, _logService.Object, _resourceProvider);
            _userReadFilterCount = new UserReadFilterCount(_dbContext, _logService.Object, _resourceProvider);
        }
    }
}