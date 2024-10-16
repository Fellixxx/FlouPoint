using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LayerPersistence.Repositories
{
    public class TestEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    [TestFixture]
    public class ReadTests
    {
        private Mock<DbContext> _dbContextMock;
        private Mock<DbSet<TestEntity>> _dbSetMock;
        private Read<TestEntity> _repository;

        [SetUp]
        public void SetUp()
        {
            _dbContextMock = new Mock<DbContext>();

            // Test data for IQueryable simulation
            var testData = new List<TestEntity>
            {
                new TestEntity { Id = 1 },
                new TestEntity { Id = 2 },
                new TestEntity { Id = 3 }
            }.AsQueryable();

            _dbSetMock = new Mock<DbSet<TestEntity>>();
            _dbSetMock.As<IQueryable<TestEntity>>().Setup(m => m.Provider).Returns(testData.Provider);
            _dbSetMock.As<IQueryable<TestEntity>>().Setup(m => m.Expression).Returns(testData.Expression);
            _dbSetMock.As<IQueryable<TestEntity>>().Setup(m => m.ElementType).Returns(testData.ElementType);
            _dbSetMock.As<IQueryable<TestEntity>>().Setup(m => m.GetEnumerator()).Returns(testData.GetEnumerator());

            // Set up the DbContext to return the mock DbSet
            _dbContextMock.Setup(context => context.Set<TestEntity>()).Returns(_dbSetMock.Object);

            // Use a test implementation of the abstract class
            _repository = new TestRead(_dbContextMock.Object);
        }

        private class TestRead : Read<TestEntity>
        {
            public TestRead(DbContext context) : base(context)
            {
            }
        }

        [Test]
        public async Task ReadFilter_Should_Return_Queryable_Matching_Entities()
        {
            // Arrange
            Expression<Func<TestEntity, bool>> predicate = x => x.Id > 1;

            // Act
            var result = await _repository.ReadFilter(predicate);

            // Assert
            result.Should().HaveCount(2);
            result.First().Id.Should().Be(2);
            result.Last().Id.Should().Be(3);
        }

        [Test]
        public async Task ReadCountFilter_Should_Return_Correct_Count()
        {
            // Arrange
            Expression<Func<TestEntity, bool>> predicate = x => x.Id > 1;

            // Act
            var count = await _repository.ReadCountFilter(predicate);

            // Assert
            count.Should().Be(2);
        }

        [Test]
        public async Task ReadPageByFilter_Should_Return_Correct_Paginated_Entities()
        {
            // Arrange
            Expression<Func<TestEntity, bool>> predicate = x => x.Id > 0;
            int pageNumber = 0;
            int pageSize = 2;

            // Act
            var pagedResult = await _repository.ReadPageByFilter(predicate, pageNumber, pageSize);

            // Assert
            pagedResult.Should().HaveCount(2);
            pagedResult.First().Id.Should().Be(1);
            pagedResult.Last().Id.Should().Be(2);
        }

        [Test]
        public async Task ReadPageByFilter_Should_Return_Second_Page()
        {
            // Arrange
            Expression<Func<TestEntity, bool>> predicate = x => x.Id > 0;
            int pageNumber = 1; // Second page
            int pageSize = 2;

            // Act
            var pagedResult = await _repository.ReadPageByFilter(predicate, pageNumber, pageSize);

            // Assert
            pagedResult.Should().HaveCount(1);
            pagedResult.First().Id.Should().Be(3);
        }
    }
}
