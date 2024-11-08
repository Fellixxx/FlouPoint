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
    public class UserReadFilterPageTests: BaseTests
    {
        [TestMethod]
        public void CannotConstructWithNullLogService()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new UserReadFilterPage(_dbContext, default(ILogService)));
        }
    }
}