using FluentAssertions;
using MySql.EntityFrameworkCore.Metadata;
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
    public class ColumnTypesMySQLTests
    {
        private ColumnTypesMySQL _columnTypes;

        [SetUp]
        public void Setup()
        {
            _columnTypes = new ColumnTypesMySQL();
        }

        [Test]
        public void TypeBool_Should_Return_TinyInt_1()
        {
            // Act
            var result = _columnTypes.TypeBool;

            // Assert
            result.Should().Be("TINYINT(1)");
        }

        [Test]
        public void TypeTime_Should_Return_Datetime()
        {
            // Act
            var result = _columnTypes.TypeTime;

            // Assert
            result.Should().Be("DATETIME");
        }

        [Test]
        public void TypeVar_Should_Return_Varchar_100()
        {
            // Act
            var result = _columnTypes.TypeVar;

            // Assert
            result.Should().Be("VARCHAR(100)");
        }

        [Test]
        public void TypeVar50_Should_Return_Varchar_50()
        {
            // Act
            var result = _columnTypes.TypeVar50;

            // Assert
            result.Should().Be("VARCHAR(50)");
        }

        [Test]
        public void TypeVar64_Should_Return_Char_64()
        {
            // Act
            var result = _columnTypes.TypeVar64;

            // Assert
            result.Should().Be("char(64)");
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
        public void Integer_Should_Return_Int()
        {
            // Act
            var result = _columnTypes.Integer;

            // Assert
            result.Should().Be("INT");
        }

        [Test]
        public void Long_Should_Return_Bigint_Unsigned()
        {
            // Act
            var result = _columnTypes.Long;

            // Assert
            result.Should().Be("bigint unsigned");
        }

        [Test]
        public void Strategy_Should_Return_MySql_ValueGenerationStrategy()
        {
            // Act
            var result = _columnTypes.Strategy;

            // Assert
            result.Should().Be("MySql:ValueGenerationStrategy");
        }

        [Test]
        public void SqlStrategy_Should_Return_IdentityColumn()
        {
            // Act
            var result = _columnTypes.SqlStrategy;

            // Assert
            result.Should().Be(MySQLValueGenerationStrategy.IdentityColumn);
        }

        [Test]
        public void Name_Should_Return_MySQL_Charset()
        {
            // Act
            var result = _columnTypes.Name;

            // Assert
            result.Should().Be("MySQL:Charset");
        }

        [Test]
        public void Value_Should_Return_Utf8mb4()
        {
            // Act
            var result = _columnTypes.Value;

            // Assert
            result.Should().Be("utf8mb4");
        }
    }
}
