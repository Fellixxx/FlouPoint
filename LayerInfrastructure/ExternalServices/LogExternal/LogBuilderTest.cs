using Domain.EnumType.LogLevel;
using Domain.EnumType.OperationExecute;
using FluentAssertions;
using Infrastructure.ExternalServices.LogExternal;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayerInfrastructure.ExternalServices.LogExternal
{
    [TestFixture]
    public class LogBuilderTests
    {
        private LogBuilder _logBuilder;

        [SetUp]
        public void SetUp()
        {
            // Usamos el singleton de LogBuilder
            _logBuilder = LogBuilder.GetLogBuilder();
        }

        [Test]
        public void GetLogBuilder_Should_Return_Singleton_Instance()
        {
            // Act
            var instance1 = LogBuilder.GetLogBuilder();
            var instance2 = LogBuilder.GetLogBuilder();

            // Assert
            instance1.Should().BeSameAs(instance2);  // Verifica que ambas instancias son la misma
        }

        [Test]
        public void Trace_Should_Create_Trace_Level_Log()
        {
            // Arrange
            var message = "Trace log message";
            var entity = new { Id = 1, Name = "TestEntity" };
            var operation = OperationExecute.Add;

            // Act
            var result = _logBuilder.Trace(message, entity, operation);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccessful.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data.Message.Should().Be(message);
            result.Data.Level.Should().Be(LogLevel.Trace.ToString());
        }

        [Test]
        public void Debug_Should_Create_Debug_Level_Log()
        {
            // Arrange
            var message = "Debug log message";
            var entity = new { Id = 1, Name = "TestEntity" };
            var operation = OperationExecute.Add;

            // Act
            var result = _logBuilder.Debug(message, entity, operation);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccessful.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data.Message.Should().Be(message);
            result.Data.Level.Should().Be(LogLevel.Debug.ToString());
        }

        [Test]
        public void Information_Should_Create_Information_Level_Log()
        {
            // Arrange
            var message = "Information log message";
            var entity = new { Id = 1, Name = "TestEntity" };
            var operation = OperationExecute.Add;

            // Act
            var result = _logBuilder.Information(message, entity, operation);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccessful.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data.Message.Should().Be(message);
            result.Data.Level.Should().Be(LogLevel.Information.ToString());
        }

        [Test]
        public void Warning_Should_Create_Warning_Level_Log()
        {
            // Arrange
            var message = "Warning log message";
            var entity = new { Id = 1, Name = "TestEntity" };
            var operation = OperationExecute.Add;

            // Act
            var result = _logBuilder.Warning(message, entity, operation);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccessful.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data.Message.Should().Be(message);
            result.Data.Level.Should().Be(LogLevel.Warning.ToString());
        }

        [Test]
        public void Error_Should_Create_Error_Level_Log()
        {
            // Arrange
            var message = "Error log message";
            var entity = new { Id = 1, Name = "TestEntity" };
            var operation = OperationExecute.Add;

            // Act
            var result = _logBuilder.Error(message, entity, operation);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccessful.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data.Message.Should().Be(message);
            result.Data.Level.Should().Be(LogLevel.Error.ToString());
        }

        [Test]
        public void Fatal_Should_Create_Fatal_Level_Log()
        {
            // Arrange
            var message = "Fatal log message";
            var entity = new { Id = 1, Name = "TestEntity" };
            var operation = OperationExecute.Add;

            // Act
            var result = _logBuilder.Fatal(message, entity, operation);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccessful.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data.Message.Should().Be(message);
            result.Data.Level.Should().Be(LogLevel.Fatal.ToString());
        }
    }
}
