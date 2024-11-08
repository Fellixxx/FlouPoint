namespace Infrastructure.Test.Repositories.Implementation.CRUD.Query.User
{
    using System;
    using Application.UseCases.ExternalServices;
    using Infrastructure.Repositories.Implementation.CRUD.Query.User;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Persistence.BaseDbContext;

    [TestClass]
    public class UserReadIdTests : BaseTests
    {
        [TestMethod]
        public void CannotConstructWithNullLogService()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new UserReadId(_dbContext, default(ILogService)));
        }
    }
}