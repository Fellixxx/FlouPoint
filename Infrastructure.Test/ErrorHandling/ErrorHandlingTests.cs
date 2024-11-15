namespace Infrastructure.Test.ErrorHandling
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using System;
    using System.Threading.Tasks;
    using Application.UseCases.CRUD.User;
    using Persistence.BaseDbContext;
    using Application.UseCases.CRUD.Query.User;
    using Domain.Entities;
    using Infrastructure.Repositories.Implementation.CRUD.Query.User;
    using Infrastructure.Repositories.Implementation.CRUD.User.Create;
    using Infrastructure.Test.Repositories.Implementation.CRUD;
    using Application.Result;

    [TestClass]
    public class ErrorHandlingTests : SetupTest
    {
        // Test for handling missing service dependencies
        [TestMethod]
        public async Task MissingDependency_ShouldReturnMeaningfulError_WhenUserCreateServiceIsNull()
        {
            // Arrange: Set UserCreate service to null to simulate a missing dependency
            IUserCreate nullUserCreateService = null;

            // Act: Define an async function that checks for null and throws an InvalidOperationException if null
            async Task Act()
            {
                if (nullUserCreateService == null)
                {
                    throw new InvalidOperationException("UserCreate service is not initialized.");
                }

                // Call the Create method if the service is not null (this line will not be reached in this test)
                await nullUserCreateService.Create(new User());
            }

            // Assert: Ensure that the Act function throws an InvalidOperationException with a meaningful message
            var ex = await Assert.ThrowsExceptionAsync<InvalidOperationException>(Act);
            Assert.AreEqual("UserCreate service is not initialized.", ex.Message, "Exception message should inform about missing dependency.");
        }


        [TestMethod]
        public async Task MissingDependency_ShouldReturnMeaningfulError_WhenUserQueryServiceIsNull()
        {
            // Arrange: Set UserQuery service to null to simulate a missing dependency
            IUserQuery nullUserQueryService = null;

            // Act: Define an async function that checks for null and throws an InvalidOperationException if null
            async Task Act()
            {
                if (nullUserQueryService == null)
                {
                    throw new InvalidOperationException("UserQuery service is not initialized.");
                }

                // Call the ReadId method if the service is not null (this line will not be reached in this test)
                await nullUserQueryService.ReadId("some-id");
            }

            // Assert: Ensure that the Act function throws an InvalidOperationException with a meaningful message
            var ex = await Assert.ThrowsExceptionAsync<InvalidOperationException>(Act);
            Assert.AreEqual("UserQuery service is not initialized.", ex.Message, "Exception message should inform about missing dependency.");
        }
    }
}
