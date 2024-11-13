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
    public class UserReadFilterCountTests : SetupTest
    {

        [TestMethod]
        public void CanConstruct()
        {
            // Act
            var instance = new UserReadFilterCount(_dbContext, _logService.Object, _resourceProvider, _resourceHandler);

            // Assert
            Assert.IsNotNull(instance);
        }

        [TestMethod]
        public void CannotConstructWithNullContext()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new UserReadFilterCount(default(DataContext), _logService.Object, _resourceProvider, _resourceHandler));
        }
    }
}