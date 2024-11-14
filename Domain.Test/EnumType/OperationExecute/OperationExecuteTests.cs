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
        // Initialize any resources needed for the tests.
        }

        [TestMethod]
        public void Should_CreateCustomOperation_WithValidInputs()
        {
            var name = "TestValue907046072";
            var description = "TestValue1671246287";
            var result = ActionType.CreateCustomOperation(name, description);
            Assert.IsNotNull(result);
            Assert.AreEqual(name, result.Name);
            Assert.AreEqual(description, result.Description);
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("   ")]
        public void Should_ThrowArgumentNullException_When_CreatingCustomOperation_WithInvalidName(string value)
        {
            Assert.ThrowsException<ArgumentNullException>(() => ActionType.CreateCustomOperation(value, "TestValue514114165"));
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("   ")]
        public void Should_ThrowArgumentNullException_When_CreatingCustomOperation_WithInvalidDescription(string value)
        {
            Assert.ThrowsException<ArgumentNullException>(() => ActionType.CreateCustomOperation("TestValue1497588783", value));
        }

        [TestMethod]
        public void Should_Map_NameAndDescription_When_CreatingCustomOperation()
        {
            var name = "TestValue765520547";
            var description = "TestValue134813388";
            var result = ActionType.CreateCustomOperation(name, description);
            Assert.AreEqual(name, result.Name);
            Assert.AreEqual(description, result.Description);
        }

        [TestMethod]
        public void Should_ReturnCorrectName_ForCustomOperation()
        {
            var name = "TestValue2029983520";
            var description = "TestValue1891303237";
            _testClass = ActionType.CreateCustomOperation(name, description);
            Assert.IsInstanceOfType(_testClass.Name, typeof(string));
            Assert.AreEqual(name, _testClass.Name);
        }

        [TestMethod]
        public void Should_ReturnCorrectDescription_ForCustomOperation()
        {
            var name = "TestValue765520947";
            var description = "TestValue1891303237";
            _testClass = ActionType.CreateCustomOperation(name, description);
            Assert.IsInstanceOfType(_testClass.Description, typeof(string));
            Assert.AreEqual(description, _testClass.Description);
        }

        [TestMethod]
        public void Should_ReturnAddOperation_WithCorrectProperties()
        {
            Assert.IsInstanceOfType(ActionType.Add, typeof(ActionType));
            Assert.AreEqual("Add", ActionType.Add.Name);
            Assert.AreEqual("Add a new record.", ActionType.Add.Description);
        }

        [TestMethod]
        public void Should_ReturnModifiedOperation_WithCorrectProperties()
        {
            Assert.IsInstanceOfType(ActionType.Modified, typeof(ActionType));
            Assert.AreEqual("Modified", ActionType.Modified.Name);
            Assert.AreEqual("Modify an existing record.", ActionType.Modified.Description);
        }

        [TestMethod]
        public void Should_ReturnRemoveOperation_WithCorrectProperties()
        {
            Assert.IsInstanceOfType(ActionType.Remove, typeof(ActionType));
            Assert.AreEqual("Remove", ActionType.Remove.Name);
            Assert.AreEqual("Remove an existing record.", ActionType.Remove.Description);
        }

        [TestMethod]
        public void Should_ReturnDeactivateOperation_WithCorrectProperties()
        {
            Assert.IsInstanceOfType(ActionType.Deactivate, typeof(ActionType));
            Assert.AreEqual("Deactivate", ActionType.Deactivate.Name);
            Assert.AreEqual("Deactivate an existing record.", ActionType.Deactivate.Description);
        }

        [TestMethod]
        public void Should_ReturnActivateOperation_WithCorrectProperties()
        {
            Assert.IsInstanceOfType(ActionType.Activate, typeof(ActionType));
            Assert.AreEqual("Activate", ActionType.Activate.Name);
            Assert.AreEqual("Activate a deactivated record.", ActionType.Activate.Description);
        }

        [TestMethod]
        public void Should_ReturnGetUserByIdOperation_WithCorrectProperties()
        {
            Assert.IsInstanceOfType(ActionType.GetUserById, typeof(ActionType));
            Assert.AreEqual("GetUserById", ActionType.GetUserById.Name);
            Assert.AreEqual("Retrieve a user by their ID.", ActionType.GetUserById.Description);
        }

        [TestMethod]
        public void Should_ReturnGetAllByFilterOperation_WithCorrectProperties()
        {
            Assert.IsInstanceOfType(ActionType.GetAllByFilter, typeof(ActionType));
            Assert.AreEqual("GetAllByFilter", ActionType.GetAllByFilter.Name);
            Assert.AreEqual("Retrieve all records that match a given filter.", ActionType.GetAllByFilter.Description);
        }

        [TestMethod]
        public void Should_ReturnGetPageByFilterOperation_WithCorrectProperties()
        {
            Assert.IsInstanceOfType(ActionType.GetPageByFilter, typeof(ActionType));
            Assert.AreEqual("GetPageByFilter", ActionType.GetPageByFilter.Name);
            Assert.AreEqual("Retrieve a page of records that match a given filter.", ActionType.GetPageByFilter.Description);
        }

        [TestMethod]
        public void Should_ReturnGetCountFilterOperation_WithCorrectProperties()
        {
            Assert.IsInstanceOfType(ActionType.GetCountFilter, typeof(ActionType));
            Assert.AreEqual("GetCountFilter", ActionType.GetCountFilter.Name);
            Assert.AreEqual("Get the count of records that match a given filter.", ActionType.GetCountFilter.Description);
        }

        [TestMethod]
        public void Should_ThrowInvalidOperationException_When_CreatingOperationWithExistingName()
        {
            var name = "Add";
            var description = "Some other description";
            Assert.ThrowsException<InvalidOperationException>(() => ActionType.CreateCustomOperation(name, description));
        }

        [TestMethod]
        public void Should_EnsurePredefinedOperationsAreSingletons()
        {
            Assert.AreSame(ActionType.Add, ActionType.Add);
            Assert.AreSame(ActionType.Modified, ActionType.Modified);
            Assert.AreSame(ActionType.Remove, ActionType.Remove);
            Assert.AreSame(ActionType.Deactivate, ActionType.Deactivate);
            Assert.AreSame(ActionType.Activate, ActionType.Activate);
        }

        [TestMethod]
        public void Should_OperationEqualItself()
        {
            var name = "TestValue2123520947";
            var description = "TestValue1891303237";
            _testClass = ActionType.CreateCustomOperation(name, description);
            Assert.IsTrue(_testClass.Equals(_testClass));
            Assert.AreEqual(_testClass.GetHashCode(), _testClass.GetHashCode());
        }

        [TestMethod]
        public void Should_OperationNotEqualNull()
        {
            var name = "TestValue76523431947";
            var description = "TestValue1891303237";
            _testClass = ActionType.CreateCustomOperation(name, description);
            Assert.IsFalse(_testClass.Equals(null));
        }

        [TestMethod]
        public void Should_CreateCustomOperation_SetCorrectNameAndDescription()
        {
            var operation = ActionType.CreateCustomOperation("CustomOp", "This is a custom operation");
            Assert.IsNotNull(operation);
            Assert.AreEqual("CustomOp", operation.Name);
            Assert.AreEqual("This is a custom operation", operation.Description);
        }

        [TestMethod]
        public void Should_ReturnExpectedName_When_GettingNameForPredefinedOperation()
        {
            var operation = ActionType.Add;
            var name = ActionType.GetName(operation);
            Assert.AreEqual("Add", name);
        }

        [TestMethod]
        public void Should_ThrowArgumentNullException_When_PassingNullToGetName()
        {
            ActionType operation = null;
            Assert.ThrowsException<ArgumentNullException>(() => ActionType.GetName(operation));
        }
    }
}