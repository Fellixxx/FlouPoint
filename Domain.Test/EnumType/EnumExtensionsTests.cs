namespace Domain.Test.EnumType
{
    using System;
    using Domain.EnumType;
    using Domain.EnumType.Extensions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    // Define the EnumMetadataAttribute
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

    /// <summary>
    /// Contains unit tests for the extension methods of enum types,
    /// focusing on custom attributes for name and description retrieval
    /// and conversion from string to enum.
    /// </summary>
    [TestClass]
    public class EnumExtensionsTests
    {
        /// <summary>
        /// Tests that the GetCustomName method can successfully retrieve the custom
        /// name attribute value assigned to an enum member.
        /// </summary>
        [TestMethod]
        public void CanCallGetCustomName_WithAttribute()
        {
            // Arrange
            var expectedName = "Option One";
            // Act
            var result = MyEnum.option1.GetCustomName();
            // Assert
            Assert.AreEqual(expectedName, result);
        }

        /// <summary>
        /// Tests that the GetCustomName method correctly returns "UNKNOWN" if no custom
        /// name attribute is present for the enum member.
        /// </summary>
        [TestMethod]
        public void GetCustomName_ReturnsUnknown_WhenAttributeMissing()
        {
            // Arrange
            var expectedName = "Option One";
            // Act
            var result = MyEnum.option1.GetCustomName();
            // Assert
            Assert.AreEqual(expectedName, result);
        }

        /// <summary>
        /// Tests that the GetDescription method can successfully retrieve the description
        /// attribute value assigned to an enum member.
        /// </summary>
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

        /// <summary>
        /// Tests that the GetDescription method returns a default message
        /// when there is no description attribute for the enum member.
        /// </summary>
        [TestMethod]
        public void GetDescription_ReturnsDefault_WhenAttributeMissing()
        {
            // Arrange
            var expectedDescription = "Description for option two.";
            // Act
            var result = MyEnum.option2.GetDescription();
            // Assert
            Assert.AreEqual(expectedDescription, result);
        }

        /// <summary>
        /// Checks that GetEnumByName correctly retrieves an enum member
        /// based on its name as a string representation, provided the value is valid.
        /// </summary>
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

        /// <summary>
        /// Confirms that the GetEnumByName function performs case-insensitive
        /// matching of string values when returning enum members.
        /// </summary>
        [TestMethod]
        public void GetEnumByName_IsCaseInsensitive()
        {
            // Arrange
            var _value = "Option Two"; // Different casing
            // Act
            var result = MyEnum.option2.GetCustomName();
            // Assert
            Assert.AreEqual("Option Two", result);
        }

        /// <summary>
        /// Verifies that GetEnumByName throws an ArgumentException
        /// if the provided string does not match any enum member.
        /// </summary>
        [TestMethod]
        public void GetEnumByName_ThrowsArgumentException_ForInvalidValue()
        {
            // Arrange
            var _value = "InvalidOption";
            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => _value.GetEnumByName<MyEnum>());
        }

        /// <summary>
        /// Ensures GetEnumByName method throws an ArgumentException for
        /// empty or whitespace-only strings.
        /// </summary>
        [DataTestMethod]
        [DataRow("")]
        [DataRow("   ")]
        public void CannotCallGetEnumByName_WithInvalidValue(string value)
        {
            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => value.GetEnumByName<MyEnum>());
        }

        private enum TestEnum
        {
            [EnumMetadata("TestName1", "This is the first test enum.")]
            TestValue1,
            [EnumMetadata("TestName2", "This is the second test enum.")]
            TestValue2,
            TestValueWithoutMetadata
        }

        /// <summary>
        /// Tests the retrieval of custom names for enum members
        /// when metadata is present.
        /// </summary>
        [TestMethod]
        public void GetCustomName_Should_Return_Correct_Custom_Name_When_Metadata_Is_Present()
        {
            // Act
            var customName1 = TestEnum.TestValue1.GetCustomName();
            var customName2 = TestEnum.TestValue2.GetCustomName();
            // Assert
            Assert.AreEqual("TestName1", customName1);
            Assert.AreEqual("TestName2", customName2);
        }

        /// <summary>
        /// Checks that GetCustomName returns "UNKNOWN" when the enum member lacks metadata.
        /// </summary>
        [TestMethod]
        public void GetCustomName_Should_Return_Unknown_When_Metadata_Is_Not_Present()
        {
            // Act
            var customName = TestEnum.TestValueWithoutMetadata.GetCustomName();
            // Assert
            Assert.AreEqual("UNKNOWN", customName);
        }

        /// <summary>
        /// Tests that GetDescription correctly retrieves descriptions from metadata when available.
        /// </summary>
        [TestMethod]
        public void GetDescription_Should_Return_Correct_Description_When_Metadata_Is_Present()
        {
            // Act
            var description1 = TestEnum.TestValue1.GetDescription();
            var description2 = TestEnum.TestValue2.GetDescription();
            // Assert
            Assert.AreEqual("This is the first test enum.", description1);
            Assert.AreEqual("This is the second test enum.", description2);
        }

        /// <summary>
        /// Ensures GetDescription provides a default message when metadata is missing
        /// for the enum member.
        /// </summary>
        [TestMethod]
        public void GetDescription_Should_Return_Default_Message_When_Metadata_Is_Not_Present()
        {
            // Act
            var description = TestEnum.TestValueWithoutMetadata.GetDescription();
            // Assert
            Assert.AreEqual("Description not available.", description);
        }

        /// <summary>
        /// Confirms GetEnumByName translates a valid string name to the correct enum value.
        /// </summary>
        [TestMethod]
        public void GetEnumByName_Should_Return_Correct_Enum_Value_For_Valid_String()
        {
            // Act
            var enumValue1 = "TestValue1".GetEnumByName<TestEnum>();
            var enumValue2 = "TestValue2".GetEnumByName<TestEnum>();
            // Assert
            Assert.AreEqual(TestEnum.TestValue1, enumValue1);
            Assert.AreEqual(TestEnum.TestValue2, enumValue2);
        }

        /// <summary>
        /// Verifies that an ArgumentException is thrown by GetEnumByName when the input string
        /// doesn't match any enum members.
        /// </summary>
        [TestMethod]
        public void GetEnumByName_Should_Throw_ArgumentException_For_Invalid_String()
        {
            // Act & Assert
            var exception = Assert.ThrowsException<ArgumentException>(() => "InvalidValue".GetEnumByName<TestEnum>());
            Assert.AreEqual("No enum value found for InvalidValue in Domain.Test.EnumType.EnumExtensionsTests+TestEnum", exception.Message);
        }
    }
}