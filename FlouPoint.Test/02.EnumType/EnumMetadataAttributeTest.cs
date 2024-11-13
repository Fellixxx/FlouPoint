namespace FlouPoint.Test.EnumType
{
    using NUnit.Framework;
    using FluentAssertions;
    using System;
    using System.Reflection;
    using Domain.EnumType;

    /// <summary>
    /// Tests for verifying the functionality of the EnumMetadataAttribute.
    /// Ensures that the attribute correctly handles metadata for enum fields.
    /// </summary>
    [TestFixture]
    public class EnumMetadataAttributeTests
    {
        /// <summary>
        /// Tests the constructor of EnumMetadataAttribute to verify that 
        /// the 'Name' and 'Description' properties are set as expected.
        /// </summary>
        [Test]
        public void Constructor_Should_Set_Name_And_Description_Correctly()
        {
            // Given predefined expected name and description values.
            var expectedName = "SampleName";
            var expectedDescription = "Sample description for the enum field.";
            // When an instance of EnumMetadataAttribute is created with the expected values.
            var attribute = new EnumMetadataAttribute(expectedName, expectedDescription);
            // Then the actual attribute name and description should match the expected values.
            attribute.Name.Should().Be(expectedName);
            attribute.Description.Should().Be(expectedDescription);
        }

        /// <summary>
        /// Verifies that a specific enum field is annotated with EnumMetadataAttribute
        /// containing the correct name and description values.
        /// </summary>
        [Test]
        public void EnumField_Should_Have_EnumMetadataAttribute_With_Correct_Values()
        {
            // Given an enum field that should have the metadata attribute.
            var enumValue = SampleEnum.ValueWithMetadata;
            var expectedName = "ValueWithMetadataName";
            var expectedDescription = "Description for ValueWithMetadata.";
            // When retrieving the metadata attribute from the enum field.
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());
            var attribute = fieldInfo.GetCustomAttribute<EnumMetadataAttribute>();
            // Then the attribute should exist and contain the expected values.
            attribute.Should().NotBeNull();
            attribute.Name.Should().Be(expectedName);
            attribute.Description.Should().Be(expectedDescription);
        }

        /// <summary>
        /// Checks that attempting to retrieve the EnumMetadataAttribute from an 
        /// enum field without such an attribute returns null.
        /// </summary>
        [Test]
        public void EnumField_Without_Attribute_Should_Return_Null_When_Retrieving_EnumMetadataAttribute()
        {
            // Given an enum field that does not have the metadata attribute.
            var enumValue = SampleEnum.ValueWithoutMetadata;
            // When retrieving the metadata attribute from the enum field.
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());
            var attribute = fieldInfo.GetCustomAttribute<EnumMetadataAttribute>();
            // Then no attribute should be found (resulting in null).
            attribute.Should().BeNull();
        }

        /// <summary>
        /// Ensures that the EnumMetadataAttribute is intended to be used only on fields 
        /// and that it disallows multiple instances on a single field.
        /// </summary>
        [Test]
        public void AttributeUsage_Should_Target_Fields_And_Disallow_Multiple_Instances()
        {
            // Given the AttributeUsageAttribute metadata for EnumMetadataAttribute.
            var attributeUsage = typeof(EnumMetadataAttribute).GetCustomAttribute<AttributeUsageAttribute>();
            // Then it should target fields only, with multiple usage not allowed.
            attributeUsage.Should().NotBeNull();
            attributeUsage.ValidOn.Should().Be(AttributeTargets.Field);
            attributeUsage.AllowMultiple.Should().BeFalse();
        }

        /// <summary>
        /// Verifies that the 'Name' and 'Description' properties of the 
        /// EnumMetadataAttribute are read-only, meaning their values cannot be changed
        /// after object construction.
        /// </summary>
        [Test]
        public void Name_And_Description_Properties_Should_Be_ReadOnly()
        {
            // Given an instance of EnumMetadataAttribute.
            var attribute = new EnumMetadataAttribute("InitialName", "InitialDescription");
            // When attempting to change the properties (which should not be possible).
            // This is demonstrated through reflection to check that setters do not exist.
            var nameProperty = typeof(EnumMetadataAttribute).GetProperty("Name");
            var descriptionProperty = typeof(EnumMetadataAttribute).GetProperty("Description");
            // Then the properties should be effectively read-only (no setter available).
            nameProperty.CanWrite.Should().BeFalse();
            descriptionProperty.CanWrite.Should().BeFalse();
        }

        // Sample enum used for testing purposes
        public enum SampleEnum
        {
            // Enum field with metadata attribute for testing.
            [EnumMetadata("ValueWithMetadataName", "Description for ValueWithMetadata.")]
            ValueWithMetadata,
            // Enum field without metadata attribute for testing.
            ValueWithoutMetadata
        }
    }
}