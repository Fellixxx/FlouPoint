namespace Persistence.Test.Persistence.Data
{
    using global::Domain.Interfaces.Entity;
    using global::Persistence.Repositories;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using System;
    using System.Threading.Tasks;

    [TestClass]
    public class RepositoryTests
    {
        private Mock<DbContext> _mockContext;
        private Mock<DbSet<MockEntity>> _mockDbSet;
        private MockEntityRepository _repository;

        [TestInitialize]
        public void SetUp()
        {
            _mockContext = new Mock<DbContext>();
            _mockDbSet = new Mock<DbSet<MockEntity>>();
            _mockContext.Setup(x => x.Set<MockEntity>()).Returns(_mockDbSet.Object);
            _repository = new MockEntityRepository(_mockContext.Object);
        }

        [TestMethod]
        public async Task When_Create_ValidEntity_Then_Success()
        {
            // Given
            var entity = new MockEntity { Id = Guid.NewGuid().ToString(), Name = "Test" };

            // When
            var id = await _repository.Create(entity);

            // Then
            _mockDbSet.Verify(x => x.Add(entity), Times.Once);
            Assert.AreEqual(id, entity.Id);
        }

        [TestMethod]
        public async Task When_Create_NullEntity_Then_ThrowsException()
        {
            // Given, When, Then
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await _repository.Create(null));
        }

        [TestMethod]
        public async Task When_Update_NullEntity_Then_ThrowsException()
        {
            // Given, When, Then
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await _repository.Update(null));
        }

        [TestMethod]
        public async Task When_Delete_NullEntity_Then_ThrowsException()
        {
            // Given, When, Then
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await _repository.Delete(null));
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
