namespace FlouPoint.Test.EnumType
{
    using NUnit.Framework;
    using FluentAssertions;
    using System;
    using Domain.EnumType;

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
        [EnumMetadata("FIRST_VALUE", "First value description.")]
        FirstValue,

        [EnumMetadata("SECOND_VALUE", "Second value description.")]
        SecondValue,

        // Enum value without metadata to test default behavior
        NoMetadataValue,

        [EnumMetadata("DUPLICATE_NAME", "First duplicate description.")]
        DuplicateValueOne,

        [EnumMetadata("DUPLICATE_NAME", "Second duplicate description.")]
        DuplicateValueTwo
    }

    [TestFixture]
    public class EnumExtensionsTests
    {
        [Test]
        public void GetCustomName_Should_Return_CustomName_When_MetadataExists()
        {
            // Given
            var enumValue = SampleEnum.FirstValue;
            var expectedName = "FIRST_VALUE";

            // When
            var customName = enumValue.GetCustomName();

            // Then
            customName.Should().Be(expectedName);
        }

        [Test]
        public void GetCustomName_Should_Return_UNKNOWN_When_MetadataDoesNotExist()
        {
            // Given
            var enumValue = SampleEnum.NoMetadataValue;

            // When
            var customName = enumValue.GetCustomName();

            // Then
            customName.Should().Be("UNKNOWN");
        }

        [Test]
        public void GetDescription_Should_Return_Description_When_MetadataExists()
        {
            // Given
            var enumValue = SampleEnum.SecondValue;
            var expectedDescription = "Second value description.";

            // When
            var description = enumValue.GetDescription();

            // Then
            description.Should().Be(expectedDescription);
        }

        [Test]
        public void GetDescription_Should_Return_DefaultMessage_When_MetadataDoesNotExist()
        {
            // Given
            var enumValue = SampleEnum.NoMetadataValue;

            // When
            var description = enumValue.GetDescription();

            // Then
            description.Should().Be("Description not available.");
        }

        [Test]
        public void GetEnumByName_Should_Return_EnumValue_When_ValidNameIsProvided()
        {
            // Given
            var enumName = "FirstValue";

            // When
            var enumValue = enumName.GetEnumByName<SampleEnum>();

            // Then
            enumValue.Should().Be(SampleEnum.FirstValue);
        }

        [Test]
        public void GetEnumByName_Should_Throw_ArgumentException_When_InvalidNameIsProvided()
        {
            // Given
            var invalidName = "InvalidValue";

            // When
            Action action = () => invalidName.GetEnumByName<SampleEnum>();

            // Then
            action.Should().Throw<ArgumentException>()
                .WithMessage($"No enum value found for {invalidName} in {typeof(SampleEnum)}");
        }

        [Test]
        public void GetEnumByName_Should_Throw_ArgumentException_When_NameIsNull()
        {
            // Given
            string nullName = null;

            // When
            Action action = () => nullName.GetEnumByName<SampleEnum>();

            // Then
            action.Should().Throw<ArgumentException>()
                .WithMessage($"No enum value found for  in {typeof(SampleEnum)}");
        }

        [Test]
        public void GetEnumByName_Should_Be_CaseInsensitive()
        {
            // Given
            var enumName = "firstvalue";

            // When
            var enumValue = enumName.GetEnumByName<SampleEnum>();

            // Then
            enumValue.Should().Be(SampleEnum.FirstValue);
        }

        [Test]
        public void GetCustomName_Should_Handle_Duplicate_MetadataNames()
        {
            // Given
            var enumValue = SampleEnum.DuplicateValueOne;

            // When
            var customName = enumValue.GetCustomName();

            // Then
            customName.Should().Be("DUPLICATE_NAME");
        }

        [Test]
        public void GetDescription_Should_Handle_Duplicate_MetadataDescriptions()
        {
            // Given
            var enumValue = SampleEnum.DuplicateValueTwo;

            // When
            var description = enumValue.GetDescription();

            // Then
            description.Should().Be("Second duplicate description.");
        }

        [Test]
        public void GetCustomName_Should_Throw_Exception_When_InvalidEnumValue()
        {
            // Given
            SampleEnum invalidEnumValue = (SampleEnum)999;

            // When
            Action action = () => invalidEnumValue.GetCustomName();

            // Then
            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void GetDescription_Should_Return_DefaultMessage_When_InvalidEnumValue()
        {
            // Given
            SampleEnum invalidEnumValue = (SampleEnum)999;

            // When
            var description = invalidEnumValue.GetDescription();

            // Then
            description.Should().Be("Description not available.");
        }

        [Test]
        public void GetEnumByName_Should_Throw_ArgumentException_When_EmptyStringProvided()
        {
            // Given
            var emptyName = string.Empty;

            // When
            Action action = () => emptyName.GetEnumByName<SampleEnum>();

            // Then
            action.Should().Throw<ArgumentException>();
        }
    }
}