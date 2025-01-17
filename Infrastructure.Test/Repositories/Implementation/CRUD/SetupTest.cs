namespace Infrastructure.Test.Repositories.Implementation.CRUD
{
    using Application.UseCases.CRUD.Query.User;
    using Application.UseCases.CRUD.User;
    using Application.UseCases.ExternalServices;
    using Application.UseCases.Repository.CRUD;
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
    using Application.UseCases.ExternalServices.Resources;
    using Infrastructure.Repositories.Implementation.CRUD.Query.Resource;
    using Infrastructure.Resource;
    using System.Diagnostics;
    using Application.UseCases.CRUD.Query.Resource;
    using System.Resources;
    using Application.UseCases.ExternalServices.Resources.Provider;
    using Application.UseCases.Repository.CRUD.Query;

    /// <summary>
    /// Class to set up the test environment for CRUD operations and related services.
    /// </summary>
    [TestClass]
    public class SetupTest
    {
        // Test configuration fields
        protected DbContextOptions<DataContext> _options;
        protected DataContext _dbContext;
        protected Mock<ILogService> _logService;
        protected IUserReadFilter _userReadFilter;
        protected IUserCreate _userCreate;
        protected IUserUpdate _userUpdate;
        protected IUserDelete _userDelete;
        protected IUserStatus _userStatus;
        protected IUserReadFilterCount _userReadFilterCount;
        protected IResourcesProvider _provider;
        protected IResourceHandler _handler;
        protected IUtilEntity<Domain.Entities.User> _utilEntityUser;
        protected IUtilEntity<Domain.Entities.Resource> _utilEntityResource;
        protected Mock<IResourceReadFilter> _resourceReadFilter;
        protected Mock<IResourceReadFilterCount> _resourceReadFilterCount;
        protected Mock<IResourceReadFilterPage> _resourceReadFilterPage;
        protected Mock<IResourceReadId> _resourceReadId;
        protected IResourceQuery _resourceEntryQuery;
        protected IUserQuery _userQuery;
        protected IReadFilter<Domain.Entities.User> _readFilterUser;
        protected IReadFilter<Domain.Entities.Resource> _readFilterResource;

        protected readonly Dictionary<string, string> _resourceMessages = new()
        {
            {
                "UtilEntityFailedNecesaryData",
                "Necessary data was not provided."
            },
            {
                "LogTokenFetchFailed",
                "The log services token got failed."
            },
            {
                "EntityCheckerFailedNecesaryData",
                "Necessary data was not provided."
            },
            {
                "LogCreationFailed",
                "The log creation got failed."
            },
            {
                "LogConfigMissingError",
                "The configuration for the log services is missing the username, password, or URL."
            },
            {
                "EntityCheckerValidation",
                "The {0} does not exist."
            },
            {
                "StatusExistValidation",
                "The {0} does not exist."
            },
            {
                "LogActivationSuccess",
                "{0} was activated successfully."
            },
            {
                "StatusDisableSuccess",
                "{0} was disabled successfully."
            },
            {
                "StatusFailedNecesaryData",
                "Necessary data was not provided."
            },
            {
                "StatusSuccess",
                "Ok"
            },
            {
                "CreationSuccess",
                "{0} was created successfully."
            },
            {
                "StatusActiveSuccess",
                "{0} was activated successfully."
            },
            {
                "DeletionSuccess",
                "{0} was deleted successfully."
            },
            {
                "LogTokenFetched",
                "Token got successfully."
            },
            {
                "LogCreated",
                "The successfully SetLog."
            },
            {
                "EntityCheckerSuccess",
                "Operation completed successfully."
            },
            {
                "UtilEntitySuccess",
                "Ok"
            },
            {
                "CreateFailedDataSizeCharacter",
                "One or more data from the User have been submitted with errors {0}"
            },
            {
                "CreateFailedEmailInvalidFormat",
                "The given email is not in a valid format"
            },
            {
                "CreateFailedAlreadyRegisteredEmail",
                "A user is already registered with this email."
            },
            {
                "ResourceFailedDataLength",
                "One or more data from the User have been submitted with errors {0}"
            },
            {
                "ResourceFailedDuplicateName",
                "The given name already exists."
            },
            {
                "UpdateFailedDataLength",
                "One or more data from the User have been submitted with errors {0}"
            },
            {
                "UpdateFailedDuplicateName",
                "The given name already exists."
            },
            {
                "UpdateSearchSuccess",
                "The resource was found successfully."
            },
            {
                "UpdateSuccess",
                "{0} was updated successfully."
            },
            {
                "UpdateFailedDataSizeCharacter",
                "One or more data from the User have been submitted with errors {0}"
            },
            {
                "UpdateFailedEmailInvalidFormat",
                "The given email is not in a valid format"
            },
            {
                "UpdateFailedAlreadyRegisteredEmail",
                "A user is already registered with this email."
            },
            {
                "UpdateSuccessfullySearchGeneric",
                "The search in the {0} entity completed successfully."
            },
            {
                "UpdateEntitySearchSuccess",
                "The search in the {0} entity completed successfully."
            },
            {
                "ReadByBearerSuccess",
                "The search in the {0} entity completed successfully."
            },
            {
                "ReadIdSuccess",
                "The entity was found by id successfully."
            },
            {
                "ReadFilterSuccess",
                "The search in the {0} entity completed successfully."
            },
            {
                "ReadFilterPageSuccess",
                "The search in the {0} entity completed successfully."
            },
            {
                "ReadFilterCountSuccess",
                "The search in the {0} entity completed successfully."
            },
            {
                "ImageSuccessfullyUpload",
                "The image was uploaded successfully due to an unexpected error."
            },
            {
                "ImageConvertSuccess",
                "Ok."
            },
            {
                "CompressedSuccess",
                "The image was compressed successfully."
            }
        };
        /// <summary>
        /// Initialize the test data and services needed for the tests.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            // Setup in-memory database options and context
            _options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

            var columnTypes = new ColumnTypesPosgresql();
            _dbContext = new DataContext(_options, columnTypes);

            // Initialize mocks and services
            _logService = new Mock<ILogService>();
            _handler = SetupResourceHandlerMock();
            _provider = SetupResourceProviderMock();
            _readFilterUser = new UserReadFilter(_dbContext,_logService.Object, _provider, _handler);
            _readFilterResource = new ResourceReadFilter(_dbContext, _logService.Object, _provider, _handler);
            _provider = new ResxProvider();
            _utilEntityUser = new UtilEntity<Domain.Entities.User>(_provider, _handler, _readFilterUser);
            _utilEntityResource = new UtilEntity<Domain.Entities.Resource>(_provider, _handler, _readFilterResource);
            _resourceReadFilter = new Mock<IResourceReadFilter>();
            _resourceReadFilterCount = new Mock<IResourceReadFilterCount>();
            _resourceReadFilterPage = new Mock<IResourceReadFilterPage>();
            _resourceReadId = new Mock<IResourceReadId>();

        // Initialize the User-related services with concrete instances
        _resourceEntryQuery = new ResourceQuery(_resourceReadFilter.Object,
            _resourceReadFilterCount.Object,
            _resourceReadFilterPage.Object,
            _resourceReadId.Object
            );
            _userCreate = new UserCreate(_dbContext, _logService.Object, _utilEntityUser, _provider, _handler);
            _userDelete = new UserDelete(_dbContext, _logService.Object, _provider, _handler);
            _userUpdate = new UserUpdate(_dbContext, _logService.Object, _utilEntityUser, _provider, _handler);
            _userStatus = new UserStatus(_dbContext, _logService.Object, _provider, _handler);



        _userReadFilter = new UserReadFilter(_dbContext, _logService.Object, _provider, _handler);
            _userReadFilterCount = new UserReadFilterCount(_dbContext, _logService.Object, _provider, _handler);

            // Mocking IUserQuery as it combines all read functionalities
            var mockUserQuery = new Mock<IUserQuery>();
            _userQuery = mockUserQuery.Object;

            // Debugging output to verify initialization
            Debug.WriteLine($"_dbContext initialized: {_dbContext != null}");
            Debug.WriteLine($"_userCreate initialized: {_userCreate != null}");
            Debug.WriteLine($"_userUpdate initialized: {_userUpdate != null}");
            Debug.WriteLine($"_userDelete initialized: {_userDelete != null}");
            Debug.WriteLine($"_userReadFilter initialized: {_userReadFilter != null}");
        }

        private IResourceHandler SetupResourceHandlerMock()
        {
            var mockResourceHandler = new Mock<IResourceHandler>();
            foreach (var resource in _resourceMessages)
            {
                mockResourceHandler.Setup(rh => rh.GetResource(resource.Key)).Returns(resource.Value);
            }

            return mockResourceHandler.Object;
        }

        private IResourcesProvider SetupResourceProviderMock()
        {
            var mockResourceProvider = new Mock<IResourcesProvider>();
            foreach (var resource in _resourceMessages)
            {
                mockResourceProvider.Setup(rp => rp.GetMessageValueOrDefault(resource.Key, It.IsAny<string>())).ReturnsAsync(resource.Value);
            }

            return mockResourceProvider.Object;
        }
    }
}