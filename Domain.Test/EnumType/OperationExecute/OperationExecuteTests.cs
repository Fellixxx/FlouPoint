namespace Domain.Test.EnumType.OperationExecute
{
    using System;
    using System.Xml.Linq;
    using Domain.EnumType;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Contains unit tests for the different execution operations in the <see cref = "ActionType"/> class.
    /// </summary>
    [TestClass]
    public class OperationExecuteTests
    {
        private ActionType _testClass;
        /// <summary>
        /// Initializes the test context for each test method.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
        // Initialize any resources needed for the tests.
        }

        /// <summary>
        /// Tests that the <see cref = "ActionType.CreateCustomOperation"/> method can be called and creates a valid operation.
        /// </summary>
        [TestMethod]
        public void CanCallCreateCustomOperation()
        {
            // Arrange: Set up the test by defining a name and description for the operation.
            var name = "TestValue907046072";
            var description = "TestValue1671246287";
            // Act: Create a custom operation.
            var result = ActionType.CreateCustomOperation(name, description);
            // Assert: Verify that the operation is created correctly.
            Assert.IsNotNull(result);
            Assert.AreEqual(name, result.Name);
            Assert.AreEqual(description, result.Description);
        }

        /// <summary>
        /// Tests that the <see cref = "ActionType.CreateCustomOperation"/> method throws an exception when an invalid name is provided.
        /// </summary>
        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("   ")]
        public void CannotCallCreateCustomOperationWithInvalidName(string value)
        {
            // Assert: Verify that an ArgumentNullException is thrown for invalid names.
            Assert.ThrowsException<ArgumentNullException>(() => ActionType.CreateCustomOperation(value, "TestValue514114165"));
        }

        /// <summary>
        /// Tests that the <see cref = "ActionType.CreateCustomOperation"/> method throws an exception when an invalid description is provided.
        /// </summary>
        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("   ")]
        public void CannotCallCreateCustomOperationWithInvalidDescription(string value)
        {
            // Assert: Verify that an ArgumentNullException is thrown for invalid descriptions.
            Assert.ThrowsException<ArgumentNullException>(() => ActionType.CreateCustomOperation("TestValue1497588783", value));
        }

        /// <summary>
        /// Tests that creating a custom operation maps the name and description correctly.
        /// </summary>
        [TestMethod]
        public void CreateCustomOperationPerformsMapping()
        {
            // Arrange: Define a name and description for the operation.
            var name = "TestValue765520547";
            var description = "TestValue134813388";
            // Act: Create a custom operation.
            var result = ActionType.CreateCustomOperation(name, description);
            // Assert: Verify that the name and description are correctly mapped.
            Assert.AreEqual(name, result.Name);
            Assert.AreEqual(description, result.Description);
        }

        /// <summary>
        /// Tests that the name property of a custom operation can be retrieved.
        /// </summary>
        [TestMethod]
        public void CanGetName()
        {
            // Arrange: Create a custom operation with a specific name and description.
            var name = "TestValue2029983520";
            var description = "TestValue1891303237";
            _testClass = ActionType.CreateCustomOperation(name, description);
            // Assert: Verify that the Name property is of type string and matches the expected value.
            Assert.IsInstanceOfType(_testClass.Name, typeof(string));
            Assert.AreEqual("TestValue2029983520", _testClass.Name);
        }

        /// <summary>
        /// Tests that the description property of a custom operation can be retrieved.
        /// </summary>
        [TestMethod]
        public void CanGetDescription()
        {
            // Arrange: Create a custom operation with a specific name and description.
            var name = "TestValue765520947";
            var description = "TestValue1891303237";
            _testClass = ActionType.CreateCustomOperation(name, description);
            // Assert: Verify that the Description property is of type string and matches the expected value.
            Assert.IsInstanceOfType(_testClass.Description, typeof(string));
            Assert.AreEqual("TestValue1891303237", _testClass.Description);
        }

        /// <summary>
        /// Tests that the predefined Add operation is retrievable and has correct properties.
        /// </summary>
        [TestMethod]
        public void CanGetAdd()
        {
            // Assert: Verify that the Add operation is an instance of ActionType and check its name and description.
            Assert.IsInstanceOfType(ActionType.Add, typeof(ActionType));
            Assert.AreEqual("Add", ActionType.Add.Name);
            Assert.AreEqual("Add a new record.", ActionType.Add.Description);
        }

        /// <summary>
        /// Tests that the predefined Modified operation is retrievable and has correct properties.
        /// </summary>
        [TestMethod]
        public void CanGetModified()
        {
            // Assert: Verify that the Modified operation is an instance of ActionType and check its name and description.
            Assert.IsInstanceOfType(ActionType.Modified, typeof(ActionType));
            Assert.AreEqual("Modified", ActionType.Modified.Name);
            Assert.AreEqual("Modify an existing record.", ActionType.Modified.Description);
        }

        /// <summary>
        /// Tests that the predefined Remove operation is retrievable and has correct properties.
        /// </summary>
        [TestMethod]
        public void CanGetRemove()
        {
            // Assert: Verify that the Remove operation is an instance of ActionType and check its name and description.
            Assert.IsInstanceOfType(ActionType.Remove, typeof(ActionType));
            Assert.AreEqual("Remove", ActionType.Remove.Name);
            Assert.AreEqual("Remove an existing record.", ActionType.Remove.Description);
        }

        /// <summary>
        /// Tests that the predefined Deactivate operation is retrievable and has correct properties.
        /// </summary>
        [TestMethod]
        public void CanGetDeactivate()
        {
            // Assert: Verify that the Deactivate operation is an instance of ActionType and check its name and description.
            Assert.IsInstanceOfType(ActionType.Deactivate, typeof(ActionType));
            Assert.AreEqual("Deactivate", ActionType.Deactivate.Name);
            Assert.AreEqual("Deactivate an existing record.", ActionType.Deactivate.Description);
        }

        /// <summary>
        /// Tests that the predefined Activate operation is retrievable and has correct properties.
        /// </summary>
        [TestMethod]
        public void CanGetActivate()
        {
            // Assert: Verify that the Activate operation is an instance of ActionType and check its name and description.
            Assert.IsInstanceOfType(ActionType.Activate, typeof(ActionType));
            Assert.AreEqual("Activate", ActionType.Activate.Name);
            Assert.AreEqual("Activate a deactivated record.", ActionType.Activate.Description);
        }

        /// <summary>
        /// Tests that the predefined GetUserById operation is retrievable and has correct properties.
        /// </summary>
        [TestMethod]
        public void CanGetGetUserById()
        {
            // Assert: Verify that the GetUserById operation is an instance of ActionType and check its name and description.
            Assert.IsInstanceOfType(ActionType.GetUserById, typeof(ActionType));
            Assert.AreEqual("GetUserById", ActionType.GetUserById.Name);
            Assert.AreEqual("Retrieve a user by their ID.", ActionType.GetUserById.Description);
        }

        /// <summary>
        /// Tests that the predefined GetAllByFilter operation is retrievable and has correct properties.
        /// </summary>
        [TestMethod]
        public void CanGetGetAllByFilter()
        {
            // Assert: Verify that the GetAllByFilter operation is an instance of ActionType and check its name and description.
            Assert.IsInstanceOfType(ActionType.GetAllByFilter, typeof(ActionType));
            Assert.AreEqual("GetAllByFilter", ActionType.GetAllByFilter.Name);
            Assert.AreEqual("Retrieve all records that match a given filter.", ActionType.GetAllByFilter.Description);
        }

        /// <summary>
        /// Tests that the predefined GetPageByFilter operation is retrievable and has correct properties.
        /// </summary>
        [TestMethod]
        public void CanGetGetPageByFilter()
        {
            // Assert: Verify that the GetPageByFilter operation is an instance of ActionType and check its name and description.
            Assert.IsInstanceOfType(ActionType.GetPageByFilter, typeof(ActionType));
            Assert.AreEqual("GetPageByFilter", ActionType.GetPageByFilter.Name);
            Assert.AreEqual("Retrieve a page of records that match a given filter.", ActionType.GetPageByFilter.Description);
        }

        /// <summary>
        /// Tests that the predefined GetCountFilter operation is retrievable and has correct properties.
        /// </summary>
        [TestMethod]
        public void CanGetGetCountFilter()
        {
            // Assert: Verify that the GetCountFilter operation is an instance of ActionType and check its name and description.
            Assert.IsInstanceOfType(ActionType.GetCountFilter, typeof(ActionType));
            Assert.AreEqual("GetCountFilter", ActionType.GetCountFilter.Name);
            Assert.AreEqual("Get the count of records that match a given filter.", ActionType.GetCountFilter.Description);
        }

        /// <summary>
        /// Tests that attempting to create an operation with an existing name results in an exception.
        /// </summary>
        [TestMethod]
        public void CannotCreateOperationWithExistingName()
        {
            // Arrange: Define an existing name for the operation.
            var name = "Add";
            var description = "Some other description";
            // Act & Assert: Verify that an InvalidOperationException is thrown.
            Assert.ThrowsException<InvalidOperationException>(() => ActionType.CreateCustomOperation(name, description));
        }

        /// <summary>
        /// Tests that predefined operations are singletons.
        /// </summary>
        [TestMethod]
        public void OperationsAreSingletons()
        {
            // Assert: Verify that each predefined operation returns the same instance.
            Assert.AreSame(ActionType.Add, ActionType.Add);
            Assert.AreSame(ActionType.Modified, ActionType.Modified);
            Assert.AreSame(ActionType.Remove, ActionType.Remove);
            Assert.AreSame(ActionType.Deactivate, ActionType.Deactivate);
            Assert.AreSame(ActionType.Activate, ActionType.Activate);
        }

        /// <summary>
        /// Tests that an operation equals itself.
        /// </summary>
        [TestMethod]
        public void OperationEqualsItself()
        {
            // Arrange: Create a custom operation.
            var name = "TestValue2123520947";
            var description = "TestValue1891303237";
            _testClass = ActionType.CreateCustomOperation(name, description);
            // Assert: Verify that the operation is equal to itself and that hash codes match.
            Assert.IsTrue(_testClass.Equals(_testClass));
            Assert.AreEqual(_testClass.GetHashCode(), _testClass.GetHashCode());
        }

        /// <summary>
        /// Tests that an operation does not equal null.
        /// </summary>
        [TestMethod]
        public void OperationDoesNotEqualNull()
        {
            // Arrange: Create a custom operation.
            var name = "TestValue76523431947";
            var description = "TestValue1891303237";
            _testClass = ActionType.CreateCustomOperation(name, description);
            // Assert: Verify that the operation is not equal to null.
            Assert.IsFalse(_testClass.Equals(null));
        }

        /// <summary>
        /// Tests that the predefined Add operation's name and description are correct.
        /// </summary>
        [TestMethod]
        public void When_AddOperation_Then_NameAndDescriptionShouldMatch()
        {
            // Given: Retrieve the Add operation.
            var operation = ActionType.Add;
            // Then: Verify that the name and description match expected values.
            Assert.IsNotNull(operation);
            Assert.AreEqual("Add", operation.Name);
            Assert.AreEqual("Add a new record.", operation.Description);
        }

        /// <summary>
        /// Tests that the predefined Modified operation's name and description are correct.
        /// </summary>
        [TestMethod]
        public void When_ModifiedOperation_Then_NameAndDescriptionShouldMatch()
        {
            // Given: Retrieve the Modified operation.
            var operation = ActionType.Modified;
            // Then: Verify that the name and description match expected values.
            Assert.IsNotNull(operation);
            Assert.AreEqual("Modified", operation.Name);
            Assert.AreEqual("Modify an existing record.", operation.Description);
        }

        /// <summary>
        /// Tests that the predefined Remove operation's name and description are correct.
        /// </summary>
        [TestMethod]
        public void When_RemoveOperation_Then_NameAndDescriptionShouldMatch()
        {
            // Given: Retrieve the Remove operation.
            var operation = ActionType.Remove;
            // Then: Verify that the name and description match expected values.
            Assert.IsNotNull(operation);
            Assert.AreEqual("Remove", operation.Name);
            Assert.AreEqual("Remove an existing record.", operation.Description);
        }

        /// <summary>
        /// Tests that the predefined Deactivate operation's name and description are correct.
        /// </summary>
        [TestMethod]
        public void When_DeactivateOperation_Then_NameAndDescriptionShouldMatch()
        {
            // Given: Retrieve the Deactivate operation.
            var operation = ActionType.Deactivate;
            // Then: Verify that the name and description match expected values.
            Assert.IsNotNull(operation);
            Assert.AreEqual("Deactivate", operation.Name);
            Assert.AreEqual("Deactivate an existing record.", operation.Description);
        }

        /// <summary>
        /// Tests that the predefined Activate operation's name and description are correct.
        /// </summary>
        [TestMethod]
        public void When_ActivateOperation_Then_NameAndDescriptionShouldMatch()
        {
            // Given: Retrieve the Activate operation.
            var operation = ActionType.Activate;
            // Then: Verify that the name and description match expected values.
            Assert.IsNotNull(operation);
            Assert.AreEqual("Activate", operation.Name);
            Assert.AreEqual("Activate a deactivated record.", operation.Description);
        }

        /// <summary>
        /// Tests that the predefined GetUserById operation's name and description are correct.
        /// </summary>
        [TestMethod]
        public void When_GetUserByIdOperation_Then_NameAndDescriptionShouldMatch()
        {
            // Given: Retrieve the GetUserById operation.
            var operation = ActionType.GetUserById;
            // Then: Verify that the name and description match expected values.
            Assert.IsNotNull(operation);
            Assert.AreEqual("GetUserById", operation.Name);
            Assert.AreEqual("Retrieve a user by their ID.", operation.Description);
        }

        /// <summary>
        /// Tests that the predefined GetAllByFilter operation's name and description are correct.
        /// </summary>
        [TestMethod]
        public void When_GetAllByFilterOperation_Then_NameAndDescriptionShouldMatch()
        {
            // Given: Retrieve the GetAllByFilter operation.
            var operation = ActionType.GetAllByFilter;
            // Then: Verify that the name and description match expected values.
            Assert.IsNotNull(operation);
            Assert.AreEqual("GetAllByFilter", operation.Name);
            Assert.AreEqual("Retrieve all records that match a given filter.", operation.Description);
        }

        /// <summary>
        /// Tests that the predefined GetPageByFilter operation's name and description are correct.
        /// </summary>
        [TestMethod]
        public void When_GetPageByFilterOperation_Then_NameAndDescriptionShouldMatch()
        {
            // Given: Retrieve the GetPageByFilter operation.
            var operation = ActionType.GetPageByFilter;
            // Then: Verify that the name and description match expected values.
            Assert.IsNotNull(operation);
            Assert.AreEqual("GetPageByFilter", operation.Name);
            Assert.AreEqual("Retrieve a page of records that match a given filter.", operation.Description);
        }

        /// <summary>
        /// Tests that the predefined GetCountFilter operation's name and description are correct.
        /// </summary>
        [TestMethod]
        public void When_GetCountFilterOperation_Then_NameAndDescriptionShouldMatch()
        {
            // Given: Retrieve the GetCountFilter operation.
            var operation = ActionType.GetCountFilter;
            // Then: Verify that the name and description match expected values.
            Assert.IsNotNull(operation);
            Assert.AreEqual("GetCountFilter", operation.Name);
            Assert.AreEqual("Get the count of records that match a given filter.", operation.Description);
        }

        /// <summary>
        /// Tests that creating a custom operation sets its name and description correctly.
        /// </summary>
        [TestMethod]
        public void When_CreateCustomOperation_Then_NameAndDescriptionShouldMatch()
        {
            // Given: Define a name and description for a custom operation and create it.
            var operation = ActionType.CreateCustomOperation("CustomOp", "This is a custom operation");
            // Then: Verify that the custom operation's properties match the given values.
            Assert.IsNotNull(operation);
            Assert.AreEqual("CustomOp", operation.Name);
            Assert.AreEqual("This is a custom operation", operation.Description);
        }

        /// <summary>
        /// Tests that retrieving the name of a predefined operation returns the expected name.
        /// </summary>
        [TestMethod]
        public void When_GetNameForPredefinedOperation_Then_NameShouldMatch()
        {
            // Given: Retrieve a predefined operation.
            var operation = ActionType.Add;
            // When: Get the name of the operation.
            var name = ActionType.GetName(operation);
            // Then: Verify that the name matches the expected value.
            Assert.AreEqual("Add", name);
        }

        /// <summary>
        /// Tests that passing a null operation to GetName throws an ArgumentNullException.
        /// </summary>
        [TestMethod]
        public void When_NullOperationPassedToGetName_Then_ArgumentNullExceptionShouldBeThrown()
        {
            // Given: Define a null operation.
            ActionType operation = null;
            // When & Then: Verify that an ArgumentNullException is thrown when getting the name.
            Assert.ThrowsException<ArgumentNullException>(() => ActionType.GetName(operation));
        }
    }
}