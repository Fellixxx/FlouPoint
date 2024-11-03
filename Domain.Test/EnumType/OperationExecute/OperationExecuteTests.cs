
namespace Domain.Test.EnumType.OperationExecute
{
    using System;
    using System.Xml.Linq;
    using Domain.EnumType;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    [TestClass]
    public class OperationExecuteTests
    {
        private OperationExecute _testClass;

        [TestInitialize]
        public void SetUp()
        {
           
        }

        [TestMethod]
        public void CanCallCreateCustomOperation()
        {
            // Arrange
            var name = "TestValue907046072";
            var description = "TestValue1671246287";

            // Act
            var result = OperationExecute.CreateCustomOperation(name, description);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(name, result.Name);
            Assert.AreEqual(description, result.Description);
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("   ")]
        public void CannotCallCreateCustomOperationWithInvalidName(string value)
        {
            Assert.ThrowsException<ArgumentNullException>(() => OperationExecute.CreateCustomOperation(value, "TestValue514114165"));
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("   ")]
        public void CannotCallCreateCustomOperationWithInvalidDescription(string value)
        {
            Assert.ThrowsException<ArgumentNullException>(() => OperationExecute.CreateCustomOperation("TestValue1497588783", value));
        }

        [TestMethod]
        public void CreateCustomOperationPerformsMapping()
        {
            // Arrange
            var name = "TestValue765520547";
            var description = "TestValue134813388";

            // Act
            var result = OperationExecute.CreateCustomOperation(name, description);

            // Assert
            Assert.AreEqual(name, result.Name);
            Assert.AreEqual(description, result.Description);
        }

        [TestMethod]
        public void CanGetName()
        {
            // Arrange
            var name = "TestValue2029983520";
            var description = "TestValue1891303237";
            _testClass = OperationExecute.CreateCustomOperation(name, description);

            // Assert
            Assert.IsInstanceOfType(_testClass.Name, typeof(string));
            Assert.AreEqual("TestValue2029983520", _testClass.Name);
        }

        [TestMethod]
        public void CanGetDescription()
        {
            // Arrange
            var name = "TestValue765520947";
            var description = "TestValue1891303237";
            _testClass = OperationExecute.CreateCustomOperation(name, description);
            // Assert
            Assert.IsInstanceOfType(_testClass.Description, typeof(string));
            Assert.AreEqual("TestValue1891303237", _testClass.Description);
        }

        [TestMethod]
        public void CanGetAdd()
        {
            // Assert
            Assert.IsInstanceOfType(OperationExecute.Add, typeof(OperationExecute));
            Assert.AreEqual("Add", OperationExecute.Add.Name);
            Assert.AreEqual("Add a new record.", OperationExecute.Add.Description);
        }

        [TestMethod]
        public void CanGetModified()
        {
            // Assert
            Assert.IsInstanceOfType(OperationExecute.Modified, typeof(OperationExecute));
            Assert.AreEqual("Modified", OperationExecute.Modified.Name);
            Assert.AreEqual("Modify an existing record.", OperationExecute.Modified.Description);
        }

        [TestMethod]
        public void CanGetRemove()
        {
            // Assert
            Assert.IsInstanceOfType(OperationExecute.Remove, typeof(OperationExecute));
            Assert.AreEqual("Remove", OperationExecute.Remove.Name);
            Assert.AreEqual("Remove an existing record.", OperationExecute.Remove.Description);
        }

        [TestMethod]
        public void CanGetDeactivate()
        {
            // Assert
            Assert.IsInstanceOfType(OperationExecute.Deactivate, typeof(OperationExecute));
            Assert.AreEqual("Deactivate", OperationExecute.Deactivate.Name);
            Assert.AreEqual("Deactivate an existing record.", OperationExecute.Deactivate.Description);
        }

        [TestMethod]
        public void CanGetActivate()
        {
            // Assert
            Assert.IsInstanceOfType(OperationExecute.Activate, typeof(OperationExecute));
            Assert.AreEqual("Activate", OperationExecute.Activate.Name);
            Assert.AreEqual("Activate a deactivated record.", OperationExecute.Activate.Description);
        }

        [TestMethod]
        public void CanGetGetUserById()
        {
            // Assert
            Assert.IsInstanceOfType(OperationExecute.GetUserById, typeof(OperationExecute));
            Assert.AreEqual("GetUserById", OperationExecute.GetUserById.Name);
            Assert.AreEqual("Retrieve a user by their ID.", OperationExecute.GetUserById.Description);
        }

        [TestMethod]
        public void CanGetGetAllByFilter()
        {
            // Assert
            Assert.IsInstanceOfType(OperationExecute.GetAllByFilter, typeof(OperationExecute));
            Assert.AreEqual("GetAllByFilter", OperationExecute.GetAllByFilter.Name);
            Assert.AreEqual("Retrieve all records that match a given filter.", OperationExecute.GetAllByFilter.Description);
        }

        [TestMethod]
        public void CanGetGetPageByFilter()
        {
            // Assert
            Assert.IsInstanceOfType(OperationExecute.GetPageByFilter, typeof(OperationExecute));
            Assert.AreEqual("GetPageByFilter", OperationExecute.GetPageByFilter.Name);
            Assert.AreEqual("Retrieve a page of records that match a given filter.", OperationExecute.GetPageByFilter.Description);
        }

        [TestMethod]
        public void CanGetGetCountFilter()
        {
            // Assert
            Assert.IsInstanceOfType(OperationExecute.GetCountFilter, typeof(OperationExecute));
            Assert.AreEqual("GetCountFilter", OperationExecute.GetCountFilter.Name);
            Assert.AreEqual("Get the count of records that match a given filter.", OperationExecute.GetCountFilter.Description);
        }

