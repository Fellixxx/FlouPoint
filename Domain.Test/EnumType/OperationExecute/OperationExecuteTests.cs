
namespace Domain.Test.EnumType.OperationExecute
{
    using System;
    using System.Xml.Linq;
    using Domain.EnumType;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    [TestClass]
    public class OperationExecuteTests
    {
        private ActionType _testClass;

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
            var result = ActionType.CreateCustomOperation(name, description);

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
            Assert.ThrowsException<ArgumentNullException>(() => ActionType.CreateCustomOperation(value, "TestValue514114165"));
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("   ")]
        public void CannotCallCreateCustomOperationWithInvalidDescription(string value)
        {
            Assert.ThrowsException<ArgumentNullException>(() => ActionType.CreateCustomOperation("TestValue1497588783", value));
        }

        [TestMethod]
        public void CreateCustomOperationPerformsMapping()
        {
            // Arrange
            var name = "TestValue765520547";
            var description = "TestValue134813388";

            // Act
            var result = ActionType.CreateCustomOperation(name, description);

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
            _testClass = ActionType.CreateCustomOperation(name, description);

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
            _testClass = ActionType.CreateCustomOperation(name, description);
            // Assert
            Assert.IsInstanceOfType(_testClass.Description, typeof(string));
            Assert.AreEqual("TestValue1891303237", _testClass.Description);
        }

        [TestMethod]
        public void CanGetAdd()
        {
            // Assert
            Assert.IsInstanceOfType(ActionType.Add, typeof(ActionType));
            Assert.AreEqual("Add", ActionType.Add.Name);
            Assert.AreEqual("Add a new record.", ActionType.Add.Description);
        }

        [TestMethod]
        public void CanGetModified()
        {
            // Assert
            Assert.IsInstanceOfType(ActionType.Modified, typeof(ActionType));
            Assert.AreEqual("Modified", ActionType.Modified.Name);
            Assert.AreEqual("Modify an existing record.", ActionType.Modified.Description);
        }

        [TestMethod]
        public void CanGetRemove()
        {
            // Assert
            Assert.IsInstanceOfType(ActionType.Remove, typeof(ActionType));
            Assert.AreEqual("Remove", ActionType.Remove.Name);
            Assert.AreEqual("Remove an existing record.", ActionType.Remove.Description);
        }

        [TestMethod]
        public void CanGetDeactivate()
        {
            // Assert
            Assert.IsInstanceOfType(ActionType.Deactivate, typeof(ActionType));
            Assert.AreEqual("Deactivate", ActionType.Deactivate.Name);
            Assert.AreEqual("Deactivate an existing record.", ActionType.Deactivate.Description);
        }

        [TestMethod]
        public void CanGetActivate()
        {
            // Assert
            Assert.IsInstanceOfType(ActionType.Activate, typeof(ActionType));
            Assert.AreEqual("Activate", ActionType.Activate.Name);
            Assert.AreEqual("Activate a deactivated record.", ActionType.Activate.Description);
        }

        [TestMethod]
        public void CanGetGetUserById()
        {
            // Assert
            Assert.IsInstanceOfType(ActionType.GetUserById, typeof(ActionType));
            Assert.AreEqual("GetUserById", ActionType.GetUserById.Name);
            Assert.AreEqual("Retrieve a user by their ID.", ActionType.GetUserById.Description);
        }

        [TestMethod]
        public void CanGetGetAllByFilter()
        {
            // Assert
            Assert.IsInstanceOfType(ActionType.GetAllByFilter, typeof(ActionType));
            Assert.AreEqual("GetAllByFilter", ActionType.GetAllByFilter.Name);
            Assert.AreEqual("Retrieve all records that match a given filter.", ActionType.GetAllByFilter.Description);
        }

        [TestMethod]
        public void CanGetGetPageByFilter()
        {
            // Assert
            Assert.IsInstanceOfType(ActionType.GetPageByFilter, typeof(ActionType));
            Assert.AreEqual("GetPageByFilter", ActionType.GetPageByFilter.Name);
            Assert.AreEqual("Retrieve a page of records that match a given filter.", ActionType.GetPageByFilter.Description);
        }

        [TestMethod]
        public void CanGetGetCountFilter()
        {
            // Assert
            Assert.IsInstanceOfType(ActionType.GetCountFilter, typeof(ActionType));
            Assert.AreEqual("GetCountFilter", ActionType.GetCountFilter.Name);
            Assert.AreEqual("Get the count of records that match a given filter.", ActionType.GetCountFilter.Description);
        }

