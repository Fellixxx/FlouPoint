namespace Domain.Test.EnumType
{
    using System;
    using Domain.EnumType;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    // Define the EnumMetadataAttribute
    [AttributeUsage(AttributeTargets.Field)]
    public class EnumMetadataAttribute : Attribute
    {
        public string Name { get; }
        public string Description { get; }

        public EnumMetadataAttribute(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }

    // Define the enum with EnumMetadataAttribute
    public enum MyEnum
    {
        [EnumMetadata("Option One", "Description for option one.")]
        option1,

        [EnumMetadata("Option Two", "Description for option two.")]
        option2,

        [EnumMetadata("Option Three", "Description for option three.")]
        option3
    }


    [TestClass]
    public class EnumExtensionsTests
    {
        [TestMethod]
        public void CanCallGetCustomName_WithAttribute()
        {
            // Arrange
            var expectedName = "UNKNOWN";

            // Act
            var result = MyEnum.option1.GetCustomName<MyEnum>();

            // Assert
            Assert.AreEqual(expectedName, result);
        }

        [TestMethod]
        public void GetCustomName_ReturnsUnknown_WhenAttributeMissing()
        {
            // Arrange
            var expectedName = "UNKNOWN";

            // Act
            var result = MyEnum.option1.GetCustomName();

            // Assert
            Assert.AreEqual(expectedName, result);
        }

        [TestMethod]
        public void CanCallGetDescription_WithAttribute()
        {
            // Arrange
            var expectedDescription = "Description for option two.";

            // Act
            var result = MyEnum.option2.GetDescription();

            // Assert
            Assert.AreEqual(expectedDescription, result);
        }

        [TestMethod]
        public void GetDescription_ReturnsDefault_WhenAttributeMissing()
        {
            // Arrange
            var expectedDescription = "Description not available.";

            // Act
            var result = MyEnum.option2.GetDescription();

            // Assert
            Assert.AreEqual(expectedDescription, result);
        }

        [TestMethod]
        public void CanCallGetEnumByName_ValidValue()
        {
            // Arrange
            var _value = "option3";

            // Act
            var result = _value.GetEnumByName<MyEnum>();

            // Assert
            Assert.AreEqual(MyEnum.option3, result);
        }

        [TestMethod]
        public void GetEnumByName_IsCaseInsensitive()
        {
            // Arrange
            var _value = "Option2"; // Different casing

            // Act
            var result = _value.GetEnumByName<MyEnum>();

            // Assert
            Assert.AreEqual(MyEnum.option2, result);
        }

        [TestMethod]
        public void GetEnumByName_ThrowsArgumentException_ForInvalidValue()
        {
            // Arrange
            var _value = "InvalidOption";

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => _value.GetEnumByName<MyEnum>());
        }

        [TestMethod]
        public void GetEnumByName_ThrowsArgumentNullException_ForNullValue()
        {
            // Arrange
            string _value = null;

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => _value.GetEnumByName<MyEnum>());
        }

        [DataTestMethod]
        [DataRow("")]
        [DataRow("   ")]
        public void CannotCallGetEnumByName_WithInvalidValue(string value)
        {
            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => value.GetEnumByName<MyEnum>());
        }
    }
}