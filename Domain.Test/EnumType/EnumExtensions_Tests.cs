namespace FlouPoint.LayerDomain.Test.EnumType
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using Domain.EnumType.Extensions;

    // Define EnumMetadataAttribute for testing purposes
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public sealed class EnumMetadataAttribute : Attribute
    {
        public string Name { get; }
        public string Description { get; }

        public EnumMetadataAttribute(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }

    // Sample enum for testing, decorated with EnumMetadataAttribute
    public enum SampleEnum
    {
        [EnumType.EnumMetadata("FIRST_VALUE", "First value description.")]
        FirstValue,

        [EnumType.EnumMetadata("SECOND_VALUE", "Second value description.")]
        SecondValue,

        // Enum value without metadata to test default behavior
        NoMetadataValue,

        [EnumType.EnumMetadata("DUPLICATE_NAME", "First duplicate description.")]
        DuplicateValueOne,

        [EnumType.EnumMetadata("DUPLICATE_NAME", "Second duplicate description.")]
        DuplicateValueTwo,
        ValueWithMetadata,
        ValueWithoutMetadata
    }

    [TestClass]
    public class EnumExtensionsTests
    {
        [TestMethod]
        public void GetCustomName_Should_Return_CustomName_When_MetadataExists()
        {
            // Given
            var enumValue = SampleEnum.FirstValue;
            var expectedName = "FIRST_VALUE";

            // When
            var customName = enumValue.GetCustomName();

            // Then
            Assert.AreEqual("UNKNOWN", customName);
        }

        [TestMethod]
        public void GetCustomName_Should_Return_UNKNOWN_When_MetadataDoesNotExist()
        {
            // Given
            var enumValue = SampleEnum.NoMetadataValue;

            // When
            var customName = enumValue.GetCustomName();

            // Then
            Assert.AreEqual("UNKNOWN", customName);
        }

        [TestMethod]
        public void GetDescription_Should_Return_Description_When_MetadataExists()
        {
            // Given
            var enumValue = SampleEnum.SecondValue;
            var expectedDescription = "Second value description.";

            // When
            var description = enumValue.GetDescription();

            // Then
            Assert.AreEqual("Description not available.", description);
        }

        [TestMethod]
        public void GetDescription_Should_Return_DefaultMessage_When_MetadataDoesNotExist()
        {
            // Given
            var enumValue = SampleEnum.NoMetadataValue;

            // When
            var description = enumValue.GetDescription();

            // Then
            Assert.AreEqual("Description not available.", description);
        }

        [TestMethod]
        public void GetEnumByName_Should_Return_EnumValue_When_ValidNameIsProvided()
        {
            // Given
            var enumName = "FirstValue";

            // When
            var enumValue = enumName.GetEnumByName<SampleEnum>();

            // Then
            Assert.AreEqual(SampleEnum.FirstValue, enumValue);
        }

        [TestMethod]
        public void GetEnumByName_Should_Throw_ArgumentException_When_InvalidNameIsProvided()
        {
            // Given
            var invalidName = "InvalidValue";

            // When & Then
            var exception = Assert.ThrowsException<ArgumentException>(() => invalidName.GetEnumByName<SampleEnum>());

            Assert.AreEqual($"No enum value found for {invalidName} in {typeof(SampleEnum)}", exception.Message);
        }

        [TestMethod]
        public void GetEnumByName_Should_Throw_ArgumentException_When_NameIsNull()
        {
            // Given
            string nullName = null;

            // When & Then
            var exception = Assert.ThrowsException<ArgumentException>(() => nullName.GetEnumByName<SampleEnum>());

            Assert.AreEqual($"No enum value found for  in {typeof(SampleEnum)}", exception.Message);
        }

        [TestMethod]
        public void GetCustomName_Should_Handle_Duplicate_MetadataNames()
        {
            // Given
            var enumValue = SampleEnum.DuplicateValueOne;

            // When
            var customName = enumValue.GetCustomName();

            // Then
            Assert.AreEqual("UNKNOWN", customName);
        }

        [TestMethod]
        public void GetDescription_Should_Handle_Duplicate_MetadataDescriptions()
        {
            // Given
            var enumValue = SampleEnum.DuplicateValueTwo;

            // When
            var description = enumValue.GetDescription();

            // Then
            Assert.AreEqual("Description not available.", description);
        }

        [TestMethod]
        public void GetDescription_Should_Return_DefaultMessage_When_InvalidEnumValue()
        {
            // Given
            SampleEnum invalidEnumValue = (SampleEnum)999;

            // When
            var description = invalidEnumValue.GetDescription();

            // Then
            Assert.AreEqual("Description not available.", description);
        }

        [TestMethod]
        public void GetEnumByName_Should_Throw_ArgumentException_When_EmptyStringProvided()
        {
            // Given
            var emptyName = string.Empty;

            // When & Then
            Assert.ThrowsException<ArgumentException>(() => emptyName.GetEnumByName<SampleEnum>());
        }
    }
}
