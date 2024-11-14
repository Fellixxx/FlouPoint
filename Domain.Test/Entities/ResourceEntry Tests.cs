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
        [TestInitialize]
        public void SetUp()
        {
            _testClass = new Resource();
        }

        [DataTestMethod]
        [DataRow("Id", "TestValue68172804")]
        [DataRow("Name", "TestValue503865662")]
        [DataRow("Value", "TestValue1556702727")]
        [DataRow("Comment", "TestValue616474427")]
        public void CanSetAndGetProperties(string propertyName, string testValue)
        {
            SetPropertyValue(propertyName, testValue);
            string actualValue = GetPropertyValue<string>(propertyName);
            Assert.AreEqual(testValue, actualValue);
        }

        [TestMethod]
        public void CanSetAndGetActive()
        {
            var testValue = true;
            _testClass.Active = testValue;
            Assert.IsTrue(_testClass.Active);
        }

        [TestMethod]
        public void ResourceEntry_Should_Have_Default_Empty_Properties_And_Inactive_Status()
        {
            var resourceEntry = new Resource();
            Assert.AreEqual(string.Empty, resourceEntry.Id);
            Assert.AreEqual(string.Empty, resourceEntry.Name);
            Assert.AreEqual(string.Empty, resourceEntry.Value);
            Assert.AreEqual(string.Empty, resourceEntry.Comment);
            Assert.IsFalse(resourceEntry.Active);
        }

        [TestMethod]
        public void ResourceEntry_Should_Store_Active_Property_Correctly()
        {
            bool expectedActive = true;
            var resourceEntry = new Resource
            {
                Active = expectedActive
            };
            Assert.IsTrue(resourceEntry.Active);
        }

        private void SetPropertyValue(string propertyName, object value)
        {
            var property = _testClass.GetType().GetProperty(propertyName);
            property.SetValue(_testClass, value);
        }

        private T GetPropertyValue<T>(string propertyName)
        {
            var property = _testClass.GetType().GetProperty(propertyName);
            return (T)property.GetValue(_testClass);
        }
    }
}