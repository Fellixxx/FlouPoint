namespace LayerInfrastructure.ExternalServices
{
    using global::Application.Result;
    using global::Application.Result.Error;
    using global::Application.UseCases.ExternalServices;
    using global::Domain.DTO.Log;
    using global::Domain.EnumType;
    using FluentAssertions;
    using global::Infrastructure.ExternalServices.LogExternal;
    using global::Infrastructure.Other;
    using Microsoft.Extensions.Configuration;
    using Moq;
    using Newtonsoft.Json;
    using NUnit.Framework;
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using global::Application.UseCases.Wrapper;
    using global::Domain.DTO.ResponseLogin;
    using global::Domain.EnumType.OperationExecute;
    using LayerInfrastructure.ExternalServices.LogExternal;

    [TestFixture]
    internal class LogServiceTests
    {
        private const string MessageSuccessful = "The log was create successfully.";
        private ILogService? logService;
        private Mock<IHttpClientFactory> mockHttpClientFactory;
        private Mock<IConfiguration> mockConfiguration;
        private Mock<IWrapper> mockIHttpContentWrapper;
        private const string SuccessMessage = "Successfully created log.";
        private Mock<IConfigurationSection> mockConfigSectionUsername;
        private Mock<IConfigurationSection> mockConfigSectionPassword;
        private Mock<IConfigurationSection> mockConfigSectionUrlLogservice;


        [SetUp]
        public void SetUp()
        {
            mockHttpClientFactory = new Mock<IHttpClientFactory>();
            mockConfiguration = new Mock<IConfiguration>();
            mockConfigSectionUsername = new Mock<IConfigurationSection>();
            mockConfigSectionPassword = new Mock<IConfigurationSection>();
            mockConfigSectionUrlLogservice = new Mock<IConfigurationSection>();
            mockIHttpContentWrapper = new Mock<IWrapper>();
        }

        void SetUpConfiguration(string username = "admin", string password = "password", string urllogservice = "url")
        {
            SetConfigurationValues(username, password, urllogservice);
            SetBehavior();
            SetBussinesValues();
        }

        private void SetBehavior()
        {
            logService = new LogService(
                mockHttpClientFactory.Object,
                mockConfiguration.Object,
                mockIHttpContentWrapper.Object
                );
        }
        private void SetConfigurationValues(string username, string password, string urllogservice)
        {
            mockConfigSectionUsername
                .Setup(x => x.Value)
                .Returns(username);
            mockConfigSectionPassword
                .Setup(x => x.Value)
                .Returns(password);
            mockConfigSectionUrlLogservice
                .Setup(x => x.Value)
                .Returns(urllogservice);
            mockConfiguration
                .Setup(section => section.GetSection("mongodb:username"))
                .Returns(mockConfigSectionUsername.Object);
            mockConfiguration
                .Setup(section => section.GetSection("mongodb:password"))
                .Returns(mockConfigSectionPassword.Object);
            mockConfiguration
                .Setup(section => section.GetSection("logService:urlLogservice"))
                .Returns(mockConfigSectionUrlLogservice.Object);
        }

        private static string GetToken()
        {
            ResponseLogin token = new ResponseLogin()
            {
                AccessToken = "xxnDBVrrFUhVvPWQasfdasfccFUasfddasQWQPh7L"
            };

            return JsonConvert.SerializeObject(token);
        }

        private void SetBussinesValues()
        {
            string tokenReponse = GetToken();
            mockIHttpContentWrapper
                .Setup(response => response.ReadAsStringAsync(It.IsAny<HttpContent>()))
                .Returns(Task.FromResult(tokenReponse));

            HttpResponseMessage httpResponseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            mockIHttpContentWrapper
                .Setup(response => response.PostAsync(
                    It.IsAny<HttpClient>(),
                    It.IsAny<string>(),
                    It.IsAny<HttpContent>(),
                    It.IsAny<AuthenticationHeaderValue>()))
                .Returns(Task.FromResult(httpResponseMessage));
        }

        [Test]
        public async Task CreateLog_ValidLogObject_ReturnsSuccess()
        {
            SetUpConfiguration();
            // Given
            var log = new Log { /* populate log object */ };

            // When
            var result = await logService.CreateLog(log);

            // Then
            result.IsSuccessful.Should().BeTrue();
            result.Data.Should().Be(string.Empty);
            result.Error.Should().Be("NONE");
            result.Message.Should().Be("The log was create successfully.");
        }

        [Test]
        public async Task CreateLog_NullLogObject_ReturnsFailure()
        {
            SetUpConfiguration();
            // Given
            Log log = null!;

            // When
            var result = await logService.CreateLog(log);

            // Then
            result.IsSuccessful.Should().BeTrue();
            result.Error.Should().Be("NONE");
            result.Message.Should().Be("The log was create successfully.");
        }

        [Test]
        public async Task CreateLog_ExceptionThrown_ReturnsFailure()
        {
            SetUpConfiguration();
            // Given
            var log = new Log { /* populate log object */ };

            mockIHttpContentWrapper
                .Setup(x => x.PostAsync(It.IsAny<HttpClient>(), It.IsAny<string>(), It.IsAny<HttpContent>(), It.IsAny<AuthenticationHeaderValue>()))
                .Throws(new Exception("Some exception"));

            // When
            var result = await logService.CreateLog(log);

            // Then
            result.IsSuccessful.Should().BeFalse();
            result.Error.Should().Be("UNEXPECTED_ERROR");
            result.Message.Should().Be("The log was create successfully.");
        }

        [Test]
        public Task When_CreateLog_ValidLogObject_Then_Success()
        {
            SetUpConfiguration();
            // Given
            SetBehavior();
            var myObject = new
            {
                Name = "Test Object",
                Description = "This is a description of the test object."
            };

            Exception ex = new Exception("This is a exception of the test log");
            Log log = Util.GetLogError(ex, myObject, OperationExecute.Activate);

            // When
            Task<OperationResult<string>>? result = logService?.CreateLog(log);

            // Then
            UtilTest<string>.Assert(result);
            result.Result.IsSuccessful.Should().BeTrue();
            result.Result.Data.Should().Be(string.Empty);
            result.Result.Message.Should().Be(MessageSuccessful);
            return Task.CompletedTask;
        }


        [Test]
        public Task When_CreateLog_InvalidLogObject_Then_Success()
        {
            SetUpConfiguration();
            // Given
            object? myObject = null;
            Exception ex = new Exception("This is a exception of the test log");
            Log log = Util.GetLogError(ex, myObject, OperationExecute.Activate);

            // When
            Task<OperationResult<string>> result = logService.CreateLog(log);

            // Then
            UtilTest<string>.Assert(result);
            result.Result.IsSuccessful.Should().BeTrue();
            result.Result.Data.Should().Be(string.Empty);
            result.Result.Message.Should().Be("The log was create successfully.");
            return Task.CompletedTask;
        }

        [Test]
        public Task When_CreateLog_InvalidLogObject_InvalidException_Then_Failed()
        {
            SetUpConfiguration();
            // Given
            object? myObject = null;
            Exception? ex = default(Exception);

            // When

            // Then
            Assert.ThrowsAsync<Exception>(async () => Util.GetLogError(ex, myObject, OperationExecute.Activate));
            return Task.CompletedTask;
        }

        [Test]
        public Task When_CreateLog_InvalidConfiguracion_Then_Failed()
        {
            SetUpConfiguration(username: string.Empty, password: string.Empty, urllogservice: string.Empty);
            // Given
            var myObject = new
            {
                Name = "Test Object",
                Description = "This is a description of the test object."
            };

            Exception ex = new Exception("This is a exception of the test log");
            Log log = Util.GetLogError(ex, myObject, OperationExecute.Activate);

            // When
            Task<OperationResult<string>> result = logService.CreateLog(log);

            // Then
            UtilTest<string>.Assert(result);
            result.Result.IsSuccessful.Should().BeFalse();
            result.Result.Data.Should().BeNull();
            string expected = ErrorTypes.ConfigurationMissingError.GetCustomName();
            result.Result.Error.Should().Be(expected);
            return Task.CompletedTask;
        }

    }
}
