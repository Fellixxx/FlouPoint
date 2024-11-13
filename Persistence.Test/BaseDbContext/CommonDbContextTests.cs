namespace Persistence.Test.BaseDbContext
{
    using System;
    using Domain.Entities;
    using global::Persistence.BaseDbContext;
    using global::Persistence.CreateStruture.Constants.ColumnType;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NSubstitute;
    using TContext = System.String;

    [TestClass]
    public class CommonDbContextTests
    {
        private class TestCommonDbContext : DataContext
        {
            public TestCommonDbContext(DbContextOptions options, IColumnTypes columnTypes) : base(options, columnTypes)
            {
            }

            public void PublicOnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);
            }
        }

        private TestCommonDbContext _testClass;
        private DbContextOptions<DataContext> _options;
        private IColumnTypes _columnTypes;
        private ILogger<DataContext> _logger;

        [TestInitialize]
        public void SetUp()
        {
            _options = new DbContextOptionsBuilder<DataContext>().Options;
            _columnTypes = Substitute.For<IColumnTypes>();
            _logger = Substitute.For<ILogger<DataContext>>();
            _testClass = new TestCommonDbContext(_options, _columnTypes);
        }

        [TestMethod]
        public void CanConstruct()
        {
            // Act
            var instance = new TestCommonDbContext(_options, _columnTypes);

            // Assert
            Assert.IsNotNull(instance);
        }

        [TestMethod]
        public void CannotConstructWithNullOptions()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new TestCommonDbContext(default(DbContextOptions<DataContext>), _columnTypes));
        }

        [TestMethod]
        public void CanSetAndGetLists()
        {
            // Arrange
            var testValue = Substitute.For<DbSet<Resource>>();

            // Act
            _testClass.Lists = testValue;

            // Assert
            Assert.AreSame(testValue, _testClass.Lists);
        }
    }
}