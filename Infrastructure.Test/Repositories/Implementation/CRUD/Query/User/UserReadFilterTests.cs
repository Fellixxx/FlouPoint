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

    [TestClass]
    public class UserReadFilterTests : ResourceProviderTests
    {
        [TestMethod]
        public void CanConstruct()
        {
            // Act
            var instance = new UserReadFilter(_dbContext, _logService.Object, _resourceProvider, _resourceHandler);

            // Assert
            Assert.IsNotNull(instance);
        }

        [TestMethod]
        public void CannotConstructWithNullContext()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new UserReadFilter(default(CommonDbContext), _logService.Object, _resourceProvider, _resourceHandler));
        }
    }
}