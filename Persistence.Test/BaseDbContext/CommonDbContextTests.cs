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
        private class TestCommonDbContext : CommonDbContext
        {
            public TestCommonDbContext(DbContextOptions options, IColumnTypes columnTypes, ILogger<CommonDbContext> logger) : base(options, columnTypes, logger)
            {
            }

            public void PublicOnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);
            }
        }

        private TestCommonDbContext _testClass;
        private DbContextOptions<CommonDbContext> _options;
        private IColumnTypes _columnTypes;
        private ILogger<CommonDbContext> _logger;

        [TestInitialize]
        public void SetUp()
        {
            _options = new DbContextOptionsBuilder<CommonDbContext>().Options;
            _columnTypes = Substitute.For<IColumnTypes>();
            _logger = Substitute.For<ILogger<CommonDbContext>>();
            _testClass = new TestCommonDbContext(_options, _columnTypes, _logger);
        }

        [TestMethod]
        public void CanConstruct()
        {
            // Act
            var instance = new TestCommonDbContext(_options, _columnTypes, _logger);

            // Assert
            Assert.IsNotNull(instance);
        }

        [TestMethod]
        public void CannotConstructWithNullOptions()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new TestCommonDbContext(default(DbContextOptions<CommonDbContext>), _columnTypes, _logger));
        }

        [TestMethod]
        public void CanSetAndGetLists()
        {
            // Arrange
            var testValue = Substitute.For<DbSet<ResourceEntry>>();

            // Act
            _testClass.Lists = testValue;

            // Assert
            Assert.AreSame(testValue, _testClass.Lists);
        }
    }
}