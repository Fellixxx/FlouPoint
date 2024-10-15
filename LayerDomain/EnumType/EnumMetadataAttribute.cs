using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayerDomain.EnumType
{
    using Domain.EnumType;
    using FluentAssertions;
    using NUnit.Framework;

    [TestFixture]
    public class EnumMetadataAttributeTests
    {
        [Test]
        public void EnumMetadataAttribute_Should_Store_Name_And_Description_Correctly()
        {
            // Arrange
            string expectedName = "TestName";
            string expectedDescription = "This is a test description for the enum field.";

            // Act
            var enumMetadataAttribute = new EnumMetadataAttribute(expectedName, expectedDescription);

            // Assert
            enumMetadataAttribute.Name.Should().Be(expectedName);
            enumMetadataAttribute.Description.Should().Be(expectedDescription);
        }

        [Test]
        public void EnumMetadataAttribute_Should_Throw_No_Exception_When_Initialized_With_Valid_Parameters()
        {
            // Act
            Action act = () => new EnumMetadataAttribute("ValidName", "Valid description.");

            // Assert
            act.Should().NotThrow();
        }

        [Test]
        public void EnumMetadataAttribute_Should_Be_Applied_To_Enum_Fields()
        {
            // Arrange & Act
            var enumType = typeof(TestEnumWithMetadata);
            var fieldInfo = enumType.GetField(nameof(TestEnumWithMetadata.TestValue));

            // Assert
            var attribute = (EnumMetadataAttribute)Attribute.GetCustomAttribute(fieldInfo, typeof(EnumMetadataAttribute));
            attribute.Should().NotBeNull();
            attribute.Name.Should().Be("TestValueName");
            attribute.Description.Should().Be("This is a description for TestValue.");
        }

        private enum TestEnumWithMetadata
        {
            [EnumMetadata("TestValueName", "This is a description for TestValue.")]
            TestValue
        }
    }
}
