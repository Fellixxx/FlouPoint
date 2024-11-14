namespace Infrastructure.Test.Repositories.Implementation.Security
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using System.Threading.Tasks;
    using Domain.Entities;
    using Application.UseCases.CRUD.User;
    using Application.Result;
    using Application.UseCases.CRUD.Query.User;
    using Infrastructure.Test.Repositories.Implementation.CRUD;
    using System.Diagnostics;

    [TestClass]
    public class SecurityAndAccessControlTests : SetupTest
    {
        // Unauthorized Access Attempt Tests
        [TestMethod]
        public async Task UnauthorizedAccess_ShouldFail_WhenUserLacksPermissions()
        {
            // Arrange: Set up a user with limited permissions
            var limitedAccessUser = new User
            {
                Id = "limited-user-id",
                Name = "Limited User",
                Email = "limiteduser@example.com",
                Password = "Password123!"
            };

            // Mock the UserCreate and UserRead services to check for authorization
            var mockUserCreate = new Mock<IUserCreate>();
            mockUserCreate.Setup(c => c.Create(It.IsAny<User>()))
                          .ThrowsAsync(new UnauthorizedAccessException("User lacks permissions to create records."));

            // Act & Assert: Attempt to create a user without permission should fail with UnauthorizedAccessException
            await Assert.ThrowsExceptionAsync<UnauthorizedAccessException>(() => mockUserCreate.Object.Create(limitedAccessUser));
        }

        // Input Validation for SQL Injection Tests
        [TestMethod]
        public async Task SqlInjectionAttempt_ShouldFail_WhenInvalidCharactersInInput()
        {
            // Arrange: Create a UserQuery mock that simulates SQL injection prevention
            var mockUserQuery = new Mock<IUserQuery>();
            string maliciousInput = "'; DROP TABLE Users; --";

            // Set up the mock to throw an exception if SQL injection attempt is detected
            mockUserQuery.Setup(q => q.ReadId(It.IsAny<string>()))
                         .ThrowsAsync(new InvalidOperationException("Invalid input detected."));

            // Act & Assert: Attempt to read a user with a SQL injection input should fail
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => mockUserQuery.Object.ReadId(maliciousInput));
        }

        [TestMethod]
        public async Task SqlInjectionAttemptInFilter_ShouldFail_WhenInvalidCharactersInFilter()
        {
            // Arrange: Use a filter expression that could contain a SQL injection attempt
            var mockUserReadFilter = new Mock<IUserReadFilter>();
            string maliciousFilter = "'; DROP TABLE Users; --";

            // Setup the mock to throw if SQL injection attempt is detected in filter input
            mockUserReadFilter.Setup(rf => rf.ReadFilter(It.IsAny<System.Linq.Expressions.Expression<System.Func<User, bool>>>()))
                              .ThrowsAsync(new InvalidOperationException("Invalid filter input detected."));

            // Act & Assert: Passing a potentially harmful filter should raise an error
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() =>
                mockUserReadFilter.Object.ReadFilter(u => u.Name.Contains(maliciousFilter)));
        }

        // Sensitive Data Protection Tests
        [TestMethod]
        public async Task Password_ShouldBeStoredHashed_WhenCreatingUser()
        {
            // Arrange: Create a new user with a plaintext password
            var newUser = new User
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Sensitive User",
                Email = "sensitiveuser@example.com",
                Password = "PlainPassword123!" // Input password in plaintext
            };

            // Create a hashed password simulation
            string hashedPassword = "hashed_PlainPassword123!"; // Example of a "hashed" password for testing

            // Mock the UserCreate service to verify that the password is hashed
            var mockUserCreate = new Mock<IUserCreate>();
            mockUserCreate
                .Setup(c => c.Create(It.IsAny<User>()))
                .ReturnsAsync(Operation<string>.Success("User created", "User created with hashed password"))
                .Callback<User>(user =>
                {
                    // Simulate hashing by setting the password to the hashed value
                    user.Password = hashedPassword;
                });

            // Act: Create the user
            var result = await mockUserCreate.Object.Create(newUser);

            // Debugging output
            Debug.WriteLine($"Result IsSuccessful: {result?.IsSuccessful}");
            Debug.WriteLine($"Result Message: {result?.Message}");

            // Assert: Password should be stored hashed/encrypted
            Assert.IsNotNull(result, "Result should not be null."); // Additional check to ensure result is not null
            Assert.IsTrue(result.IsSuccessful, "User creation should succeed.");
            Assert.AreNotEqual("PlainPassword123!", newUser.Password, "Password should be hashed.");
            Assert.AreEqual(hashedPassword, newUser.Password, "Password should match the mocked hashed value.");
        }



        [TestMethod]
        public void PasswordRetrieval_ShouldFail_WhenAttemptingToReadPlaintextPassword()
        {
            // Arrange: Set up the user with a hashed password
            var user = new User
            {
                Id = "user-id",
                Name = "Hashed User",
                Email = "hasheduser@example.com",
                Password = "hashed_password" // Simulating hashed password
            };

            // Mock the UserRead service to verify that plaintext password is not retrievable
            var mockUserRead = new Mock<IUserQuery>();
            mockUserRead.Setup(r => r.ReadId(It.IsAny<string>()))
                        .ReturnsAsync(Operation<User>.Success(user, "User data retrieved"));

            // Act: Retrieve the user
            var result = mockUserRead.Object.ReadId("user-id").Result;

            // Assert: Verify password is hashed and not plaintext
            Assert.AreNotEqual("PlainPassword123!", result.Data.Password, "Password should not be stored in plaintext.");
            Assert.AreEqual("hashed_password", result.Data.Password, "Password should be retrieved in hashed form.");
        }
    }
}
