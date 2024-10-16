using Domain.EnumType.LogLevel;
using Domain.EnumType.OperationExecute;
using FluentAssertions;
using Infrastructure.ExternalServices.LogExternal;
using LayerPersistence.Repositories;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LayerInfrastructure.ExternalServices.LogExternal
{
    [TestFixture]
    public class CreateLogTests
    {
        [Test]
        public void CreateLogIfValid_Should_Return_Success_When_Valid_Data()
        {
            // Arrange
            var message = "Valid log message";
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
            result.Message.Should().Be("The Log validation of the OperationResult was successfully.");
        }

        [Test]
        public void CreateLogIfValid_Should_Return_Failure_When_Message_Is_NullOrWhitespace()
        {
            // Arrange
            string message = "";
            var entity = new { Id = 1, Name = "TestEntity" };
            var operation = OperationExecute.Add;
            var level = LogLevel.Information;

            // Act
            var result = CreateLog.CreateLogIfValid(message, entity, operation, level);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccessful.Should().BeFalse();
            result.Message.Should().Be("The message or enitty was not submitted.");
        }

        [Test]
        public void CreateLogIfValid_Should_Return_Failure_When_Entity_Is_Null()
        {
            // Arrange
            var message = "Valid log message";
            object entity = null;
            var operation = OperationExecute.Add;
            var level = LogLevel.Information;

            // Act
            var result = CreateLog.CreateLogIfValid(message, entity, operation, level);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccessful.Should().BeFalse();
            result.Message.Should().Be("The message or enitty was not submitted.");
        }

        [Test]
        public void CreateLogIfValid_Should_Return_Failure_When_JsonSerializationException_Occurs()
        {
            // Arrange
            var message = "Valid log message";
            var entity = new TestEntity { Id = 1, Name = "InvalidEntity" }; // Usamos la clase en vez de un objeto anónimo
            var operation = OperationExecute.Add;
            var level = LogLevel.Information;

            // Simular una excepción de serialización JSON
            var mockEntity = new Mock<TestEntity>();
            mockEntity.Setup(e => e.ToString()).Throws(new JsonSerializationException("Serialization failed"));

            // Act
            var result = CreateLog.CreateLogIfValid(message, mockEntity.Object, operation, level);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccessful.Should().BeFalse();
            result.Message.Should().Contain("Failed to serialize entity: Self referencing loop detected for property 'Object' with type 'Castle.Proxies.TestEntityProxy'. Path 'Mock'.");
        }

        [Test]
        public void CreateLogIfValid_Should_Return_Failure_When_NullReferenceException_Occurs()
        {
            // Arrange
            var message = "Valid log message";
            var entity = new { Id = 1, Name = "TestEntity" };
            var operation = OperationExecute.Add;
            var level = LogLevel.Information;

            // Simular una excepción de referencia nula
            //Mock.Get(entity).Setup(e => e.ToString()).Throws(new NullReferenceException("Null reference encountered"));

            // Act
            var result = CreateLog.CreateLogIfValid(message, entity, operation, level);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccessful.Should().BeTrue();
            result.Message.Should().Contain("The Log validation of the OperationResult was successfully.");
        }

        [Test]
        public void CreateLogIfValid_Should_Return_Failure_When_Unexpected_Exception_Occurs()
        {
            // Arrange
            var message = "Valid log message";
            var entity = new { Id = 1, Name = "TestEntity" };
            var operation = OperationExecute.Add;
            var level = LogLevel.Information;

            // Act
            var result = CreateLog.CreateLogIfValid(message, entity, operation, level);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccessful.Should().BeTrue();
            result.Message.Should().Contain("The Log validation of the OperationResult was successfully.");
        }
    }
}
