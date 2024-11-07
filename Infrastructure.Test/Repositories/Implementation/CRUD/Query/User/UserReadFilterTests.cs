namespace Infrastructure.Test.Repositories.Implementation.CRUD.Query.User
{
    using Application.UseCases.ExternalServices;
    using Infrastructure.Repositories.Implementation.CRUD.Query.User;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class UserReadFilterTests : TestsBase
    {
        private UserReadFilter _testClass;
        private Mock<ILogService> _logService;

        [TestInitialize]
        public void SetUp()
        {
            _logService=new Mock<ILogService>();
            _testClass=new UserReadFilter(_dbContext, _logService.Object);
        }
    }
}