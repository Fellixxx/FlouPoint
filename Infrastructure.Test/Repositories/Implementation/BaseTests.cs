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
using Infrastructure.Repositories.Implementation.CRUD.Query.User;
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

namespace Infrastructure.Test.Repositories.Implementation
{
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

        [TestInitialize]
        public virtual void Setup()
        {
            _logService = new Mock<ILogService>();
            _configuration = new Mock<IConfiguration>();
            _otpValidate = new Mock<IOtpValidate>();
            _otpGenerate = new Mock<IOtpGenerate>();



            _configurationSection = new Mock<IConfigurationSection>();
            _configurationSection.SetupGet(m => m.Value)
                .Returns("ssnDBVccFUhVvPWQPh7LssnDBVccFUhVvPWQPh7L");
            _configuration.Setup(config => config.GetSection(It.IsAny<string>()))
                .Returns(_configurationSection.Object);

            _options = new DbContextOptionsBuilder<DataContext>()
               .UseInMemoryDatabase(databaseName: "testdb")
               .EnableSensitiveDataLogging(true)
               .Options;
            _configSectionUsername = new Mock<IConfigurationSection>();
            _configSectionPassword = new Mock<IConfigurationSection>();
            _configSectionEmail = new Mock<IConfigurationSection>();
            _configSectionMasive = new Mock<IConfigurationSection>();

            _configSectionUsername
               .Setup(x => x.Value)
               .Returns("usernameanonymous");
            _configSectionPassword
                .Setup(x => x.Value)
                .Returns("72b28030ce99fa4d0ab678f1d4a4374cc0d7bb676eb4307b0fa105f4c66b644e");
            _configSectionEmail
                .Setup(x => x.Value)
                .Returns("user.anonymous@withoutemail.com");
            _configSectionMasive
                .Setup(x => x.Value)
                .Returns("false");
            _configuration
                .Setup(section => section.GetSection("anonymous:username"))
                .Returns(_configSectionUsername.Object);
            _configuration
                .Setup(section => section.GetSection("anonymous:password"))
                .Returns(_configSectionPassword.Object);
            _configuration
                .Setup(section => section.GetSection("anonymous:email"))
                .Returns(_configSectionEmail.Object);
            _configuration
                .Setup(section => section.GetSection("genesys:isMassive"))
                .Returns(_configSectionMasive.Object);


            IColumnTypes _columnTypes = new ColumnTypesPosgresql();
            _dbContext = new DataContext(_options, _columnTypes);

            _resxResourceProvider = new ResxProvider();
            _resourceEntryQuery = new ResourceEntryQuery(_dbContext, _logService.Object);
            _databaseResourceProvider = new DatabaseProvider(_dbContext, _resourceEntryQuery);
        }


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

        protected static string GetValueModified(int minimumLength, int maximumLength, string userName)
        {
            userName = GetMaximumLength(maximumLength, userName);
            userName = GetMinimumLength(minimumLength, userName);
            return userName;
        }

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
