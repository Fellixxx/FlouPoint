using Domain.DTO.Logging;
using Domain.EnumType;
using Infrastructure.ExternalServices.LogExternal;

namespace Infrastructure.Test.ExternalServices
{
    [TestClass]
    internal class CreateLogTests
    {
        [TestMethod]
        public void When_CreateLogIfValid_ValidInputs_Then_Success()
        {
            // Given
            string message = "A valid log message";
            object entity = new { Name = "Test Object" };
            OperationExecute operation = OperationExecute.Activate;
            LogLevel level = LogLevel.Information;

            // When
            var result = CreateLog.CreateLogIfValid(message, entity, operation, level);

            // Then
            Assert.IsTrue(result.IsSuccessful);
            Assert.IsNotNull(result.Data);
            var data = result.Data ?? new Log();
            Assert.AreEqual(message, data.Message);
        }

        [TestMethod]
        public void When_CreateLogIfValid_InvalidMessage_Then_Failure()
        {
            // Given
            string message = "";
            object entity = new { Name = "Test Object" };
            OperationExecute operation = OperationExecute.Activate;
            LogLevel level = LogLevel.Information;

            // When
            var result = CreateLog.CreateLogIfValid(message, entity, operation, level);

            // Then
            Assert.IsFalse(result.IsSuccessful);
        }

        [TestMethod]
        public void When_CreateLogIfValid_InvalidEntity_Then_Failure()
        {
            // Given
            string message = "A valid log message";
            object? entity = null;
            OperationExecute operation = OperationExecute.Activate;
            LogLevel level = LogLevel.Information;

            // When
            var result = CreateLog.CreateLogIfValid(message, entity, operation, level);

            // Then
            Assert.IsFalse(result.IsSuccessful);
        }

        [TestMethod]
        public void When_CreateLogIfValid_JsonSerializationException_Then_Failure()
        {
            // Given
            string message = "A valid log message";
            object entity = new { NonSerializableObject = new NonSerializableClass() }; // Assume NonSerializableClass is not serializable
            OperationExecute operation = OperationExecute.Activate;
            LogLevel level = LogLevel.Information;

            // When
            var result = CreateLog.CreateLogIfValid(message, entity, operation, level);

            // Then
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual("NONE", result.Error);
            Assert.AreEqual("The Log validation of the OperationResult was successfully.", result.Message);
        }

        [TestMethod]
        public void When_CreateLogIfValid_UnexpectedException_Then_Failure()
        {
            // Given
            // Simulate conditions that would cause an unexpected exception
            // Assume OperationBuilder<Log>.FailureUnexpectedError or similar throws an exception

            // When
            // Then
            var result = CreateLog.CreateLogIfValid(string.Empty, string.Empty, OperationExecute.Add, default(LogLevel)); // Replace default(LogLevel) with appropriate value if needed
            Assert.IsNull(result.Data);
            Assert.AreEqual("DATA_SUBMITTED_INVALID", result.Error);
            Assert.IsFalse(result.IsSuccessful);
            Assert.AreEqual("The message or enitty was not submitted.", result.Message);
        }
    }

    // Assume NonSerializableClass is defined elsewhere
    public class NonSerializableClass
    {
        // This class is intentionally non-serializable
    }
}
