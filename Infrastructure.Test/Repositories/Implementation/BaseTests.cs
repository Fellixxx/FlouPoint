namespace Infrastructure.Test.Repositories.Implementation
{
    using Application.UseCases.CRUD.Query.User;
    using Application.UseCases.CRUD.User;
    using Application.UseCases.CRUD.Validation;
    using Application.UseCases.ExternalServices;
    using Application.UseCases.Operations;
    using Application.UseCases.Repository.CRUD.Resource;
    using Application.UseCases.Repository.Status.StatusChange;
    using Domain.Entities;
    using Infrastructure.Resource;
    using Infrastructure.Repositories.Implementation.CRUD.Query;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Configuration;
    using Moq;
    using Persistence.BaseDbContext;
    using Persistence.BaseDbContext.Interface;
    using Persistence.CreateStruture.Constants.ColumnType;
    using Persistence.GenerateData.Interface;
    using System.Security.Cryptography;
    using System.Text;
    using Application.UseCases.ExternalServices.Resources;

    /// <summary>
    /// BaseTests provides a foundational setup for testing repository implementations.
    /// It includes initialization of mock services, configurations, and setup for the DataContext.
    /// </summary>
    [TestClass]
    public class BaseTests
    {
        protected DataContext _dbContext;
        protected Mock<IDataContext> _dbContextMock;
        protected DbContextOptions<DataContext> _options;
        protected Mock<ILogService> _logService;
        protected Mock<IConfiguration> _configuration;
        protected Mock<IConfigurationSection> _configurationSection;
        protected Mock<IOtpValidate> _otpValidate;
        protected Mock<IOtpGenerate> _otpGenerate;
        protected Mock<IConfigurationSection> _configSectionUsername;
        protected Mock<IConfigurationSection> _configSectionPassword;
        protected Mock<IConfigurationSection> _configSectionEmail;
        protected Mock<IConfigurationSection> _configSectionMasive;
        protected IDataSeeder _dataSeeder;
        protected IUserCreate _userCreate;
        protected IUserDelete _userDelete;
        protected IUserUpdate _userUpdate;
        protected IStatus _userStatus;
        protected IUserChecker _userExistenceValidator;
        protected Mock<IDistributedCache> _distributedCacheMock;
        protected IUserReadFilterPage _userReadFilterPage;
        protected IUserReadId _userReadId;
        protected IResourcesProvider _resxResourceProvider;
        protected IResourcesProvider _databaseResourceProvider;
        protected IQuery _resourceEntryQuery;
        /// <summary>
        /// Initializes the test context by setting up mocks, configurations, in-memory database, and resource providers.
        /// </summary>
        [TestInitialize]
        public virtual void Setup()
        {
            _logService = new Mock<ILogService>();
            _configuration = new Mock<IConfiguration>();
            _otpValidate = new Mock<IOtpValidate>();
            _otpGenerate = new Mock<IOtpGenerate>();
            _configurationSection = new Mock<IConfigurationSection>();
            _configurationSection.SetupGet(m => m.Value).Returns("ssnDBVccFUhVvPWQPh7LssnDBVccFUhVvPWQPh7L");
            _configuration.Setup(config => config.GetSection(It.IsAny<string>())).Returns(_configurationSection.Object);
            _options = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase(databaseName: "testdb").EnableSensitiveDataLogging(true).Options;
            _configSectionUsername = new Mock<IConfigurationSection>();
            _configSectionPassword = new Mock<IConfigurationSection>();
            _configSectionEmail = new Mock<IConfigurationSection>();
            _configSectionMasive = new Mock<IConfigurationSection>();
            _configSectionUsername.Setup(x => x.Value).Returns("usernameanonymous");
            _configSectionPassword.Setup(x => x.Value).Returns("72b28030ce99fa4d0ab678f1d4a4374cc0d7bb676eb4307b0fa105f4c66b644e");
            _configSectionEmail.Setup(x => x.Value).Returns("user.anonymous@withoutemail.com");
            _configSectionMasive.Setup(x => x.Value).Returns("false");
            _configuration.Setup(section => section.GetSection("anonymous:username")).Returns(_configSectionUsername.Object);
            _configuration.Setup(section => section.GetSection("anonymous:password")).Returns(_configSectionPassword.Object);
            _configuration.Setup(section => section.GetSection("anonymous:email")).Returns(_configSectionEmail.Object);
            _configuration.Setup(section => section.GetSection("genesys:isMassive")).Returns(_configSectionMasive.Object);
            IColumnTypes _columnTypes = new ColumnTypesPosgresql();
            _dbContext = new DataContext(_options, _columnTypes);
            _resxResourceProvider = new ResxProvider();
            _resourceEntryQuery = new ResourceEntryQuery(_dbContext, _logService.Object);
            _databaseResourceProvider = new DatabaseProvider(_dbContext, _resourceEntryQuery);
        }

        /// <summary>
        /// Generates a string with the specified maximum length, padding if necessary.
        /// </summary>
        /// <param name = "maximumLength">The maximum length for the string.</param>
        /// <param name = "value">The original string value.</param>
        /// <returns>A string modified to meet the maximum length condition.</returns>
        protected static string GetMaximumLength(int maximumLength, string value)
        {
            if (maximumLength > 0)
            {
                if (value.Length < maximumLength)
                {
                    value = value.PadRight(maximumLength + 1, '_');
                }
            }

            return value;
        }

        /// <summary>
        /// Trims the string to meet the specified minimum length requirement.
        /// </summary>
        /// <param name = "minimumLength">The minimum length for the string.</param>
        /// <param name = "value">The original string value.</param>
        /// <returns>A string modified to meet the minimum length condition.</returns>
        protected static string GetMinimumLength(int minimumLength, string value)
        {
            if (minimumLength > 0)
            {
                if (value.Length > minimumLength)
                {
                    value = value.Substring(0, minimumLength - 1);
                }
            }

            return value;
        }

        /// <summary>
        /// Creates a User object with specified name, optionally modifying its length.
        /// </summary>
        /// <param name = "name">The base name for the user.</param>
        /// <param name = "minimumLength">Optional minimum length for user attributes.</param>
        /// <param name = "maximumLength">Optional maximum length for user attributes.</param>
        /// <returns>A new User object with specified properties.</returns>
        protected static User GetUser(string name = "doe", int minimumLength = 0, int maximumLength = 0)
        {
            string userName = $"john.{name}";
            userName = GetValueModified(minimumLength, maximumLength, userName);
            string email = $"john.{name}@example.com";
            email = GetValueModified(minimumLength, maximumLength, email);
            string password = "password";
            password = GetValueModified(minimumLength, maximumLength, password);
            return new User
            {
                Id = Guid.NewGuid().ToString(),
                Name = userName,
                Email = email,
                Password = password
            };
        }

        /// <summary>
        /// Creates a User object with specified username and email.
        /// </summary>
        /// <param name = "userName">The username for the user.</param>
        /// <param name = "email">The email for the user.</param>
        /// <returns>A new User object with the specified properties.</returns>
        protected static User GetUser(string userName, string email)
        {
            string password = "password";
            string id = Guid.NewGuid().ToString();
            return new User
            {
                Id = id,
                Name = userName,
                Email = email,
                Password = password,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };
        }

        /// <summary>
        /// Creates a User object with specified username, email, and password.
        /// </summary>
        /// <param name = "userName">The username for the user.</param>
        /// <param name = "email">The email for the user.</param>
        /// <param name = "password">The password for the user.</param>
        /// <returns>A new User object with the specified properties.</returns>
        protected static User GetUser(string userName, string email, string password)
        {
            return new User
            {
                Id = Guid.NewGuid().ToString(),
                Name = userName,
                Email = email,
                Password = password
            };
        }

        /// <summary>
        /// Adjusts the length of a string by applying both minimum and maximum length modifications.
        /// </summary>
        /// <param name = "minimumLength">The minimum length for the string.</param>
        /// <param name = "maximumLength">The maximum length for the string.</param>
        /// <param name = "userName">The userName to be modified.</param>
        /// <returns>A string modified to satisfy the length constraints.</returns>
        protected static string GetValueModified(int minimumLength, int maximumLength, string userName)
        {
            userName = GetMaximumLength(maximumLength, userName);
            userName = GetMinimumLength(minimumLength, userName);
            return userName;
        }

        /// <summary>
        /// Computes the SHA256 hash of the input string.
        /// </summary>
        /// <param name = "rawData">The input data to hash.</param>
        /// <returns>A string representing the SHA256 hash of the input data.</returns>
        protected static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using SHA256 sha256Hash = SHA256.Create();
            // ComputeHash - returns byte array  
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
            // Convert byte array to a string   
            StringBuilder builder = new StringBuilder();
            foreach (byte v in bytes)
            {
                builder.Append(v.ToString("x2"));
            }

            return builder.ToString();
        }
    }
}