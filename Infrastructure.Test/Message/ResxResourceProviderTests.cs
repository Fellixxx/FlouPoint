namespace Infrastructure.Test.Message
{
    using System;
    using System.Threading.Tasks;
    using Infrastructure.Resource;
    using Infrastructure.Test.Repositories.Implementation;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Unit tests for testing the ResxResourceProvider functionality.
    /// </summary>
    [TestClass]
    public class ResxResourceProviderTests : BaseTests
    {
        /// <summary>
        /// Tests that the GetEntries method successfully retrieves a non-null list of entries
        /// and checks if the count of entries is equal to the expected value.
        /// </summary>
        [TestMethod]
        public void CanCallGetEntries()
        {
            // Act: Call the GetEntries method to retrieve resource entries.
            var result = ResxProvider.GetEntries();
            // Assert: Verify that the result is not null and contains the expected number of entries.
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count, 29);
        }

        /// <summary>
        /// Tests that calling GetMessage with a valid key returns a successful result with no error type.
        /// </summary>
        [TestMethod]
        public async Task CanCallGetMessage()
        {
            // Arrange: Set up a valid resource key for fetching the message.
            var key = "Infrastructure.ExternalServices.LogExternal.ResourceLogService.resources.SuccessfullySetLog";
            // Act: Asynchronously call the GetMessage method with the key.
            var result = await _resxResourceProvider.GetMessage(key);
            // Assert: Verify that the result is successful and has no associated error type.
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual(result.Type, Application.Result.Error.ErrorTypes.None);
        }

        /// <summary>
        /// Tests that calling GetMessage with an invalid key returns a failed result and specific error details.
        /// </summary>
        [TestMethod]
        public async Task KeyNotFoundCallGetMessage()
        {
            // Arrange: Set up an invalid resource key which is not expected to be found.
            var key = "Infrastructure.Other.ResourceExample.resources.ResorceExample";
            // Act: Asynchronously call the GetMessage method with the invalid key.
            var result = await _resxResourceProvider.GetMessage(key);
            // Assert: Verify that the result is not successful, and contains expected error details.
            Assert.IsFalse(result.IsSuccessful);
            Assert.AreEqual(Application.Result.Error.ErrorTypes.BusinessValidation, result.Type);
            Assert.AreEqual("BUSINESS_VALIDATION_ERROR", result.Error);
            Assert.AreEqual("Resource not found.", result.Message);
        }
    }
}