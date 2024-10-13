namespace FlouPoint.LayerDomain.Test.EnumType
{
    using NUnit.Framework;
    using FluentAssertions;
    using System;
    using System.Reflection;
    using Domain.EnumType;

    [TestFixture]
    public class EnumMetadataAttributeTests
    {
        [Test]
        public void Constructor_Should_Set_Name_And_Description_Correctly()
        {
            // Given
            var expectedName = "SampleName";
            var expectedDescription = "Sample description for the enum field.";

            // When
            var attribute = new EnumMetadataAttribute(expectedName, expectedDescription);

            // Then
            attribute.Name.Should().Be(expectedName);
            attribute.Description.Should().Be(expectedDescription);
        }

        [Test]
        public void EnumField_Should_Have_EnumMetadataAttribute_With_Correct_Values()
        {
            // Given
            var enumValue = SampleEnum.ValueWithMetadata;
            var expectedName = "ValueWithMetadataName";
            var expectedDescription = "Description for ValueWithMetadata.";

            // When
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());
            var attribute = fieldInfo.GetCustomAttribute<EnumMetadataAttribute>();

            // Then
            attribute.Should().NotBeNull();
            attribute.Name.Should().Be(expectedName);
            attribute.Description.Should().Be(expectedDescription);
        }

        [Test]
        public void EnumField_Without_Attribute_Should_Return_Null_When_Retrieving_EnumMetadataAttribute()
        {
            // Given
            var enumValue = SampleEnum.ValueWithoutMetadata;

            // When
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());
            var attribute = fieldInfo.GetCustomAttribute<EnumMetadataAttribute>();

            // Then
            attribute.Should().BeNull();
        }

        [Test]
        public void AttributeUsage_Should_Target_Fields_And_Disallow_Multiple_Instances()
        {
            // Given
            var attributeUsage = typeof(EnumMetadataAttribute).GetCustomAttribute<AttributeUsageAttribute>();

            // Then
            attributeUsage.Should().NotBeNull();
            attributeUsage.ValidOn.Should().Be(AttributeTargets.Field);
            attributeUsage.AllowMultiple.Should().BeFalse();
        }

        [Test]
        public void Name_And_Description_Properties_Should_Be_ReadOnly()
        {
            // Given
            var attribute = new EnumMetadataAttribute("InitialName", "InitialDescription");

            // When
            // Properties should be read-only, meaning attempts to set them should not be possible.
            // The following would cause a compile-time error:
            // attribute.Name = "NewName";
            // attribute.Description = "NewDescription";

            // Then
            var nameProperty = typeof(EnumMetadataAttribute).GetProperty("Name");
            var descriptionProperty = typeof(EnumMetadataAttribute).GetProperty("Description");

            nameProperty.CanWrite.Should().BeFalse();
            descriptionProperty.CanWrite.Should().BeFalse();
        }

        // Sample enum for testing
        public enum SampleEnum
        {
            [EnumMetadata("ValueWithMetadataName", "Description for ValueWithMetadata.")]
            ValueWithMetadata,

            ValueWithoutMetadata
        }
    }
}
