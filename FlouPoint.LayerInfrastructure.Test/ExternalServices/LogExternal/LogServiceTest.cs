namespace FlouPoint.LayerInfrastructure.Test.ExternalServices.LogExternal
{
    using Application.Result;
    using Application.Result.Error;
    using Application.UseCases.Wrapper;
    using Domain.DTO.Log;
    using FluentAssertions;
    using global::Infrastructure.ExternalServices.LogExternal;
    using Infrastructure.ExternalServices.LogExternal;
    using Microsoft.Extensions.Configuration;
    using Moq;
    using NUnit.Framework;
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    [TestFixture]
    public class LogServiceTests
    {
        private Mock<IHttpClientFactory> _mockClientFactory;
        private Mock<IConfiguration> _mockConfiguration;
        private Mock<IWrapper> _mockHttpContentWrapper;
        private LogService _logService;

        [SetUp]
        public void SetUp()
        {
            _mockClientFactory = new Mock<IHttpClientFactory>();
            _mockConfiguration = new Mock<IConfiguration>();
            _mockHttpContentWrapper = new Mock<IWrapper>();

            // Instantiate the LogService with mocks
            _logService = new LogService(_mockClientFactory.Object, _mockConfiguration.Object, _mockHttpContentWrapper.Object);
        }

    }
}

