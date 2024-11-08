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
    public class UserReadIdTests: BaseTests
    {
        [TestMethod]
        public void CannotConstructWithNullLogService()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new UserReadId(_dbContext, default(ILogService)));
        }
    }
}