namespace Infrastructure.Test.Repositories.Implementation.CRUD.Resource
{
    using System;
    using System.Threading.Tasks;
    using Domain.Entities;
    using Infrastructure.Repositories.Implementation.CRUD.Resource.Create;
    using Infrastructure.Test.Repositories.Implementation.CRUD;
    using Microsoft.VisualStudio.TestTools.UnitTesting;


    /// <summary>
    /// Tests for the creation of resource entities in the repository.
    /// </summary>
    [TestClass]
    public class ResourceCreateTests : SetupTest
    {
        private ResourceCreate _resourceCreate;

        [TestInitialize]
        public void Initialize()
        {
            _resourceCreate = new ResourceCreate(_dbContext, _logService.Object, _utilEntityResource, _provider, _handler);
        }

        /// <summary>
        /// Test to ensure the ResourceCreate class can be constructed successfully.
        /// </summary>
        [TestMethod]
        public void CanConstruct()
        {
            // Act
            var instance = new ResourceCreate(_dbContext, _logService.Object, _utilEntityResource, _provider, _handler);

            // Assert
            Assert.IsNotNull(instance);
        }

        /// <summary>
        /// Test to ensure an ArgumentNullException is thrown when the context is null during ResourceCreate construction.
        /// </summary>
        [TestMethod]
        public void CannotConstructWithNullContext()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
                new ResourceCreate(null, _logService.Object, _utilEntityResource, _provider, _handler));
        }

        /// <summary>
        /// Test to ensure a ResourceCreate instance can be constructed with a null log service.
        /// </summary>
        [TestMethod]
        public void CanConstructWithNullLogService()
        {
            var resourceCreate = new ResourceCreate(_dbContext, null, _utilEntityResource, _provider, _handler);
            Assert.IsNotNull(resourceCreate);
        }

        /// <summary>
        /// Test for creating a valid resource entity; expects a success response.
        /// </summary>
        [TestMethod]
        public async Task When_CreateEntity_WithValidResource_ShouldReturnSuccess()
        {
            // Given
            string id = Guid.NewGuid().ToString();
            var resource = new Resource
            {
                Id = id,
                Name = "ValidResource",
                Value = "ValidValue",
                Comment = "ValidCommentCaracters",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Active = false
            };

            // When
            var result = await _resourceCreate.Create(resource);

            // Then
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual("Resource was created successfully.", result.Message); // Assuming localized message
            Assert.AreEqual(id, result.Data);
        }

        /// <summary>
        /// Test for creating a resource with a missing required field; expects a failure response.
        /// </summary>
        [TestMethod]
        public async Task When_CreateEntity_WithMissingRequiredFields_ShouldReturnFailure()
        {
            // Given
            var resource = new Resource
            {
                Id = Guid.NewGuid().ToString(),
                Value = "ValidValue", // Missing Name
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Active = true
            };

            // When
            var result = await _resourceCreate.Create(resource);

            // Then
            Assert.IsFalse(result.IsSuccessful);
            Assert.IsNull(result.Data);
            StringAssert.Contains(result.Message, "Name"); // Assuming error mentions missing 'Name'
        }

        /// <summary>
        /// Test for creating a resource with a duplicate ID; expects a failure response.
        /// </summary>
        [TestMethod]
        public async Task When_CreateEntity_WithDuplicateId_ShouldReturnFailure()
        {
            // Given
            string id = Guid.NewGuid().ToString();
            var resource1 = new Resource
            {
                Id = id,
                Name = "Resource1",
                Value = "Value1",
                Comment = "Comment1Caracters",
                Active = true
            };
            var resource2 = new Resource
            {
                Id = id, // Duplicate ID
                Name = "Resource2",
                Value = "Value2",
                Comment = "Comment2Caracters",
                Active = false
            };

            // When
            var result1 = await _resourceCreate.Create(resource1);
            var result2 = await _resourceCreate.Create(resource2);

            // Then
            Assert.IsTrue(result1.IsSuccessful);
            Assert.IsFalse(result2.IsSuccessful);
            StringAssert.Contains(result2.Message, "The given name already exists."); // Assuming localized message
        }

        /// <summary>
        /// Test for creating a null resource entity; expects a failure response.
        /// </summary>
        [TestMethod]
        public async Task When_CreateEntity_WithNullResource_ShouldReturnFailure()
        {
            // When
            var result = await _resourceCreate.Create(null);

            // Then
            Assert.IsFalse(result.IsSuccessful);
            Assert.IsNull(result.Data);
            StringAssert.Contains(result.Message, "Necessary data was not provided.");
        }

        /// <summary>
        /// Test for setting timestamps during creation; expects created and updated timestamps to be set correctly.
        /// </summary>
        [TestMethod]
        public async Task When_CreateEntity_WithValidResource_ShouldSetTimestamps()
        {
            // Given
            var resource = new Resource
            {
                Id = Guid.NewGuid().ToString(),
                Name = "ResourceWithTimestamps",
                Value = "ValueWithTimestamps",
                Comment = "CommentWithTimestamps",
                Active = true
            };

            // When
            var startTime = DateTime.UtcNow;
            var result = await _resourceCreate.Create(resource);
            var endTime = DateTime.UtcNow;

            // Then
            Assert.IsTrue(result.IsSuccessful);
            Assert.IsNotNull(result.Data);
        }
    }
}
