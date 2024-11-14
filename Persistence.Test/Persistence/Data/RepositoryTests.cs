namespace Persistence.Test.Persistence.Data
{
    using global::Domain.Interfaces.Entity;
    using global::Persistence.Repositories;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// This class contains unit tests for the Repository using a MockEntity.
    /// </summary>
    [TestClass]
    public class RepositoryTests
    {
        private Mock<DbContext> _mockContext;
        private Mock<DbSet<MockEntity>> _mockDbSet;
        private MockEntityRepository _repository;
        /// <summary>
        /// Initializes the test by setting up the mock objects.
        /// </summary>
        [TestInitialize]
        public void Initialize_Mocks()
        {
            _mockContext = new Mock<DbContext>();
            _mockDbSet = new Mock<DbSet<MockEntity>>();
            _mockContext.Setup(x => x.Set<MockEntity>()).Returns(_mockDbSet.Object);
            _repository = new MockEntityRepository(_mockContext.Object);
        }

        /// <summary>
        /// Verifies that a valid entity is successfully created.
        /// </summary>
        [TestMethod]
        public async Task Create_ValidEntity_ShouldAddEntity()
        {
            // Arrange
            var entity = new MockEntity
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Test"
            };
            // Act
            var id = await _repository.Create(entity);
            // Assert
            _mockDbSet.Verify(x => x.Add(entity), Times.Once);
            Assert.AreEqual(id, entity.Id);
        }

        /// <summary>
        /// Verifies that creating a null entity throws an exception.
        /// </summary>
        [TestMethod]
        public async Task Create_NullEntity_ShouldThrowArgumentNullException()
        {
            // Arrange, Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await _repository.Create(null));
        }

        /// <summary>
        /// Verifies that updating a null entity throws an exception.
        /// </summary>
        [TestMethod]
        public async Task Update_NullEntity_ShouldThrowArgumentNullException()
        {
            // Arrange, Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await _repository.Update(null));
        }

        /// <summary>
        /// Verifies that deleting a null entity throws an exception.
        /// </summary>
        [TestMethod]
        public async Task Delete_NullEntity_ShouldThrowArgumentNullException()
        {
            // Arrange, Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await _repository.Delete(null));
        }

        // More test methods can be added for other Repository methods
        /// <summary>
        /// A custom repository class for MockEntity.
        /// </summary>
        public class MockEntityRepository : Repository<MockEntity>
        {
            public MockEntityRepository(DbContext context) : base(context)
            {
            }
        }

        /// <summary>
        /// A mock entity class implementing IEntity for testing purposes.
        /// </summary>
        public class MockEntity : IEntity
        {
            public string Id { get; set; }
            public string? Name { get; set; }
            public bool Active { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        }
    }
}