        [TestMethod]
        public void CannotCreateOperationWithExistingName()
        {
            // Arrange
            var name = "Add";
            var description = "Some other description";

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => OperationExecute.CreateCustomOperation(name, description));
        }

        [TestMethod]
        public void OperationsAreSingletons()
        {
            // Assert
            Assert.AreSame(OperationExecute.Add, OperationExecute.Add);
            Assert.AreSame(OperationExecute.Modified, OperationExecute.Modified);
            Assert.AreSame(OperationExecute.Remove, OperationExecute.Remove);
            Assert.AreSame(OperationExecute.Deactivate, OperationExecute.Deactivate);
            Assert.AreSame(OperationExecute.Activate, OperationExecute.Activate);
        }

        [TestMethod]
        public void OperationEqualsItself()
        {
            // Arrange
            var name = "TestValue2123520947";
            var description = "TestValue1891303237";
            _testClass = OperationExecute.CreateCustomOperation(name, description);
            // Assert
            Assert.IsTrue(_testClass.Equals(_testClass));
            Assert.AreEqual(_testClass.GetHashCode(), _testClass.GetHashCode());
        }

        [TestMethod]
        public void OperationDoesNotEqualNull()
        {
            // Arrange
            var name = "TestValue76523431947";
            var description = "TestValue1891303237";
            _testClass = OperationExecute.CreateCustomOperation(name, description);
            // Assert
            Assert.IsFalse(_testClass.Equals(null));
        }
        [TestMethod]
        public void When_AddOperation_Then_NameAndDescriptionShouldMatch()
        {
            // Given
            var operation = OperationExecute.Add;

            // Then
            Assert.IsNotNull(operation);
            Assert.AreEqual("Add", operation.Name);
            Assert.AreEqual("Add a new record.", operation.Description);
        }

        [TestMethod]
        public void When_ModifiedOperation_Then_NameAndDescriptionShouldMatch()
        {
            // Given
            var operation = OperationExecute.Modified;

            // Then
            Assert.IsNotNull(operation);
            Assert.AreEqual("Modified", operation.Name);
            Assert.AreEqual("Modify an existing record.", operation.Description);
        }

        [TestMethod]
        public void When_RemoveOperation_Then_NameAndDescriptionShouldMatch()
        {
            // Given
            var operation = OperationExecute.Remove;

            // Then
            Assert.IsNotNull(operation);
            Assert.AreEqual("Remove", operation.Name);
            Assert.AreEqual("Remove an existing record.", operation.Description);
        }

        [TestMethod]
        public void When_DeactivateOperation_Then_NameAndDescriptionShouldMatch()
        {
            // Given
            var operation = OperationExecute.Deactivate;

            // Then
            Assert.IsNotNull(operation);
            Assert.AreEqual("Deactivate", operation.Name);
            Assert.AreEqual("Deactivate an existing record.", operation.Description);
        }

        [TestMethod]
        public void When_ActivateOperation_Then_NameAndDescriptionShouldMatch()
        {
            // Given
            var operation = OperationExecute.Activate;

            // Then
            Assert.IsNotNull(operation);
            Assert.AreEqual("Activate", operation.Name);
            Assert.AreEqual("Activate a deactivated record.", operation.Description);
        }

        [TestMethod]
        public void When_GetUserByIdOperation_Then_NameAndDescriptionShouldMatch()
        {
            // Given
            var operation = OperationExecute.GetUserById;

            // Then
            Assert.IsNotNull(operation);
            Assert.AreEqual("GetUserById", operation.Name);
            Assert.AreEqual("Retrieve a user by their ID.", operation.Description);
        }

        [TestMethod]
        public void When_GetAllByFilterOperation_Then_NameAndDescriptionShouldMatch()
        {
            // Given
            var operation = OperationExecute.GetAllByFilter;

            // Then
            Assert.IsNotNull(operation);
            Assert.AreEqual("GetAllByFilter", operation.Name);
            Assert.AreEqual("Retrieve all records that match a given filter.", operation.Description);
        }

        [TestMethod]
        public void When_GetPageByFilterOperation_Then_NameAndDescriptionShouldMatch()
        {
            // Given
            var operation = OperationExecute.GetPageByFilter;

            // Then
            Assert.IsNotNull(operation);
            Assert.AreEqual("GetPageByFilter", operation.Name);
            Assert.AreEqual("Retrieve a page of records that match a given filter.", operation.Description);
        }

        [TestMethod]
        public void When_GetCountFilterOperation_Then_NameAndDescriptionShouldMatch()
        {
            // Given
            var operation = OperationExecute.GetCountFilter;

            // Then
            Assert.IsNotNull(operation);
            Assert.AreEqual("GetCountFilter", operation.Name);
            Assert.AreEqual("Get the count of records that match a given filter.", operation.Description);
        }

        [TestMethod]
        public void When_CreateCustomOperation_Then_NameAndDescriptionShouldMatch()
        {
            // Given
            var operation = OperationExecute.CreateCustomOperation("CustomOp", "This is a custom operation");

            // Then
            Assert.IsNotNull(operation);
            Assert.AreEqual("CustomOp", operation.Name);
            Assert.AreEqual("This is a custom operation", operation.Description);
        }

        [TestMethod]
        public void When_GetNameForPredefinedOperation_Then_NameShouldMatch()
        {
            // Given
            var operation = OperationExecute.Add;

            // When
            var name = OperationExecute.GetName(operation);

            // Then
            Assert.AreEqual("Add", name);
        }

        [TestMethod]
        public void When_NullOperationPassedToGetName_Then_ArgumentNullExceptionShouldBeThrown()
        {
            // Given
            OperationExecute operation = null;

            // When & Then
            Assert.ThrowsException<ArgumentNullException>(() => OperationExecute.GetName(operation));
        }
    }
}