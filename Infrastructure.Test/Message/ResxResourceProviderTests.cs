namespace Infrastructure.Test.Message
{
    using System.Threading.Tasks;
    using Infrastructure.Resource;
    using Infrastructure.Test.Repositories.Implementation;
    using Infrastructure.Test.Repositories.Implementation.CRUD;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Unit tests for testing the ResxResourceProvider functionality.
    /// </summary>
    [TestClass]
    public class ResxResourceProviderTests : SetupTest
    {
        /// <summary>
        /// Tests that ResxProvider.GetEntries retrieves a non-null list with the correct count.
        /// </summary>
        [TestMethod]
        public void GetEntries_ReturnsCorrectNumberOfEntries()
        {
            // Act: Call the GetEntries method to retrieve resource entries.
            var result = ResxProvider.GetEntries();
            // Assert: Verify that the result is not null and contains the expected number of entries.
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count, 31);
        }

        /// <summary>
        /// Tests that GetMessage returns a successful result for a valid key with no error type.
        /// </summary>
        [TestMethod]
        public async Task GetMessage_ValidKey_ReturnsSuccess()
        {
            // Arrange: Set up a valid resource key for fetching the message.
            var key = "Infrastructure.ExternalServices.LogExternal.ServiceBase.LogServiceBaseResource.resources.SuccessfullySetLog";
            // Act: Asynchronously call the GetMessage method with the key.
            var result = await _resxResourceProvider.GetMessage(key);
            // Assert: Verify that the result is successful and has no associated error type.
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual(result.Type, Application.Result.Error.ErrorTypes.None);
        }

        /// <summary>
        /// Tests that GetMessage with an invalid key returns a failed result with error details.
        /// </summary>
        [TestMethod]
        public async Task GetMessage_InvalidKey_ReturnsErrorDetails()
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