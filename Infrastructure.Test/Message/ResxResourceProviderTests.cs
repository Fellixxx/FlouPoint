namespace Infrastructure.Test.Message
{
    using System;
    using System.Threading.Tasks;
    using Infrastructure.Message;
    using Infrastructure.Test.Repositories.Implementation;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ResxResourceProviderTests: BaseTests
    {
        [TestMethod]
        public void CanCallGetEntries()
        {
            // Act
            var result = ResxResourceProvider.GetEntries();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count, 78);
        }

        [TestMethod]
        public async Task CanCallGetMessage()
        {
            // Arrange
            var key = "Infrastructure.Other.ResourceExample.resources.ResorceExample";

            // Act
            var result = await _resxResourceProvider.GetMessage(key);

            // Assert
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual(result.ErrorType, Application.Result.Error.ErrorTypes.None);
        }
    }
}