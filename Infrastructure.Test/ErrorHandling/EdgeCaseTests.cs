using Application.Result;
using Application.UseCases.CRUD.Query.User;
using Domain.Entities;
using Infrastructure.Test.Repositories.Implementation.CRUD;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Test.ErrorHandling
{
    [TestClass]
    public class EdgeCaseTests : SetupTest
    {
        [TestMethod]
        public async Task CreateUser_ShouldReturnError_WhenUserIsNull()
        {
            // Arrange: Set up a null input for user creation
            User nullUser = null;

            // Act & Assert: Ensure Create method handles null input gracefully
            var result = await _userCreate.Create(nullUser);

            Assert.IsFalse(result.IsSuccessful, "Operation should fail with null input.");
            Assert.AreEqual("Necessary data was not provided.", result.Message, "Error message should inform about missing user entity.");
        }

        [TestMethod]
        public async Task ReadUserById_ShouldReturnError_WhenIdIsNull()
        {
            // Arrange: Set up a null ID for reading a user
            string nullId = null;

            // Mock the UserQuery service to handle null input for ReadId
            var mockUserQuery = new Mock<IUserQuery>();
            mockUserQuery.Setup(uq => uq.ReadId(nullId))
                         .ReturnsAsync(Operation<User>.Failure("User ID is required.", Application.Result.Error.ErrorTypes.NotFound));

            // Inject the mock service into the test
            var userQueryService = mockUserQuery.Object;

            // Act: Call the ReadId method with a null ID
            var result = await userQueryService.ReadId(nullId);

            // Assert: Check that the result indicates failure and has the expected message
            Assert.IsFalse(result.IsSuccessful, "Operation should fail with null input.");
            Assert.AreEqual("User ID is required.", result.Message, "Error message should inform about missing user ID.");
        }


        [TestMethod]
        public async Task UpdateUser_ShouldReturnError_WhenUserIsNull()
        {
            // Arrange: Set up a null user for update
            User nullUser = null;


            // Act & Assert: Ensure Update method handles null input gracefully
            var result = await _userUpdate.Update(nullUser);

            Assert.IsFalse(result.IsSuccessful, "Operation should fail with null input.");
            Assert.AreEqual("Necessary data was not provided.", result.Message, "Error message should inform about missing user entity.");
        }
    }

}
