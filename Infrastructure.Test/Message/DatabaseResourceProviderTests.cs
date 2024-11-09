namespace Infrastructure.Test.Message
{
    using System;
    using System.Threading.Tasks;
    using Application.UseCases.Repository.CRUD.ResourceEntry;
    using Infrastructure.Message;
    using Infrastructure.Test.Repositories.Implementation;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using TContext = System.String;

    [TestClass]
    public class DatabaseResourceProviderTests: BaseTests
    {
        [TestMethod]
        public void CanConstruct()
        {
            // Act
            var instance = new DatabaseResourceProvider(_dbContext, _resourceEntryQuery);

            // Assert
            Assert.IsNotNull(instance);
        }
    }
}