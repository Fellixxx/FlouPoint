namespace Infrastructure.Test.Message
{
    using System;
    using System.Threading.Tasks;
    using Infrastructure.Resource;
    using Infrastructure.Test.Repositories.Implementation;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ResxResourceProviderTests: BaseTests
    {
        [TestMethod]
        public void CanCallGetEntries()
        {
            // Act
            var result = ResxProvider.GetEntries();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count, 29);
        }

        [TestMethod]
        public async Task CanCallGetMessage()
        {
            // Arrange
            var key = "Infrastructure.ExternalServices.LogExternal.ResourceLogService.resources.SuccessfullySetLog";

            // Act
            var result = await _resxResourceProvider.GetMessage(key);

            // Assert
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual(result.Type, Application.Result.Error.ErrorTypes.None);
        }

        [TestMethod]
        public async Task KeyNotFoundCallGetMessage()
        {
            // Arrange
            var key = "Infrastructure.Other.ResourceExample.resources.ResorceExample";

            // Act
            var result = await _resxResourceProvider.GetMessage(key);

            // Assert
            Assert.IsFalse(result.IsSuccessful);
            Assert.AreEqual(Application.Result.Error.ErrorTypes.BusinessValidation, result.Type);
            Assert.AreEqual("BUSINESS_VALIDATION_ERROR", result.Error);
            Assert.AreEqual("Resource not found.", result.Message);
            
        }
    }
}