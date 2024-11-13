﻿using Application.UseCases.CRUD.Query.User;
using Application.UseCases.CRUD.User;
using Application.UseCases.ExternalServices;
using Application.UseCases.Repository.CRUD;
using Application.UseCases.Repository;
using Application.UseCases.Repository.Status.Status;
using Infrastructure.Repositories.Implementation.CRUD.Query.User;
using Infrastructure.Repositories.Implementation.CRUD.User;
using Infrastructure.Repositories.Implementation.Status;
using Microsoft.EntityFrameworkCore;
using Moq;
using Persistence.BaseDbContext;
using Persistence.CreateStruture.Constants.ColumnType;
using Infrastructure.Repositories.Abstract.CRUD.Util;
using Infrastructure.Repositories.Implementation.CRUD.User.Create;
using Infrastructure.Repositories.Implementation.CRUD.User.Update;
using Application.UseCases.ExternalServices.Resorces;


namespace Infrastructure.Test.Repositories.Implementation.CRUD
{
    [TestClass]
    public class SetupTest
    {
        protected DbContextOptions<DataContext> _options;
        protected DataContext _dbContext;
        protected Mock<ILogService> _logService;
        protected IUserReadFilter _userReadFilter;
        protected IUserCreate _userCreate;
        protected IUserUpdate _userUpdate;
        protected IUserDelete _userDelete;
        protected IUserStatus _userStatus;
        protected IUserReadFilterCount _userReadFilterCount;
        protected IResorcesProvider _resourceProvider;
        protected IResourceHandler _resourceHandler;
        protected IUtilEntity<Domain.Entities.User> _utilEntity;

        protected readonly Dictionary<string, string> _resourceMessages = new()
        {
            { "EntityFailedNecesaryData", "Necessary data was not provided." },
            { "FailedGetToken", "The log services token got failed." },
            { "FailedNecesaryData", "Necessary data was not provided." },
            { "FailedSetLog", "The log creation got failed." },
            { "FailureConfigurationMissingError", "The configuration for the log services is missing the username, password, or URL." },
            { "GenericExistValidation", "The {0} does not exist." },
            { "LogSuccessfullyGenericActiveated", "{0} was activated successfully." },
            { "StatusSuccessfullyGenericDisabled", "{0} was disabled successfully." },
            { "StatusFailedNecesaryData", "Necessary data was not provided." },
            { "StatusGlobalOkMessage", "Ok" },
            { "SuccessfullyGeneric", "{0} was created successfully." },
            { "SuccessfullyGenericActiveated", "{0} was activated successfully." },
            { "SuccessfullyGenericDeleted", "{0} was deleted successfully." },
            { "SuccessfullyGetToken", "Token got successfully." },
            { "SuccessfullySetLog", "The successfully SetLog." },
            { "ValidationGlobalOkMessage", "Operation completed successfully." },
            { "UtilGlobalOkMessage", "Ok" },
            { "FailedDataSizeCharacter", "One or more data from the User have been submitted with errors {0}" },
            { "FailedEmailInvalidFormat", "The given email is not in a valid format" },
            { "FailedAlreadyRegisteredEmail", "A user is already registered with this email." },
            { "CreateFailedDataSizeCharacter", "One or more data from the User have been submitted with errors {0}" },
            { "CreateFailedEmailInvalidFormat", "The given email is not in a valid format" },
            { "CreateFailedAlreadyRegisteredEmail", "A user is already registered with this email." },

            { "SuccessfullyGenericUpdated", "{0} was updated successfully." },

            { "UpdateFailedDataSizeCharacter", "One or more data from the User have been submitted with errors {0}" },
            { "UpdateFailedEmailInvalidFormat", "The given email is not in a valid format" },
            { "UpdateFailedAlreadyRegisteredEmail", "A user is already registered with this email." },
            { "UpdateSuccessfullySearchGeneric", "The search in the {0} entity completed successfully." },
            { "ImageSuccessfullyUpload", "The image was uploaded successfully due to an unexpected error." },
            { "ImageGlobalOkMessage", "Ok." },
            { "SuccessCompressed", "The image was compressed successfully." }
        };

        [TestInitialize]
        public void SetUp()
        {
            _options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

            var columnTypes = new ColumnTypesPosgresql();
            _dbContext = new DataContext(_options, columnTypes);
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

        private IResorcesProvider SetupResourceProviderMock()
        {
            var mockResourceProvider = new Mock<IResorcesProvider>();

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
