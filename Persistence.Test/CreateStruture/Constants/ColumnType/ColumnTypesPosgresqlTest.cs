using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Persistence.CreateStruture.Constants.ColumnType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayerPersistence
{
    [TestClass]
    public class ColumnTypesPosgresqlTests
    {
        private ColumnTypesPosgresql _columnTypes;

        [TestInitialize]
        public void Setup()
        {
            _columnTypes = new ColumnTypesPosgresql();
        }

        [TestMethod]
        public void TypeBool_Should_Return_Boolean()
        {
            // Act
            var result = _columnTypes.TypeBool;

            // Assert
            Assert.AreEqual(result,"boolean");
        }

        [TestMethod]
        public void TypeTime_Should_Return_Timestamp_With_TimeZone()
        {
            // Act
            var result = _columnTypes.TypeTime;

            // Assert
            Assert.AreEqual(result,"TIMESTAMPTZ");
        }

        [TestMethod]
        public void TypeVar_Should_Return_Character_Varying_100()
        {
            // Act
            var result = _columnTypes.TypeVar;

            // Assert
            Assert.AreEqual(result,"character varying(100)");
        }

        [TestMethod]
        public void TypeVar50_Should_Return_Character_Varying_50()
        {
            // Act
            var result = _columnTypes.TypeVar50;

            // Assert
            Assert.AreEqual(result,"character varying(50)");
        }

        [TestMethod]
        public void TypeVar64_Should_Return_Char_64()
        {
            // Act
            var result = _columnTypes.TypeVar64;

            // Assert
            Assert.AreEqual(result,"character char(64)");
        }

        [TestMethod]
        public void TypeBlob_Should_Return_Blob()
        {
            // Act
            var result = _columnTypes.TypeBlob;

            // Assert
            Assert.AreEqual(result,"Blob");
        }

        [TestMethod]
        public void Integer_Should_Return_Integer()
        {
            // Act
            var result = _columnTypes.Integer;

            // Assert
            Assert.AreEqual(result,"integer");
        }

        [TestMethod]
        public void Long_Should_Return_Bigint_Unsigned_Default_Null()
        {
            // Act
            var result = _columnTypes.Long;

            // Assert
            Assert.AreEqual(result,"bigint unsigned DEFAULT NULL");
        }

        [TestMethod]
        public void Strategy_Should_Return_Npgsql_ValueGenerationStrategy()
        {
            // Act
            var result = _columnTypes.Strategy;

            // Assert
            Assert.AreEqual(result,"Npgsql:ValueGenerationStrategy");
        }

        [TestMethod]
        public void SqlStrategy_Should_Return_IdentityByDefaultColumn()
        {
            // Act
            var result = _columnTypes.SqlStrategy;

            // Assert
            Assert.AreEqual(result,NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);
        }

        [TestMethod]
        public void Name_Should_Return_PostgreSQL_Encoding()
        {
            // Act
            var result = _columnTypes.Name;

            // Assert
            Assert.AreEqual(result,"PostgreSQL:Encoding");
        }

        [TestMethod]
        public void Value_Should_Return_UTF8()
        {
            // Act
            var result = _columnTypes.Value;

            // Assert
            Assert.AreEqual(result,"UTF8");
        }
    }
}
