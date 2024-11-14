namespace Domain.Test.EnumType
{
    using System;
    using Domain.EnumType.Extensions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// This class contains unit tests for the <c>EnumMetadata</c> class, which is used to attach metadata to enum type fields.
    /// The tests ensure that the <c>EnumMetadata</c> is correctly constructed, initialized, and applied to enum fields.
    /// </summary>
    [TestClass]
    public class EnumMetadataAttributeTests
    {
        private EnumMetadata _testClass;
        private string _name;
        private string _description;
        /// <summary>
        /// Sets up the test by initializing the required variables and creating an instance of <c>EnumMetadata</c>
        /// with a predefined name and description.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            _name = "TestValue1974655783";
            _description = "TestValue680922702";
            _testClass = new EnumMetadata(_name, _description);
        }

        /// <summary>
        /// Tests whether an instance of <c>EnumMetadata</c> can be successfully constructed.
        /// </summary>
        [TestMethod]
        public void ShouldConstructEnumMetadata()
        {
            // Act
            var instance = new EnumMetadata(_name, _description);
            // Assert
            Assert.IsNotNull(instance);
        }

        /// <summary>
        /// Tests that constructing <c>EnumMetadata</c> with an invalid (null or whitespace) name throws an <c>ArgumentNullException</c>.
        /// </summary>
        /// <param name = "value">An invalid name.</param>
        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("   ")]
        public void ShouldThrowArgumentNullExceptionForInvalidName(string value)
        {
            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new EnumMetadata(value, _description));
        }

        /// <summary>
        /// Tests that constructing <c>EnumMetadata</c> with an invalid (null or whitespace) description throws an <c>ArgumentNullException</c>.
        /// </summary>
        /// <param name = "value">An invalid description.</param>
        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("   ")]
        public void ShouldThrowArgumentNullExceptionForInvalidDescription(string value)
        {
            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new EnumMetadata(_name, value));
        }

        /// <summary>
        /// Verifies that the <c>Name</c> property of <c>EnumMetadata</c> is initialized correctly.
        /// </summary>
        [TestMethod]
        public void ShouldInitializeNameCorrectly()
        {
            // Assert
            Assert.AreEqual(_name, _testClass.Name);
        }

        /// <summary>
        /// Verifies that the <c>Description</c> property of <c>EnumMetadata</c> is initialized correctly.
        /// </summary>
        [TestMethod]
        public void ShouldInitializeDescriptionCorrectly()
        {
            // Assert
            Assert.AreEqual(_description, _testClass.Description);
        }

        /// <summary>
        /// Tests that <c>EnumMetadata</c> correctly stores the name and description when initialized.
        /// </summary>
        [TestMethod]
        public void ShouldStoreNameAndDescriptionCorrectly()
        {
            // Arrange
            string expectedName = "TestName";
            string expectedDescription = "This is a test description for the enum field.";
            // Act
            var enumMetadata = new EnumMetadata(expectedName, expectedDescription);
            // Assert
            Assert.AreEqual(expectedName, enumMetadata.Name);
            Assert.AreEqual(expectedDescription, enumMetadata.Description);
        }

        /// <summary>
        /// Ensures that no exception is thrown when <c>EnumMetadata</c> is initialized with valid parameters.
        /// </summary>
        [TestMethod]
        public void ShouldNotThrowExceptionWhenInitializedWithValidParameters()
        {
            // Act & Assert
            new EnumMetadata("ValidName", "Valid description.");
        }

        /// <summary>
        /// Validates that the <c>EnumMetadata</c> attribute is correctly applied to enum fields.
        /// </summary>
        [TestMethod]
        public void ShouldApplyEnumMetadataAttributeToEnumFields()
        {
            // Arrange & Act
            var enumType = typeof(TestEnumWithMetadata);
            var fieldInfo = enumType.GetField(nameof(TestEnumWithMetadata.TestValue));
            // Assert
            var attribute = (EnumMetadata)Attribute.GetCustomAttribute(fieldInfo, typeof(EnumMetadata));
            Assert.IsNotNull(attribute);
            Assert.AreEqual("TestValueName", attribute.Name);
            Assert.AreEqual("This is a description for TestValue.", attribute.Description);
        }

        /// <summary>
        /// A test enum used to demonstrate and verify the application of the <c>EnumMetadata</c> attribute.
        /// </summary>
        private enum TestEnumWithMetadata
        {
            [EnumMetadata("TestValueName", "This is a description for TestValue.")]
            TestValue
        }
    }
}