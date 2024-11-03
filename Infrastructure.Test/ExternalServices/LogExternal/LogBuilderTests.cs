using Domain.EnumType.LogLevel;
using Domain.EnumType.OperationExecute;
using Infrastructure.ExternalServices.LogExternal;

namespace Infrastructure.Test.ExternalServices.LogExternal
{
    [TestClass]
    public class LogBuilderTests
    {
        private LogBuilder _logBuilder;

        [TestInitialize]
        public void SetUp()
        {
            // Usamos el singleton de LogBuilder
            _logBuilder = LogBuilder.GetLogBuilder();
        }

        [TestMethod]
        public void GetLogBuilder_Should_Return_Singleton_Instance()
        {
            // Act
            var instance1 = LogBuilder.GetLogBuilder();
            var instance2 = LogBuilder.GetLogBuilder();

            // Assert
            Assert.AreSame(instance1, instance2);  // Verifica que ambas instancias son la misma
        }

        [TestMethod]
        public void Trace_Should_Create_Trace_Level_Log()
        {
            // Arrange
            var message = "Trace log message";
            var entity = new { Id = 1, Name = "TestEntity" };
            var operation = OperationExecute.Add;

            // Act
            var result = _logBuilder.Trace(message, entity, operation);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccessful);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(message, result.Data.Message);
            Assert.AreEqual(LogLevel.Trace.ToString(), result.Data.Level);
        }

        [TestMethod]
        public void Debug_Should_Create_Debug_Level_Log()
        {
            // Arrange
            var message = "Debug log message";
            var entity = new { Id = 1, Name = "TestEntity" };
            var operation = OperationExecute.Add;

            // Act
            var result = _logBuilder.Debug(message, entity, operation);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccessful);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(message, result.Data.Message);
            Assert.AreEqual(LogLevel.Debug.ToString(), result.Data.Level);
        }

        [TestMethod]
        public void Information_Should_Create_Information_Level_Log()
        {
            // Arrange
            var message = "Information log message";
            var entity = new { Id = 1, Name = "TestEntity" };
            var operation = OperationExecute.Add;

            // Act
            var result = _logBuilder.Information(message, entity, operation);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccessful);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(message, result.Data.Message);
            Assert.AreEqual(LogLevel.Information.ToString(), result.Data.Level);
        }

        [TestMethod]
        public void Warning_Should_Create_Warning_Level_Log()
        {
            // Arrange
            var message = "Warning log message";
            var entity = new { Id = 1, Name = "TestEntity" };
            var operation = OperationExecute.Add;

            // Act
            var result = _logBuilder.Warning(message, entity, operation);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccessful);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(message, result.Data.Message);
            Assert.AreEqual(LogLevel.Warning.ToString(), result.Data.Level);
        }

        [TestMethod]
        public void Error_Should_Create_Error_Level_Log()
        {
            // Arrange
            var message = "Error log message";
            var entity = new { Id = 1, Name = "TestEntity" };
            var operation = OperationExecute.Add;

            // Act
            var result = _logBuilder.Error(message, entity, operation);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccessful);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(message, result.Data.Message);
            Assert.AreEqual(LogLevel.Error.ToString(), result.Data.Level);
        }

        [TestMethod]
        public void Fatal_Should_Create_Fatal_Level_Log()
        {
            // Arrange
            var message = "Fatal log message";
            var entity = new { Id = 1, Name = "TestEntity" };
            var operation = OperationExecute.Add;

            // Act
            var result = _logBuilder.Fatal(message, entity, operation);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsSuccessful);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(message, result.Data.Message);
            Assert.AreEqual(LogLevel.Fatal.ToString(), result.Data.Level);
        }
    }
}
