namespace LayerInfrastructure.ExternalServices
{
    using FluentAssertions;
    using global::Application.UseCases.ExternalServices;
    using global::Domain.DTO.Log;
    using global::Domain.EnumType.OperationExecute;
    using global::Domain.EnumType.LogLevel;
    using global::Infrastructure.ExternalServices.LogExternal;
    using NUnit.Framework;

    internal class LogBuilderTests
    {
        private ILogBuilder<Log>? logBuilder;

        [SetUp]
        public void Setup()
        {
            logBuilder = LogBuilder.GetLogBuilder();
        }

        [Test]
        public void When_Trace_ValidEntity_Then_Success()
        {
            // Given
            string message = "This is a Trace log message";
            object entity = new { Name = "Entity1" };
            OperationExecute operation = OperationExecute.Activate;

            // When
            var result = logBuilder?.Trace(message, entity, operation);

            // Then
            result.Should().NotBeNull();
            result.IsSuccessful.Should().BeTrue();
        }

        [Test]
        public void When_Debug_ValidEntity_Then_Success()
        {
            // Given
            string message = "This is a Debug log message";
            object entity = new { Name = "Entity2" };
            OperationExecute operation = OperationExecute.Activate;

            // When
            var result = logBuilder?.Debug(message, entity, operation);

            // Then
            result.Should().NotBeNull();
            result.IsSuccessful.Should().BeTrue();
        }

        // You can similarly write tests for the other methods: Information, Warning, Error, and Fatal

        [Test]
        public void When_Information_InvalidEntity_Then_Failed()
        {
            // Given
            string message = "This is an Information log message";
            object? entity = null;
            OperationExecute operation = OperationExecute.Activate;

            // When
            var result = logBuilder?.Information(message, entity, operation);

            // Then
            result.Should().NotBeNull();
            result.IsSuccessful.Should().BeFalse();
        }
    }
}
