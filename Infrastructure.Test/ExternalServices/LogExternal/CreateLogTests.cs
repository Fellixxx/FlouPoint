using Domain.EnumType.LogLevel;
using Domain.EnumType.OperationExecute;
using Infrastructure.ExternalServices.LogExternal;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Test.ExternalServices.LogExternal
{
    [TestClass]
    public class CreateLogTests
    {
        [TestMethod]
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
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccessful);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(message, result.Data.Message);
            Assert.AreEqual("The Log validation of the OperationResult was successfully.", result.Message);
        }

        [TestMethod]
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
            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsSuccessful);
            Assert.AreEqual("The message or enitty was not submitted.", result.Message);
        }

        [TestMethod]
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
            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsSuccessful);
            Assert.AreEqual("The message or enitty was not submitted.", result.Message);
        }

        [TestMethod]
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
            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsSuccessful);
            StringAssert.Contains(result.Message, "Failed to serialize entity: Self referencing loop detected for property 'Object' with type 'Castle.Proxies.TestEntityProxy'. Path 'Mock'.");
        }

        [TestMethod]
        public void CreateLogIfValid_Should_Return_Failure_When_NullReferenceException_Occurs()
        {
            // Arrange
            var message = "Valid log message";
            var entity = new { Id = 1, Name = "TestEntity" };
            var operation = OperationExecute.Add;
            var level = LogLevel.Information;

            // Simular una excepción de referencia nula
            // Mock.Get(entity).Setup(e => e.ToString()).Throws(new NullReferenceException("Null reference encountered"));

            // Act
            var result = CreateLog.CreateLogIfValid(message, entity, operation, level);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccessful);
            StringAssert.Contains(result.Message, "The Log validation of the OperationResult was successfully.");
        }

        [TestMethod]
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
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccessful);
            StringAssert.Contains(result.Message, "The Log validation of the OperationResult was successfully.");
        }
    }

    // Assuming TestEntity is defined somewhere; including a basic definition for completeness
    public class TestEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
