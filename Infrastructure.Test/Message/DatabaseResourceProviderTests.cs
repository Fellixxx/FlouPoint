namespace Infrastructure.Test.Message
{
    using Infrastructure.Resource;
    using Infrastructure.Test.Repositories.Implementation.CRUD;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Unit test class for validating the construction and basic functionality of the DatabaseResourceProvider.
    /// </summary>
    [TestClass]
    public class DatabaseResourceProviderTests : SetupTest
    {
        /// <summary>
        /// Test to ensure that the DatabaseProvider instance can be successfully created.
        /// </summary>
        [TestMethod]
        public void DatabaseProvider_CanBeConstructedSuccessfully()
        {
            // Arrange
            // _dbContext and _resourceEntryQuery are expected to be initialized in SetupTest.
            // Act
            var databaseProviderInstance = new DatabaseProvider(_dbContext, _resourceEntryQuery);
            // Assert
            Assert.IsNotNull(databaseProviderInstance, "The DatabaseProvider instance should be successfully constructed and not null.");
        }
    }
}