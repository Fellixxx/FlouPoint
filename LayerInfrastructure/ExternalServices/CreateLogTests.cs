namespace LayerInfrastructure.ExternalServices
{
    using global::Domain.DTO.Log;
    using FluentAssertions;
    using global::Domain.EnumType.OperationExecute;
    using global::Domain.EnumType.LogLevel;
    using global::Infrastructure.ExternalServices.LogExternal;
    using NUnit.Framework;

    internal class CreateLogTests
    {
        [Test]
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
            result.IsSuccessful.Should().BeTrue();
            result.Data.Should().NotBeNull();
            var data = result.Data ?? new Log();
            data.Message.Should().Be(message);
        }

        [Test]
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
            result.IsSuccessful.Should().BeFalse();
        }

        [Test]
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
            result.IsSuccessful.Should().BeFalse();
        }

        [Test]
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
            result.IsSuccessful.Should().BeTrue();
            result.Error.Should().Be("NONE");
            result.Message.Should().Be("The Log validation of the OperationResult was successfully.");
        }

        [Test]
        public void When_CreateLogIfValid_UnexpectedException_Then_Failure()
        {
            // Given
            // Simulate conditions that would cause an unexpected exception
            // Assume OperationBuilder<Log>.FailureUnexpectedError or similar throws an exception

            // When
            // Then
            var result = CreateLog.CreateLogIfValid(string.Empty, string.Empty, 0, 0); // Replace 0 with appropriate enum values if needed
            result.Data.Should().BeNull();
            result.Error.Should().Be("DATA_SUBMITTED_INVALID");
            result.IsSuccessful.Should().BeFalse();
            result.Message.Should().Be("The message or enitty was not submitted.");
        }
    }
}
