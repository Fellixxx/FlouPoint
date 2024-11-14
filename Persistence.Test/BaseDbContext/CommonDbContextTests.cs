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

    /// <summary>
    /// Tests for CommonDbContext functionality.
    /// </summary>
    [TestClass]
    public class CommonDbContextTests
    {
        /// <summary>
        /// A testable subclass of DataContext to expose protected methods for testing.
        /// </summary>
        private class TestCommonDbContext : DataContext
        {
            public TestCommonDbContext(DbContextOptions options, IColumnTypes columnTypes) : base(options, columnTypes)
            {
            }

            /// <summary>
            /// Wrapper method for protected OnModelCreating to be tested individually.
            /// </summary>
            public void PublicOnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);
            }
        }

        private TestCommonDbContext _testContext;
        private DbContextOptions<DataContext> _options;
        private IColumnTypes _columnTypes;
        private ILogger<DataContext> _logger;
        /// <summary>
        /// Initializes test dependencies before each test.
        /// </summary>
        [TestInitialize]
        public void InitializeTestDependencies()
        {
            _options = new DbContextOptionsBuilder<DataContext>().Options;
            _columnTypes = Substitute.For<IColumnTypes>();
            _logger = Substitute.For<ILogger<DataContext>>();
            _testContext = new TestCommonDbContext(_options, _columnTypes);
        }

        /// <summary>
        /// Tests if CommonDbContext can be constructed successfully.
        /// </summary>
        [TestMethod]
        public void Constructor_InitializesInstanceSuccessfully()
        {
            // Act
            var instance = new TestCommonDbContext(_options, _columnTypes);
            // Assert
            Assert.IsNotNull(instance);
        }

        /// <summary>
        /// Tests that constructing CommonDbContext with null options throws an ArgumentNullException.
        /// </summary>
        [TestMethod]
        public void Constructor_ThrowsArgumentNullExceptionWhenOptionsIsNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new TestCommonDbContext(default(DbContextOptions<DataContext>), _columnTypes));
        }

        /// <summary>
        /// Tests if Lists property can be set and retrieved as expected.
        /// </summary>
        [TestMethod]
        public void ListsProperty_CanBeSetAndRetrievedSuccessfully()
        {
            // Arrange
            var testValue = Substitute.For<DbSet<Resource>>();
            // Act
            _testContext.Lists = testValue;
            // Assert
            Assert.AreSame(testValue, _testContext.Lists);
        }
    }
}