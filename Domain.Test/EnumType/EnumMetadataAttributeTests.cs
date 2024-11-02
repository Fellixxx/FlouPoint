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
    }
}