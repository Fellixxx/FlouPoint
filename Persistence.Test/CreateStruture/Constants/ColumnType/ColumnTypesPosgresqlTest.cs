using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Persistence.CreateStruture.Constants.ColumnType;

namespace Persistence.Test.CreateStruture.Constants.ColumnType
{
    /// <summary>
    /// Unit tests for verifying PostgreSQL column types using the ColumnTypesPosgresql class.
    /// </summary>
    [TestClass]
    public class ColumnTypesPosgresqlTests
    {
        private ColumnTypesPosgresql _columnTypes;
        /// <summary>
        /// Initializes the test setup by creating an instance of ColumnTypesPosgresql.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            _columnTypes = new ColumnTypesPosgresql();
        }

        /// <summary>
        /// Tests if the TypeBool property returns "boolean" as the column type.
        /// </summary>
        [TestMethod]
        public void TypeBool_Should_Return_Boolean()
        {
            // Act
            var result = _columnTypes.TypeBool;
            // Assert
            Assert.AreEqual(result, "boolean");
        }

        /// <summary>
        /// Tests if the TypeTime property returns "TIMESTAMPTZ" as the column type.
        /// </summary>
        [TestMethod]
        public void TypeTime_Should_Return_Timestamp_With_TimeZone()
        {
            // Act
            var result = _columnTypes.TypeTime;
            // Assert
            Assert.AreEqual(result, "TIMESTAMPTZ");
        }

        /// <summary>
        /// Tests if the TypeVar property returns "character varying(100)" as the column type.
        /// </summary>
        [TestMethod]
        public void TypeVar_Should_Return_Character_Varying_100()
        {
            // Act
            var result = _columnTypes.TypeVar;
            // Assert
            Assert.AreEqual(result, "character varying(100)");
        }

        /// <summary>
        /// Tests if the TypeVar50 property returns "character varying(50)" as the column type.
        /// </summary>
        [TestMethod]
        public void TypeVar50_Should_Return_Character_Varying_50()
        {
            // Act
            var result = _columnTypes.TypeVar50;
            // Assert
            Assert.AreEqual(result, "character varying(50)");
        }

        /// <summary>
        /// Tests if the TypeVar64 property returns "character char(64)" as the column type.
        /// </summary>
        [TestMethod]
        public void TypeVar64_Should_Return_Char_64()
        {
            // Act
            var result = _columnTypes.TypeVar64;
            // Assert
            Assert.AreEqual(result, "character char(64)");
        }

        /// <summary>
        /// Tests if the TypeBlob property returns "Blob" as the column type.
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
        /// Tests if the Integer property returns "integer" as the column type.
        /// </summary>
        [TestMethod]
        public void Integer_Should_Return_Integer()
        {
            // Act
            var result = _columnTypes.Integer;
            // Assert
            Assert.AreEqual(result, "integer");
        }

        /// <summary>
        /// Tests if the Long property returns "bigint unsigned DEFAULT NULL" as the column type.
        /// </summary>
        [TestMethod]
        public void Long_Should_Return_Bigint_Unsigned_Default_Null()
        {
            // Act
            var result = _columnTypes.Long;
            // Assert
            Assert.AreEqual(result, "bigint unsigned DEFAULT NULL");
        }

        /// <summary>
        /// Tests if the Strategy property returns "Npgsql:ValueGenerationStrategy" as the column type.
        /// </summary>
        [TestMethod]
        public void Strategy_Should_Return_Npgsql_ValueGenerationStrategy()
        {
            // Act
            var result = _columnTypes.Strategy;
            // Assert
            Assert.AreEqual(result, "Npgsql:ValueGenerationStrategy");
        }

        /// <summary>
        /// Tests if the SqlStrategy property returns IdentityByDefaultColumn strategy.
        /// </summary>
        [TestMethod]
        public void SqlStrategy_Should_Return_IdentityByDefaultColumn()
        {
            // Act
            var result = _columnTypes.SqlStrategy;
            // Assert
            Assert.AreEqual(result, NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);
        }

        /// <summary>
        /// Tests if the Name property returns "PostgreSQL:Encoding".
        /// </summary>
        [TestMethod]
        public void Name_Should_Return_PostgreSQL_Encoding()
        {
            // Act
            var result = _columnTypes.Name;
            // Assert
            Assert.AreEqual(result, "PostgreSQL:Encoding");
        }

        /// <summary>
        /// Tests if the Value property returns "UTF8".
        /// </summary>
        [TestMethod]
        public void Value_Should_Return_UTF8()
        {
            // Act
            var result = _columnTypes.Value;
            // Assert
            Assert.AreEqual(result, "UTF8");
        }
    }
}