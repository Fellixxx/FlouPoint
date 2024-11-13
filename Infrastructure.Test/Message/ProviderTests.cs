namespace Infrastructure.Test.Message
{
    using System;
    using System.Threading.Tasks;
    using Domain.Entities;
    using Infrastructure.Message;
    using Infrastructure.Test.Repositories.Implementation;
    using Infrastructure.Test.Repositories.Implementation.CRUD;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ProviderTests: SetupTest
    {
        [TestMethod]
        public async Task AllResourceKeysExistInResourceProvider()
        {
            // Arrange
            var instance = new ResxResourceProvider();
            var entriesResult = await instance.GetResourceEntries();

            // Assert early if entries result is null or contains no data
            Assert.IsNotNull(entriesResult?.Data, "Resource entries are null or empty.");

            var entries = entriesResult.Data.ToList(); // Cache to avoid multiple enumerations

            // Act
            var missingKeys = _resourceMessages.Keys.Where(key => !ResourceExists(entries, key)).ToList();

            // Assert

            //Please ensure all items are added to the appropriate resource file.

            var missingResorces = string.Join(", ", missingKeys);
            Assert.IsTrue(missingKeys.Count == 0, $"Missing resource keys: {missingResorces}");
        }

        private static bool ResourceExists(IEnumerable<Resource> entries, string key)
        {
            return entries.Any(entry => entry.Name.Contains(key));
        }
    }
}