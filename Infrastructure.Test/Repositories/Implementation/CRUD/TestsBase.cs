using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.UseCases.CRUD.Query.User;
using Application.UseCases.CRUD.User;
using Application.UseCases.ExternalServices;
using Application.UseCases.Repository.CRUD;
using Application.UseCases.Repository;
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
using Application.Result;
using Domain.Entities;

namespace Infrastructure.Test.Repositories.Implementation.CRUD
{
    [TestClass]
    public class ResourceProviderTests
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
        protected IUtilEntity<Domain.Entities.User> _utilEntity;

        private readonly Dictionary<string, string> _resourceMessages = new()
        {
            { "EntityFailedNecesaryData", "Necessary data was not provided." },
            { "FailedGetToken", "The log services token got failed." },
            { "FailedNecesaryData", "Necessary data was not provided." },
            { "FailedSetLog", "The log creation got failed." },
            { "FailureConfigurationMissingError", "The configuration for the log services is missing the username, password, or URL." },
            { "GenericExistValidation", "The {0} does not exist." },
            { "LogSuccessfullyGenericActiveated", "{0} was activated successfully." },
            { "StatusFailedNecesaryData", "Necessary data was not provided." },
            { "StatusGlobalOkMessage", "Ok" },
            { "SuccessfullyGeneric", "{0} was created successfully." },
            { "SuccessfullyGenericActiveated", "{0} was activated successfully." },
            { "SuccessfullyGenericDeleted", "{0} was deleted successfully." },
            { "SuccessfullyGetToken", "Token got successfully." },
            { "SuccessfullySetLog", "The successfully SetLog." },
            { "ValidationGlobalOkMessage", "Operation completed successfully." },
            { "UtilGlobalOkMessage", "Ok" },
        };

        [TestInitialize]
        public void SetUp()
        {
            _options = new DbContextOptionsBuilder<CommonDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

            var columnTypes = new ColumnTypesPosgresql();
            _dbContext = new CommonDbContext(_options, columnTypes);
            _logService = new Mock<ILogService>();
            _resourceHandler = SetupResourceHandlerMock();
            _resourceProvider = SetupResourceProviderMock();
            _utilEntity = new UtilEntity<Domain.Entities.User>(_resourceProvider, _resourceHandler);
            _userCreate = new UserCreate(_dbContext, _logService.Object, _utilEntity, _resourceProvider, _resourceHandler);
            _userDelete = new UserDelete(_dbContext, _logService.Object, _resourceProvider, _resourceHandler);
            _userUpdate = new UserUpdate(_dbContext, _logService.Object, _utilEntity, _resourceProvider, _resourceHandler);
            _userStatus = new UserStatus(_dbContext, _logService.Object, _resourceProvider, _resourceHandler);
            _userReadFilter = new UserReadFilter(_dbContext, _logService.Object, _resourceProvider, _resourceHandler);
            _userReadFilterCount = new UserReadFilterCount(_dbContext, _logService.Object, _resourceProvider, _resourceHandler);
        }

        [TestMethod]
        public async Task AllResourceKeysExistInResourceProvider()
        {
            // Arrange
            var instance = new ResxResourceProvider();
            var entriesResult = await instance.GetResourceEntries();

            // Assert early if entries result is null or contains no data
            Assert.IsNotNull(entriesResult?.Data, "Resource entries are null or empty.");

            var entries = entriesResult.Data.ToList(); // Cache to avoid multiple enumerations

            // Act
            var missingKeys = _resourceMessages.Keys.Where(key => !ResourceExists(entries, key)).ToList();

            // Assert

            //Please ensure all items are added to the appropriate resource file.

            var missingResorces = string.Join(", ", missingKeys);
            Assert.IsTrue(missingKeys.Count == 0, $"Missing resource keys: {missingResorces}");
        }

        private static bool ResourceExists(IEnumerable<ResourceEntry> entries, string key)
        {
            return entries.Any(entry => entry.Name.Contains(key));
        }


        private static bool ExistResorce(OperationResult<IQueryable<ResourceEntry>>? entries, string key)
        {
            return !(entries is not null && entries.Data is not null && entries.Data.Where(r => r.Name == key).Any());
        }

        private IResourceHandler SetupResourceHandlerMock()
        {
            var mockResourceHandler = new Mock<IResourceHandler>();

            foreach (var resource in _resourceMessages)
            {
                mockResourceHandler
                    .Setup(rh => rh.GetResource(resource.Key))
                    .Returns(resource.Value);
            }

            return mockResourceHandler.Object;
        }

        private IResourceProvider SetupResourceProviderMock()
        {
            var mockResourceProvider = new Mock<IResourceProvider>();

            foreach (var resource in _resourceMessages)
            {
                mockResourceProvider
                    .Setup(rp => rp.GetMessageValueOrDefault(resource.Key, It.IsAny<string>()))
                    .ReturnsAsync(resource.Value);
            }

            return mockResourceProvider.Object;
        }
    }
}
