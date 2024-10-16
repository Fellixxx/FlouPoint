using FluentAssertions;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using NUnit.Framework;
using Persistence.CreateStruture.Constants.ColumnType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayerPersistence
{
    [TestFixture]
    public class ColumnTypesPosgresqlTests
    {
        private ColumnTypesPosgresql _columnTypes;

        [SetUp]
        public void Setup()
        {
            _columnTypes = new ColumnTypesPosgresql();
        }

        [Test]
        public void TypeBool_Should_Return_Boolean()
        {
            // Act
            var result = _columnTypes.TypeBool;

            // Assert
            result.Should().Be("boolean");
        }

        [Test]
        public void TypeTime_Should_Return_Timestamp_With_TimeZone()
        {
            // Act
            var result = _columnTypes.TypeTime;

            // Assert
            result.Should().Be("TIMESTAMPTZ");
        }

        [Test]
        public void TypeVar_Should_Return_Character_Varying_100()
        {
            // Act
            var result = _columnTypes.TypeVar;

            // Assert
            result.Should().Be("character varying(100)");
        }

        [Test]
        public void TypeVar50_Should_Return_Character_Varying_50()
        {
            // Act
            var result = _columnTypes.TypeVar50;

            // Assert
            result.Should().Be("character varying(50)");
        }

        [Test]
        public void TypeVar64_Should_Return_Char_64()
        {
            // Act
            var result = _columnTypes.TypeVar64;

            // Assert
            result.Should().Be("character char(64)");
        }

        [Test]
        public void TypeBlob_Should_Return_Blob()
        {
            // Act
            var result = _columnTypes.TypeBlob;

            // Assert
            result.Should().Be("Blob");
        }

        [Test]
        public void Integer_Should_Return_Integer()
        {
            // Act
            var result = _columnTypes.Integer;

            // Assert
            result.Should().Be("integer");
        }

        [Test]
        public void Long_Should_Return_Bigint_Unsigned_Default_Null()
        {
            // Act
            var result = _columnTypes.Long;

            // Assert
            result.Should().Be("bigint unsigned DEFAULT NULL");
        }

        [Test]
        public void Strategy_Should_Return_Npgsql_ValueGenerationStrategy()
        {
            // Act
            var result = _columnTypes.Strategy;

            // Assert
            result.Should().Be("Npgsql:ValueGenerationStrategy");
        }

        [Test]
        public void SqlStrategy_Should_Return_IdentityByDefaultColumn()
        {
            // Act
            var result = _columnTypes.SqlStrategy;

            // Assert
            result.Should().Be(NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);
        }

        [Test]
        public void Name_Should_Return_PostgreSQL_Encoding()
        {
            // Act
            var result = _columnTypes.Name;

            // Assert
            result.Should().Be("PostgreSQL:Encoding");
        }

        [Test]
        public void Value_Should_Return_UTF8()
        {
            // Act
            var result = _columnTypes.Value;

            // Assert
            result.Should().Be("UTF8");
        }
    }
}
