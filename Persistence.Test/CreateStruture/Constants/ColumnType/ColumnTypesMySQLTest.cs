namespace Persistence.Test.CreateStruture.Constants.ColumnType
{
    using global::Persistence.CreateStruture.Constants.ColumnType;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MySql.EntityFrameworkCore.Metadata;

    /// <summary>
    /// Contains unit tests for the <see cref = "ColumnTypesMySQL"/> class, 
    /// which represents different MySQL column types.
    /// </summary>
    [TestClass]
    public class ColumnTypesMySQLTests
    {
        private ColumnTypesMySQL _columnTypes;
        /// <summary>
        /// Initializes the test class by creating an instance of <see cref = "ColumnTypesMySQL"/>.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            _columnTypes = new ColumnTypesMySQL();
        }

        /// <summary>
        /// Tests whether the <see cref = "ColumnTypesMySQL.TypeBool"/> property
        /// returns "TINYINT(1)" for representing boolean values in MySQL.
        /// </summary>
        [TestMethod]
        public void TypeBool_Should_Return_TinyInt_1()
        {
            // Act
            var result = _columnTypes.TypeBool;
            // Assert
            Assert.AreEqual(result, "TINYINT(1)");
        }

        /// <summary>
        /// Tests whether the <see cref = "ColumnTypesMySQL.TypeTime"/> property
        /// returns "DATETIME" to represent time values in MySQL.
        /// </summary>
        [TestMethod]
        public void TypeTime_Should_Return_Datetime()
        {
            // Act
            var result = _columnTypes.TypeTime;
            // Assert
            Assert.AreEqual(result, "DATETIME");
        }

        /// <summary>
        /// Tests whether the <see cref = "ColumnTypesMySQL.TypeVar"/> property
        /// returns "VARCHAR(100)" for variable-length string storage in MySQL.
        /// </summary>
        [TestMethod]
        public void TypeVar_Should_Return_Varchar_100()
        {
            // Act
            var result = _columnTypes.TypeVar;
            // Assert
            Assert.AreEqual(result, "VARCHAR(100)");
        }

        /// <summary>
        /// Tests whether the <see cref = "ColumnTypesMySQL.TypeVar50"/> property
        /// returns "VARCHAR(50)" for variable-length string storage, limited to 50 characters.
        /// </summary>
        [TestMethod]
        public void TypeVar50_Should_Return_Varchar_50()
        {
            // Act
            var result = _columnTypes.TypeVar50;
            // Assert
            Assert.AreEqual(result, "VARCHAR(50)");
        }

        /// <summary>
        /// Tests whether the <see cref = "ColumnTypesMySQL.TypeVar64"/> property
        /// returns "char(64)" for fixed-length string storage of 64 characters.
        /// </summary>
        [TestMethod]
        public void TypeVar64_Should_Return_Char_64()
        {
            // Act
            var result = _columnTypes.TypeVar64;
            // Assert
            Assert.AreEqual(result, "char(64)");
        }

        /// <summary>
        /// Tests whether the <see cref = "ColumnTypesMySQL.TypeBlob"/> property
        /// returns "Blob" for binary large objects in MySQL.
        /// </summary>
        [TestMethod]
        public void TypeBlob_Should_Return_Blob()
        {
            // Act
            var result = _columnTypes.TypeBlob;
            // Assert
            Assert.AreEqual(result, "Blob");
        }

        /// <summary>
        /// Tests whether the <see cref = "ColumnTypesMySQL.Integer"/> property
        /// returns "INT" for integer number types in MySQL.
        /// </summary>
        [TestMethod]
        public void Integer_Should_Return_Int()
        {
            // Act
            var result = _columnTypes.Integer;
            // Assert
            Assert.AreEqual(result, "INT");
        }

        /// <summary>
        /// Tests whether the <see cref = "ColumnTypesMySQL.Long"/> property
        /// returns "bigint unsigned" for large unsigned integer types in MySQL.
        /// </summary>
        [TestMethod]
        public void Long_Should_Return_Bigint_Unsigned()
        {
            // Act
            var result = _columnTypes.Long;
            // Assert
            Assert.AreEqual(result, "bigint unsigned");
        }

        /// <summary>
        /// Tests whether the <see cref = "ColumnTypesMySQL.Strategy"/> property
        /// returns "MySql:ValueGenerationStrategy" indicating the strategy used for value generation.
        /// </summary>
        [TestMethod]
        public void Strategy_Should_Return_MySql_ValueGenerationStrategy()
        {
            // Act
            var result = _columnTypes.Strategy;
            // Assert
            Assert.AreEqual(result, "MySql:ValueGenerationStrategy");
        }

        /// <summary>
        /// Tests whether the <see cref = "ColumnTypesMySQL.SqlStrategy"/> property
        /// returns <see cref = "MySQLValueGenerationStrategy.IdentityColumn"/> as the SQL strategy.
        /// </summary>
        [TestMethod]
        public void SqlStrategy_Should_Return_IdentityColumn()
        {
            // Act
            var result = _columnTypes.SqlStrategy;
            // Assert
            Assert.AreEqual(result, MySQLValueGenerationStrategy.IdentityColumn);
        }

        /// <summary>
        /// Tests whether the <see cref = "ColumnTypesMySQL.Name"/> property
        /// returns "MySQL:Charset" to represent the character set used in MySQL.
        /// </summary>
        [TestMethod]
        public void Name_Should_Return_MySQL_Charset()
        {
            // Act
            var result = _columnTypes.Name;
            // Assert
            Assert.AreEqual(result, "MySQL:Charset");
        }

        /// <summary>
        /// Tests whether the <see cref = "ColumnTypesMySQL.Value"/> property
        /// returns "utf8mb4" as the character encoding value for MySQL columns.
        /// </summary>
        [TestMethod]
        public void Value_Should_Return_Utf8mb4()
        {
            // Act
            var result = _columnTypes.Value;
            // Assert
            Assert.AreEqual(result, "utf8mb4");
        }
    }
}