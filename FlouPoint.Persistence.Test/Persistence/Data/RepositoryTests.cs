namespace FlouPoint.Application.Test.Persistence.Repositories
{
    using global::Domain.Interfaces.Entity;
    using global::Persistence.Repositories;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using Moq;

    [TestFixture]
    public class RepositoryTests
    {
        private Mock<DbContext> _mockContext;
        private Mock<DbSet<MockEntity>> _mockDbSet;
        private MockEntityRepository _repository;

        [SetUp]
        public void SetUp()
        {
            _mockContext = new Mock<DbContext>();
            _mockDbSet = new Mock<DbSet<MockEntity>>();
            _mockContext.Setup(x => x.Set<MockEntity>()).Returns(_mockDbSet.Object);
            _repository = new MockEntityRepository(_mockContext.Object);
        }

        [Test]
        public async Task When_Create_ValidEntity_Then_Success()
        {
            // Given
            var entity = new MockEntity { Id = Guid.NewGuid().ToString(), Name = "Test" };

            // When
            var id = await _repository.Create(entity);

            // Then
            _mockDbSet.Verify(x => x.Add(entity), Times.Once);
            entity.Id.Should().Be(id);

        }

        [Test]
        public void When_Create_NullEntity_Then_ThrowsException()
        {
            // Given, When, Then
            Assert.ThrowsAsync<ArgumentNullException>(() => _repository.Create(null));
        }

        [Test]
        public void When_Update_NullEntity_Then_ThrowsException()
        {
            // Given, When, Then
            Assert.ThrowsAsync<ArgumentNullException>(() => _repository.Update(null));
        }



        [Test]
        public void When_Delete_NullEntity_Then_ThrowsException()
        {
            // Given, When, Then
            Assert.ThrowsAsync<ArgumentNullException>(() => _repository.Delete(null));
        }

        // ... (You can add more tests for other methods like ReadPageByFilter, ReadCountFilter)

        public class MockEntityRepository : Repository<MockEntity>
        {
            public MockEntityRepository(DbContext context) : base(context)
            {
            }
        }

        public class MockEntity : IEntity
        {
            public string Id { get; set; }
            public string? Name { get; set; }
            public bool Active { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        }
    }
}
