namespace FlouPoint.Test.Persistence.Repositories
{
    using global::Domain.Interfaces.Entity;
    using global::Persistence.Repositories;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using Moq;

    /// <summary>
    /// Tests for the <see cref = "MockEntityRepository"/> class.
    /// </summary>
    [TestFixture]
    public class RepositoryTests
    {
        private Mock<DbContext> _mockContext;
        private Mock<DbSet<MockEntity>> _mockDbSet;
        private MockEntityRepository _repository;
        /// <summary>
        /// Sets up the test dependencies.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            _mockContext = new Mock<DbContext>();
            _mockDbSet = new Mock<DbSet<MockEntity>>();
            _mockContext.Setup(x => x.Set<MockEntity>()).Returns(_mockDbSet.Object);
            _repository = new MockEntityRepository(_mockContext.Object);
        }

        /// <summary>
        /// Tests that creating a valid entity succeeds.
        /// </summary>
        [Test]
        public async Task When_Create_ValidEntity_Then_Success()
        {
            // Arrange
            var entity = new MockEntity
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Test"
            };
            _mockDbSet.Setup(m => m.AddAsync(It.IsAny<MockEntity>(), default)).ReturnsAsync((MockEntity e, _) => e);
            // Act
            var id = await _repository.Create(entity);
            // Assert
            _mockDbSet.Verify(x => x.Add(It.IsAny<MockEntity>()), Times.Once);
            _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
            entity.Id.Should().Be(id);
        }

        /// <summary>
        /// Tests that creating a null entity throws an exception.
        /// </summary>
        [Test]
        public void When_Create_NullEntity_Then_ThrowsException()
        {
            // Act & Assert
            Assert.ThrowsAsync<ArgumentNullException>(() => _repository.Create(null));
        }

        /// <summary>
        /// Tests that updating a null entity throws an exception.
        /// </summary>
        [Test]
        public void When_Update_NullEntity_Then_ThrowsException()
        {
            // Act & Assert
            Assert.ThrowsAsync<ArgumentNullException>(() => _repository.Update(null));
        }

        /// <summary>
        /// Tests that deleting a null entity throws an exception.
        /// </summary>
        [Test]
        public void When_Delete_NullEntity_Then_ThrowsException()
        {
            // Act & Assert
            Assert.ThrowsAsync<ArgumentNullException>(() => _repository.Delete(null));
        }

        /// <summary>
        /// Tests that deleting a valid entity succeeds.
        /// </summary>
        [Test]
        public async Task When_Delete_ValidEntity_Then_Success()
        {
            // Arrange
            var entity = new MockEntity
            {
                Id = "1",
                Name = "Test"
            };
            _mockDbSet.Setup(m => m.FindAsync("1")).ReturnsAsync(entity);
            // Act
            await _repository.Delete(entity);
            // Assert
            _mockDbSet.Verify(x => x.Remove(It.Is<MockEntity>(e => e == entity)), Times.Once);
            _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
        }

        // More tests for other methods like ReadPageByFilter, ReadCountByFilter can be included here...
        /// <summary>
        /// Mock version of the <see cref = "Repository{T}"/> class for unit tests.
        /// </summary>
        public class MockEntityRepository : Repository<MockEntity>
        {
            public MockEntityRepository(DbContext context) : base(context)
            {
            }
        }

        /// <summary>
        /// Mock entity class for testing purposes.
        /// </summary>
        public class MockEntity : IEntity
        {
            public string Id { get; set; }
            public string? Name { get; set; }
            public bool Active { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        }
    }
}