namespace Domain.Test.Entities
{
    using System;
    using Domain.Entities;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Unit tests for the <see cref = "Resource"/> class, which verifies functionality 
    /// for setting and getting various properties of a resource entry.
    /// </summary>
    [TestClass]
    public class ResourceEntryTests
    {
        private Resource _testClass;
        /// <summary>
        /// Initializes a new instance of the <see cref = "Resource"/> class for testing.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            _testClass = new Resource();
        }

        /// <summary>
        /// Tests that various properties of the <see cref = "Resource"/> can be set and retrieved correctly.
        /// </summary>
        /// <param name = "propertyName">The name of the property to test.</param>
        /// <param name = "testValue">The value to set the property to.</param>
        [DataTestMethod]
        [DataRow("Id", "TestValue68172804")]
        [DataRow("Name", "TestValue503865662")]
        [DataRow("Value", "TestValue1556702727")]
        [DataRow("Comment", "TestValue616474427")]
        public void Properties_Should_Be_Set_And_Retrieved_Correctly(string propertyName, string testValue)
        {
            SetPropertyValue(propertyName, testValue);
            string actualValue = GetPropertyValue<string>(propertyName);
            Assert.AreEqual(testValue, actualValue);
        }

        /// <summary>
        /// Tests that the <see cref = "Active"/> property can be set and retrieved correctly.
        /// </summary>
        [TestMethod]
        public void Active_Property_Should_Be_Set_And_Retrieved_Correctly()
        {
            var testValue = true;
            _testClass.Active = testValue;
            Assert.IsTrue(_testClass.Active);
        }

        /// <summary>
        /// Tests that a new <see cref = "Resource"/> instance has default empty properties and 
        /// an inactive status.
        /// </summary>
        [TestMethod]
        public void New_ResourceEntry_Should_Have_Default_Empty_Properties_And_Inactive_Status()
        {
            var resourceEntry = new Resource();
            Assert.AreEqual(string.Empty, resourceEntry.Id);
            Assert.AreEqual(string.Empty, resourceEntry.Name);
            Assert.AreEqual(string.Empty, resourceEntry.Value);
            Assert.AreEqual(string.Empty, resourceEntry.Comment);
            Assert.IsFalse(resourceEntry.Active);
        }

        /// <summary>
        /// Tests that the <see cref = "Active"/> property is stored correctly for a <see cref = "Resource"/> instance.
        /// </summary>
        [TestMethod]
        public void Active_Property_Should_Be_Stored_Correctly()
        {
            bool expectedActive = true;
            var resourceEntry = new Resource
            {
                Active = expectedActive
            };
            Assert.IsTrue(resourceEntry.Active);
        }

        /// <summary>
        /// Sets the value of a property on the <see cref = "Resource"/> instance.
        /// </summary>
        /// <param name = "propertyName">The name of the property to set.</param>
        /// <param name = "value">The value to set the property to.</param>
        private void SetPropertyValue(string propertyName, object value)
        {
            var property = _testClass.GetType().GetProperty(propertyName);
            property.SetValue(_testClass, value);
        }

        /// <summary>
        /// Retrieves the value of a property from the <see cref = "Resource"/> instance.
        /// </summary>
        /// <typeparam name = "T">The type of the property value.</typeparam>
        /// <param name = "propertyName">The name of the property to retrieve.</param>
        /// <returns>The value of the property.</returns>
        private T GetPropertyValue<T>(string propertyName)
        {
            var property = _testClass.GetType().GetProperty(propertyName);
            return (T)property.GetValue(_testClass);
        }
    }
}