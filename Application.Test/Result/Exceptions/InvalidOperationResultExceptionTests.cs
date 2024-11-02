namespace Application.Test.Result.Exceptions
{
    using System;
    using Application.Result.Exceptions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class InvalidOperationResultExceptionTests
    {
        private InvalidOperationResultException _testClass;
        private string _message;

        [TestInitialize]
        public void SetUp()
        {
            _message="TestValue1658752453";
            _testClass=new InvalidOperationResultException(_message);
        }

        [TestMethod]
        public void CanConstruct()
        {
            // Act
            var instance = new InvalidOperationResultException(_message);

            // Assert
            Assert.IsNotNull(instance);
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("   ")]
        public void CannotConstructWithInvalidMessage(string value)
        {
            Assert.ThrowsException<ArgumentNullException>(() => new InvalidOperationResultException(value));
        }
    }
}