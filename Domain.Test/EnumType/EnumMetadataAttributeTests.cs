namespace Domain.Test.EnumType
{
    using System;
    using Domain.EnumType;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class EnumMetadataAttributeTests
    {
        private EnumMetadataAttribute _testClass;
        private string _name;
        private string _description;

        [TestInitialize]
        public void SetUp()
        {
            _name="TestValue1974655783";
            _description="TestValue680922702";
            _testClass=new EnumMetadataAttribute(_name, _description);
        }

        [TestMethod]
        public void CanConstruct()
        {
            // Act
            var instance = new EnumMetadataAttribute(_name, _description);

            // Assert
            Assert.IsNotNull(instance);
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("   ")]
        public void CannotConstructWithInvalidName(string value)
        {
            Assert.ThrowsException<ArgumentNullException>(() => new EnumMetadataAttribute(value, _description));
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("   ")]
        public void CannotConstructWithInvalidDescription(string value)
        {
            Assert.ThrowsException<ArgumentNullException>(() => new EnumMetadataAttribute(_name, value));
        }

        [TestMethod]
        public void NameIsInitializedCorrectly()
        {
            Assert.AreEqual(_name, _testClass.Name);
        }

        [TestMethod]
        public void DescriptionIsInitializedCorrectly()
        {
            Assert.AreEqual(_description, _testClass.Description);
        }

        [TestMethod]
        public void EnumMetadataAttribute_Should_Store_Name_And_Description_Correctly()
        {
            // Arrange
            string expectedName = "TestName";
            string expectedDescription = "This is a test description for the enum field.";

            // Act
            var enumMetadataAttribute = new EnumMetadataAttribute(expectedName, expectedDescription);

            // Assert
            Assert.AreEqual(expectedName, enumMetadataAttribute.Name);
            Assert.AreEqual(expectedDescription, enumMetadataAttribute.Description);
        }

        [TestMethod]
        public void EnumMetadataAttribute_Should_Throw_No_Exception_When_Initialized_With_Valid_Parameters()
        {
            // Act & Assert
            try
            {
                new EnumMetadataAttribute("ValidName", "Valid description.");
            }
            catch (Exception ex)
            {
                Assert.Fail("An exception was thrown when initializing with valid parameters: " + ex.Message);
            }
        }

        [TestMethod]
        public void EnumMetadataAttribute_Should_Be_Applied_To_Enum_Fields()
        {
            // Arrange & Act
            var enumType = typeof(TestEnumWithMetadata);
            var fieldInfo = enumType.GetField(nameof(TestEnumWithMetadata.TestValue));

            // Assert
            var attribute = (EnumMetadataAttribute)Attribute.GetCustomAttribute(fieldInfo, typeof(EnumMetadataAttribute));
            Assert.IsNotNull(attribute);
            Assert.AreEqual("TestValueName", attribute.Name);
            Assert.AreEqual("This is a description for TestValue.", attribute.Description);
        }

        private enum TestEnumWithMetadata
        {
            [EnumMetadata("TestValueName", "This is a description for TestValue.")]
            TestValue
        }
    }
}