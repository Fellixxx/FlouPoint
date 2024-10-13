namespace FlouPoint.LayerInfrastructure.Test.ExternalServices.LogExternal
{
    using Application.Result;
    using Domain.DTO.Log;
    using Domain.EnumType.LogLevel;
    using Domain.EnumType.OperationExecute;
    using FluentAssertions;
    using Infrastructure.ExternalServices.LogExternal;
    using Moq;
    using Newtonsoft.Json;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class CreateLogTests
    {
        [Test]
        public void CreateLogIfValid_Should_Return_Successful_Log_When_Valid_Data_Provided()
        {
            // Arrange
            var message = "Test log message";
            var entity = new { Id = 1, Name = "TestEntity" };
            var operation = OperationExecute.Add;
            var level = LogLevel.Information;

            // Act
            var result = CreateLog.CreateLogIfValid(message, entity, operation, level);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccessful.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data.Message.Should().Be(message);
            result.Data.EntityName.Should().Be("AnonymousType");
            result.Data.Level.Should().Be("Information");
            result.Data.Operation.Should().Be("Add");
        }

        [Test]
        public void CreateLogIfValid_Should_Return_Failure_When_Message_Is_Null_Or_Empty()
        {
            // Arrange
            string nullMessage = null;
            var entity = new { Id = 1, Name = "TestEntity" };
            var operation = OperationExecute.Add;
            var level = LogLevel.Information;

            // Act
            var result = CreateLog.CreateLogIfValid(nullMessage, entity, operation, level);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccessful.Should().BeFalse();
        }

        [Test]
        public void CreateLogIfValid_Should_Return_Failure_When_Entity_Is_Null()
        {
            // Arrange
            var message = "Test log message";
            object nullEntity = null;
            var operation = OperationExecute.Add;
            var level = LogLevel.Information;

            // Act
            var result = CreateLog.CreateLogIfValid(message, nullEntity, operation, level);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccessful.Should().BeFalse();
        }

        [Test]
        public void CreateLogIfValid_Should_Handle_JsonSerializationException_Gracefully()
        {
            // Arrange
            var message = "Test log message";
            var entity = new { Id = 1, Name = "TestEntity" };
            var operation = OperationExecute.Add;
            var level = LogLevel.Information;


            // Act
            var result = CreateLog.CreateLogIfValid(message, entity, operation, level);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccessful.Should().BeFalse();
            result.Message.Should().Contain("Failed to serialize entity");
        }

        [Test]
        public void CreateLogIfValid_Should_Handle_NullReferenceException_Gracefully()
        {
            // Arrange
            var message = "Test log message";
            var entity = new { Id = 1, Name = "TestEntity" };
            var operation = OperationExecute.Add;
            var level = LogLevel.Information;


            // Act
            var result = CreateLog.CreateLogIfValid(message, entity, operation, level);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccessful.Should().BeFalse();
            result.Message.Should().Contain("Null reference encountered");
        }

        [Test]
        public void CreateLogIfValid_Should_Handle_General_Exception_Gracefully()
        {
            // Arrange
            var message = "Test log message";
            var entity = new { Id = 1, Name = "TestEntity" };
            var operation = OperationExecute.Add;
            var level = LogLevel.Information;

            // Act
            var result = CreateLog.CreateLogIfValid(message, entity, operation, level);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccessful.Should().BeFalse();
            result.Message.Should().Contain("An unexpected error occurred");
        }
    }
}
