namespace Infrastructure.Test.Repositories.Implementation.CRUD.Query.User
{
    using System;
    using Application.UseCases.ExternalServices;
    using Infrastructure.Repositories.Implementation.CRUD.Query.User;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Persistence.BaseDbContext;
    using Persistence.CreateStruture.Constants.ColumnType;
    using TContext = System.String;

    /// <summary>
    /// Unit tests for the UserReadFilterCount class.
    /// </summary>
    [TestClass]
    public class UserReadFilterCountTests : SetupTest
    {
        /// <summary>
        /// Tests if the UserReadFilterCount can be constructed successfully.
        /// </summary>
        [TestMethod]
        public void CanConstruct()
        {
            // Act: Create an instance of UserReadFilterCount
            var instance = new UserReadFilterCount(_dbContext, _logService.Object, _resourceProvider, _resourceHandler);
            // Assert: Verify that the instance is not null
            Assert.IsNotNull(instance);
        }

        /// <summary>
        /// Tests that UserReadFilterCount cannot be constructed with a null DataContext.
        /// </summary>
        [TestMethod]
        public void CannotConstructWithNullContext()
        {
            // Assert: Verify that constructing with a null DataContext throws an ArgumentNullException
            Assert.ThrowsException<ArgumentNullException>(() => new UserReadFilterCount(default(DataContext), _logService.Object, _resourceProvider, _resourceHandler));
        }
    }
}