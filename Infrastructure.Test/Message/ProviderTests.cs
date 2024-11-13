namespace Infrastructure.Test.Message
{
    using System;
    using System.Threading.Tasks;
    using Domain.Entities;
    using Infrastructure.Resource;
    using Infrastructure.Test.Repositories.Implementation;
    using Infrastructure.Test.Repositories.Implementation.CRUD;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Contains unit tests for the resource provider functionality to validate that 
    /// all expected resource keys are available in the resource entries.
    /// </summary>
    [TestClass]
    public class ProviderTests : SetupTest
    {
        /// <summary>
        /// Validates that all defined resource keys exist in the resource provider.
        /// </summary>
        [TestMethod]
        public async Task AllResourceKeysExistInResourceProvider()
        {
            // Arrange: Create an instance of ResxProvider and fetch its resource entries.
            var instance = new ResxProvider();
            var entriesResult = await instance.GetResourceEntries();
            // Assert: Ensure that the resource entries are not null or empty.
            Assert.IsNotNull(entriesResult?.Data, "Resource entries are null or empty.");
            // Convert and cache the entries to a list to avoid multiple enumerations.
            var entries = entriesResult.Data.ToList();
            // Act: Identify missing keys by checking if they exist in the resource entries.
            var missingKeys = _resourceMessages.Keys.Where(key => !ResourceExists(entries, key)).ToList();
            // Assert: Validate that there are no missing keys.
            // If there are any missing keys, failure message lists them.
            var missingResources = string.Join(", ", missingKeys);
            Assert.IsTrue(missingKeys.Count == 0, $"Missing resource keys: {missingResources}");
        }

        /// <summary>
        /// Checks if a resource key exists in the provided resource entries.
        /// </summary>
        /// <param name = "entries">The collection of resource entries to search in.</param>
        /// <param name = "key">The resource key to look for.</param>
        /// <returns>True if the resource key exists, otherwise false.</returns>
        private static bool ResourceExists(IEnumerable<Resource> entries, string key)
        {
            return entries.Any(entry => entry.Name.Contains(key));
        }
    }
}