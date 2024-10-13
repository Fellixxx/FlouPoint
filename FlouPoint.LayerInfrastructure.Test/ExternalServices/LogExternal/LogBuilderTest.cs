namespace FlouPoint.LayerInfrastructure.Test.ExternalServices.LogExternal
{
    using Application.Result;
    using Application.Result.Error;
    using Application.UseCases.ExternalServices;
    using Domain.DTO.Log;
    using Domain.EnumType.LogLevel;
    using Domain.EnumType.OperationExecute;
    using FluentAssertions;
    using global::Infrastructure.ExternalServices.LogExternal;
    using Infrastructure.ExternalServices.LogExternal;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class LogBuilderTests
    {
        private Mock<ILogBuilder<Log>> _mockLogBuilder;
        private object _testEntity;

        [SetUp]
        public void SetUp()
        {
            _mockLogBuilder = new Mock<ILogBuilder<Log>>();
            _testEntity = new { Id = 1, Name = "TestEntity" };
        }

        [Test]
        public void GetLogBuilder_Should_Return_Singleton_Instance()
        {
            // Act
            var logBuilder1 = LogBuilder.GetLogBuilder();
            var logBuilder2 = LogBuilder.GetLogBuilder();

            // Assert
            logBuilder1.Should().BeSameAs(logBuilder2);
        }

        [Test]
        public void Trace_Should_Create_Trace_Level_Log()
        {
            // Arrange
            var message = "Trace log message";
            var operation = OperationExecute.Add;

            // Act
            var result = LogBuilder.GetLogBuilder().Trace(message, _testEntity, operation);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccessful.Should().BeTrue();
            result.Data.Level.Should().Be("Trace");
        }

        [Test]
        public void Debug_Should_Create_Debug_Level_Log()
        {
            // Arrange
            var message = "Debug log message";
            var operation = OperationExecute.Add;

            // Act
            var result = LogBuilder.GetLogBuilder().Debug(message, _testEntity, operation);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccessful.Should().BeTrue();
            result.Data.Level.Should().Be("Debug");
        }

        [Test]
        public void Information_Should_Create_Information_Level_Log()
        {
            // Arrange
            var message = "Information log message";
            var operation = OperationExecute.Add;

            // Act
            var result = LogBuilder.GetLogBuilder().Information(message, _testEntity, operation);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccessful.Should().BeTrue();
            result.Data.Level.Should().Be("Information");
        }

        [Test]
        public void Warning_Should_Create_Warning_Level_Log()
        {
            // Arrange
            var message = "Warning log message";
            var operation = OperationExecute.Add;

            // Act
            var result = LogBuilder.GetLogBuilder().Warning(message, _testEntity, operation);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccessful.Should().BeTrue();
            result.Data.Level.Should().Be("Warning");
        }

        [Test]
        public void Error_Should_Create_Error_Level_Log()
        {
            // Arrange
            var message = "Error log message";
            var operation = OperationExecute.Add;

            // Act
            var result = LogBuilder.GetLogBuilder().Error(message, _testEntity, operation);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccessful.Should().BeTrue();
            result.Data.Level.Should().Be("Error");
        }

        [Test]
        public void Fatal_Should_Create_Fatal_Level_Log()
        {
            // Arrange
            var message = "Fatal log message";
            var operation = OperationExecute.Add;

            // Act
            var result = LogBuilder.GetLogBuilder().Fatal(message, _testEntity, operation);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccessful.Should().BeTrue();
            result.Data.Level.Should().Be("Fatal");
        }

        [Test]
        public void LogBuilder_Should_Return_Failure_If_Message_Is_Invalid()
        {
            // Arrange
            string nullMessage = null;
            var operation = OperationExecute.Add;

            // Act
            var result = LogBuilder.GetLogBuilder().Trace(nullMessage, _testEntity, operation);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccessful.Should().BeFalse();
        }

        [Test]
        public void LogBuilder_Should_Return_Failure_If_Entity_Is_Null()
        {
            // Arrange
            var message = "Trace log message";
            object nullEntity = null;
            var operation = OperationExecute.Add;

            // Act
            var result = LogBuilder.GetLogBuilder().Trace(message, nullEntity, operation);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccessful.Should().BeFalse();
        }
    }
}

