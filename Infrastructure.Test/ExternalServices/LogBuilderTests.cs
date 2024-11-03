using Application.UseCases.ExternalServices;
using Domain.DTO.Logging;
using Domain.EnumType;
using Infrastructure.ExternalServices.LogExternal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Test.ExternalServices
{
    [TestClass]
    internal class LogBuilderTests
    {
        private ILogBuilder<Log>? logBuilder;

        [TestInitialize]
        public void Setup()
        {
            logBuilder = LogBuilder.GetLogBuilder();
        }

        [TestMethod]
        public void When_Trace_ValidEntity_Then_Success()
        {
            // Given
            string message = "This is a Trace log message";
            object entity = new { Name = "Entity1" };
            OperationExecute operation = OperationExecute.Activate;

            // When
            var result = logBuilder?.Trace(message, entity, operation);

            // Then
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccessful);
        }

        [TestMethod]
        public void When_Debug_ValidEntity_Then_Success()
        {
            // Given
            string message = "This is a Debug log message";
            object entity = new { Name = "Entity2" };
            OperationExecute operation = OperationExecute.Activate;

            // When
            var result = logBuilder?.Debug(message, entity, operation);

            // Then
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccessful);
        }

        // You can similarly write tests for the other methods: Information, Warning, Error, and Fatal

        [TestMethod]
        public void When_Information_InvalidEntity_Then_Failed()
        {
            // Given
            string message = "This is an Information log message";
            object? entity = null;
            OperationExecute operation = OperationExecute.Activate;

            // When
            var result = logBuilder?.Information(message, entity, operation);

            // Then
            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsSuccessful);
        }
    }
}