        [TestMethod]
        public void CannotCreateOperationWithExistingName()
        {
            // Arrange
            var name = "Add";
            var description = "Some other description";

            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => ActionType.CreateCustomOperation(name, description));
        }

        [TestMethod]
        public void OperationsAreSingletons()
        {
            // Assert
            Assert.AreSame(ActionType.Add, ActionType.Add);
            Assert.AreSame(ActionType.Modified, ActionType.Modified);
            Assert.AreSame(ActionType.Remove, ActionType.Remove);
            Assert.AreSame(ActionType.Deactivate, ActionType.Deactivate);
            Assert.AreSame(ActionType.Activate, ActionType.Activate);
        }

        [TestMethod]
        public void OperationEqualsItself()
        {
            // Arrange
            var name = "TestValue2123520947";
            var description = "TestValue1891303237";
            _testClass = ActionType.CreateCustomOperation(name, description);
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
            _testClass = ActionType.CreateCustomOperation(name, description);
            // Assert
            Assert.IsFalse(_testClass.Equals(null));
        }
        [TestMethod]
        public void When_AddOperation_Then_NameAndDescriptionShouldMatch()
        {
            // Given
            var operation = ActionType.Add;

            // Then
            Assert.IsNotNull(operation);
            Assert.AreEqual("Add", operation.Name);
            Assert.AreEqual("Add a new record.", operation.Description);
        }

        [TestMethod]
        public void When_ModifiedOperation_Then_NameAndDescriptionShouldMatch()
        {
            // Given
            var operation = ActionType.Modified;

            // Then
            Assert.IsNotNull(operation);
            Assert.AreEqual("Modified", operation.Name);
            Assert.AreEqual("Modify an existing record.", operation.Description);
        }

        [TestMethod]
        public void When_RemoveOperation_Then_NameAndDescriptionShouldMatch()
        {
            // Given
            var operation = ActionType.Remove;

            // Then
            Assert.IsNotNull(operation);
            Assert.AreEqual("Remove", operation.Name);
            Assert.AreEqual("Remove an existing record.", operation.Description);
        }

        [TestMethod]
        public void When_DeactivateOperation_Then_NameAndDescriptionShouldMatch()
        {
            // Given
            var operation = ActionType.Deactivate;

            // Then
            Assert.IsNotNull(operation);
            Assert.AreEqual("Deactivate", operation.Name);
            Assert.AreEqual("Deactivate an existing record.", operation.Description);
        }

        [TestMethod]
        public void When_ActivateOperation_Then_NameAndDescriptionShouldMatch()
        {
            // Given
            var operation = ActionType.Activate;

            // Then
            Assert.IsNotNull(operation);
            Assert.AreEqual("Activate", operation.Name);
            Assert.AreEqual("Activate a deactivated record.", operation.Description);
        }

        [TestMethod]
        public void When_GetUserByIdOperation_Then_NameAndDescriptionShouldMatch()
        {
            // Given
            var operation = ActionType.GetUserById;

            // Then
            Assert.IsNotNull(operation);
            Assert.AreEqual("GetUserById", operation.Name);
            Assert.AreEqual("Retrieve a user by their ID.", operation.Description);
        }

        [TestMethod]
        public void When_GetAllByFilterOperation_Then_NameAndDescriptionShouldMatch()
        {
            // Given
            var operation = ActionType.GetAllByFilter;

            // Then
            Assert.IsNotNull(operation);
            Assert.AreEqual("GetAllByFilter", operation.Name);
            Assert.AreEqual("Retrieve all records that match a given filter.", operation.Description);
        }

        [TestMethod]
        public void When_GetPageByFilterOperation_Then_NameAndDescriptionShouldMatch()
        {
            // Given
            var operation = ActionType.GetPageByFilter;

            // Then
            Assert.IsNotNull(operation);
            Assert.AreEqual("GetPageByFilter", operation.Name);
            Assert.AreEqual("Retrieve a page of records that match a given filter.", operation.Description);
        }

        [TestMethod]
        public void When_GetCountFilterOperation_Then_NameAndDescriptionShouldMatch()
        {
            // Given
            var operation = ActionType.GetCountFilter;

            // Then
            Assert.IsNotNull(operation);
            Assert.AreEqual("GetCountFilter", operation.Name);
            Assert.AreEqual("Get the count of records that match a given filter.", operation.Description);
        }

        [TestMethod]
        public void When_CreateCustomOperation_Then_NameAndDescriptionShouldMatch()
        {
            // Given
            var operation = ActionType.CreateCustomOperation("CustomOp", "This is a custom operation");

            // Then
            Assert.IsNotNull(operation);
            Assert.AreEqual("CustomOp", operation.Name);
            Assert.AreEqual("This is a custom operation", operation.Description);
        }

        [TestMethod]
        public void When_GetNameForPredefinedOperation_Then_NameShouldMatch()
        {
            // Given
            var operation = ActionType.Add;

            // When
            var name = ActionType.GetName(operation);

            // Then
            Assert.AreEqual("Add", name);
        }

        [TestMethod]
        public void When_NullOperationPassedToGetName_Then_ArgumentNullExceptionShouldBeThrown()
        {
            // Given
            ActionType operation = null;

            // When & Then
            Assert.ThrowsException<ArgumentNullException>(() => ActionType.GetName(operation));
        }
    }
}