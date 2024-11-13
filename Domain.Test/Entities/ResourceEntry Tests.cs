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
        /// Initializes a new instance of the <see cref = "Resource"/> class before each test.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            _testClass = new Resource();
        }

        /// <summary>
        /// Tests that the <see cref = "Resource.Id"/> property can be set and retrieved correctly.
        /// </summary>
        [TestMethod]
        public void CanSetAndGetId()
        {
            // Arrange
            var testValue = "TestValue68172804";
            // Act
            _testClass.Id = testValue;
            // Assert
            Assert.AreEqual(testValue, _testClass.Id);
        }

        /// <summary>
        /// Tests that the <see cref = "Resource.Name"/> property can be set and retrieved correctly.
        /// </summary>
        [TestMethod]
        public void CanSetAndGetName()
        {
            // Arrange
            var testValue = "TestValue503865662";
            // Act
            _testClass.Name = testValue;
            // Assert
            Assert.AreEqual(testValue, _testClass.Name);
        }

        /// <summary>
        /// Tests that the <see cref = "Resource.Value"/> property can be set and retrieved correctly.
        /// </summary>
        [TestMethod]
        public void CanSetAndGetValue()
        {
            // Arrange
            var testValue = "TestValue1556702727";
            // Act
            _testClass.Value = testValue;
            // Assert
            Assert.AreEqual(testValue, _testClass.Value);
        }

        /// <summary>
        /// Tests that the <see cref = "Resource.Comment"/> property can be set and retrieved correctly.
        /// </summary>
        [TestMethod]
        public void CanSetAndGetComment()
        {
            // Arrange
            var testValue = "TestValue616474427";
            // Act
            _testClass.Comment = testValue;
            // Assert
            Assert.AreEqual(testValue, _testClass.Comment);
        }

        /// <summary>
        /// Tests that the <see cref = "Resource.Active"/> property can be set and retrieved correctly.
        /// </summary>
        [TestMethod]
        public void CanSetAndGetActive()
        {
            // Arrange
            var testValue = true;
            // Act
            _testClass.Active = testValue;
            // Assert
            Assert.AreEqual(testValue, _testClass.Active);
        }

        /// <summary>
        /// Verifies that a newly created <see cref = "Resource"/> object has default empty properties 
        /// and is inactive.
        /// </summary>
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

        /// <summary>
        /// Tests that the <see cref = "Resource.Id"/> property stores its value correctly.
        /// </summary>
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

        /// <summary>
        /// Tests that the <see cref = "Resource.Name"/> property stores its value correctly.
        /// </summary>
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

        /// <summary>
        /// Tests that the <see cref = "Resource.Value"/> property stores its value correctly.
        /// </summary>
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

        /// <summary>
        /// Tests that the <see cref = "Resource.Comment"/> property stores its value correctly.
        /// </summary>
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

        /// <summary>
        /// Tests that the <see cref = "Resource.Active"/> property stores its value correctly.
        /// </summary>
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