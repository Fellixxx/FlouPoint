namespace Domain.Test.Entities
{
    using System;
    using Domain.Entities;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ResourceEntryTests
    {
        private Resource _testClass;

        [TestInitialize]
        public void SetUp()
        {
            _testClass=new Resource();
        }

        [TestMethod]
        public void CanSetAndGetId()
        {
            // Arrange
            var testValue = "TestValue68172804";

            // Act
            _testClass.Id=testValue;

            // Assert
            Assert.AreEqual(testValue, _testClass.Id);
        }

        [TestMethod]
        public void CanSetAndGetName()
        {
            // Arrange
            var testValue = "TestValue503865662";

            // Act
            _testClass.Name=testValue;

            // Assert
            Assert.AreEqual(testValue, _testClass.Name);
        }

        [TestMethod]
        public void CanSetAndGetValue()
        {
            // Arrange
            var testValue = "TestValue1556702727";

            // Act
            _testClass.Value=testValue;

            // Assert
            Assert.AreEqual(testValue, _testClass.Value);
        }

        [TestMethod]
        public void CanSetAndGetComment()
        {
            // Arrange
            var testValue = "TestValue616474427";

            // Act
            _testClass.Comment=testValue;

            // Assert
            Assert.AreEqual(testValue, _testClass.Comment);
        }

        [TestMethod]
        public void CanSetAndGetActive()
        {
            // Arrange
            var testValue = true;

            // Act
            _testClass.Active=testValue;

            // Assert
            Assert.AreEqual(testValue, _testClass.Active);
        }

        [TestMethod]
        public void ResourceEntry_Should_Have_Default_Empty_Properties_And_Inactive_Status()
        {
            // Act
            var resourceEntry = new Resource();

            // Assert
            Assert.AreEqual(resourceEntry.Id, string.Empty);
            Assert.AreEqual(resourceEntry.Name, string.Empty);
            Assert.AreEqual(resourceEntry.Value, string.Empty);
            Assert.AreEqual(resourceEntry.Comment, string.Empty);
            Assert.AreEqual(resourceEntry.Active, false);
        }

        [TestMethod]
        public void ResourceEntry_Should_Store_Id_Property_Correctly()
        {
            // Arrange
            string expectedId = "12345";

            // Act
            var resourceEntry = new Resource
            {
                Id = expectedId
            };

            // Assert
            Assert.AreEqual(resourceEntry.Id, expectedId);
        }

        [TestMethod]
        public void ResourceEntry_Should_Store_Name_Property_Correctly()
        {
            // Arrange
            string expectedName = "SampleResource";

            // Act
            var resourceEntry = new Resource
            {
                Name = expectedName
            };

            // Assert
            Assert.AreEqual(resourceEntry.Name, expectedName);
        }

        [TestMethod]
        public void ResourceEntry_Should_Store_Value_Property_Correctly()
        {
            // Arrange
            string expectedValue = "ResourceValue";

            // Act
            var resourceEntry = new Resource
            {
                Value = expectedValue
            };

            // Assert
            Assert.AreEqual(resourceEntry.Value, expectedValue);
        }

        [TestMethod]
        public void ResourceEntry_Should_Store_Comment_Property_Correctly()
        {
            // Arrange
            string expectedComment = "This is a test comment.";

            // Act
            var resourceEntry = new Resource
            {
                Comment = expectedComment
            };

            // Assert
            Assert.AreEqual(resourceEntry.Comment, expectedComment);
        }

        [TestMethod]
        public void ResourceEntry_Should_Store_Active_Property_Correctly()
        {
            // Arrange
            bool expectedActive = true;

            // Act
            var resourceEntry = new Resource
            {
                Active = expectedActive
            };

            // Assert
            Assert.AreEqual(resourceEntry.Active, true);
        }
    }
}