namespace Persistence.Test.CreateStruture.Constants.ColumnType
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MySql.EntityFrameworkCore.Metadata;
    using Persistence.CreateStruture.Constants.ColumnType;

    [TestClass]
    public class ColumnTypesMySQLTests
    {
        private ColumnTypesMySQL _columnTypes;

        [TestInitialize]
        public void Setup()
        {
            _columnTypes = new ColumnTypesMySQL();
        }

        [TestMethod]
        public void TypeBool_Should_Return_TinyInt_1()
        {
            // Act
            var result = _columnTypes.TypeBool;

            // Assert
            Assert.AreEqual(result, "TINYINT(1)");
        }

        [TestMethod]
        public void TypeTime_Should_Return_Datetime()
        {
            // Act
            var result = _columnTypes.TypeTime;

            // Assert
            Assert.AreEqual(result, "DATETIME");
        }

        [TestMethod]
        public void TypeVar_Should_Return_Varchar_100()
        {
            // Act
            var result = _columnTypes.TypeVar;

            // Assert
            Assert.AreEqual(result, "VARCHAR(100)");
        }

        [TestMethod]
        public void TypeVar50_Should_Return_Varchar_50()
        {
            // Act
            var result = _columnTypes.TypeVar50;

            // Assert
            Assert.AreEqual(result, "VARCHAR(50)");
        }

        [TestMethod]
        public void TypeVar64_Should_Return_Char_64()
        {
            // Act
            var result = _columnTypes.TypeVar64;

            // Assert
            Assert.AreEqual(result, "char(64)");
        }

        [TestMethod]
        public void TypeBlob_Should_Return_Blob()
        {
            // Act
            var result = _columnTypes.TypeBlob;

            // Assert
            Assert.AreEqual(result, "Blob");
        }

        [TestMethod]
        public void Integer_Should_Return_Int()
        {
            // Act
            var result = _columnTypes.Integer;

            // Assert
            Assert.AreEqual(result, "INT");
        }

        [TestMethod]
        public void Long_Should_Return_Bigint_Unsigned()
        {
            // Act
            var result = _columnTypes.Long;

            // Assert
            Assert.AreEqual(result, "bigint unsigned");
        }

        [TestMethod]
        public void Strategy_Should_Return_MySql_ValueGenerationStrategy()
        {
            // Act
            var result = _columnTypes.Strategy;

            // Assert
            Assert.AreEqual(result, "MySql:ValueGenerationStrategy");
        }

        [TestMethod]
        public void SqlStrategy_Should_Return_IdentityColumn()
        {
            // Act
            var result = _columnTypes.SqlStrategy;

            // Assert
            Assert.AreEqual(result, MySQLValueGenerationStrategy.IdentityColumn);
        }

        [TestMethod]
        public void Name_Should_Return_MySQL_Charset()
        {
            // Act
            var result = _columnTypes.Name;

            // Assert
            Assert.AreEqual(result, "MySQL:Charset");
        }

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
