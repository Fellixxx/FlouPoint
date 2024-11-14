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
    /// This test class contains unit tests for the <see cref = "UserReadFilter"/> class,
    /// specifically testing its construction and initialization behavior.
    /// </summary>
    [TestClass]
    public class UserReadFilterTests : SetupTest
    {
        /// <summary>
        /// Tests whether an instance of <see cref = "UserReadFilter"/> can be constructed successfully with valid parameters.
        /// </summary>
        [TestMethod]
        public void Constructor_ShouldInitializeInstance_WithValidParameters()
        {
            // Act
            var instance = new UserReadFilter(_dbContext, _logService.Object, _resourceProvider, _resourceHandler);
            // Assert
            Assert.IsNotNull(instance, "Instance of UserReadFilter should not be null when constructed with valid parameters.");
        }

        /// <summary>
        /// Tests if the <see cref = "UserReadFilter"/> constructor throws an <see cref = "ArgumentNullException"/>
        /// when a null DataContext is passed, ensuring that invalid states are not allowed.
        /// </summary>
        [TestMethod]
        public void Constructor_ShouldThrowArgumentNullException_WhenDataContextIsNull()
        {
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() => new UserReadFilter(default(DataContext), _logService.Object, _resourceProvider, _resourceHandler), "Constructor should throw ArgumentNullException when a null DataContext is provided.");
        }
    }
}