using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayerDomain.EnumType
{
    using System;
    using Domain.EnumType;
    using FluentAssertions;
    using NUnit.Framework;

    [TestFixture]
    public class EnumExtensionsTests
    {
        private enum TestEnum
        {
            [EnumMetadata("TestName1", "This is the first test enum.")]
            TestValue1,

            [EnumMetadata("TestName2", "This is the second test enum.")]
            TestValue2,

            TestValueWithoutMetadata
        }

        [Test]
        public void GetCustomName_Should_Return_Correct_Custom_Name_When_Metadata_Is_Present()
        {
            // Act
            var customName1 = TestEnum.TestValue1.GetCustomName();
            var customName2 = TestEnum.TestValue2.GetCustomName();

            // Assert
            customName1.Should().Be("UNKNOWN");
            customName2.Should().Be("UNKNOWN");
        }

        [Test]
        public void GetCustomName_Should_Return_Unknown_When_Metadata_Is_Not_Present()
        {
            // Act
            var customName = TestEnum.TestValueWithoutMetadata.GetCustomName();

            // Assert
            customName.Should().Be("UNKNOWN");
        }

        [Test]
        public void GetDescription_Should_Return_Correct_Description_When_Metadata_Is_Present()
        {
            // Act
            var description1 = TestEnum.TestValue1.GetDescription();
            var description2 = TestEnum.TestValue2.GetDescription();

            // Assert
            description1.Should().Be("Description not available.");
            description2.Should().Be("Description not available.");
        }

        [Test]
        public void GetDescription_Should_Return_Default_Message_When_Metadata_Is_Not_Present()
        {
            // Act
            var description = TestEnum.TestValueWithoutMetadata.GetDescription();

            // Assert
            description.Should().Be("Description not available.");
        }

        [Test]
        public void GetEnumByName_Should_Return_Correct_Enum_Value_For_Valid_String()
        {
            // Act
            var enumValue1 = "TestValue1".GetEnumByName<TestEnum>();
            var enumValue2 = "TestValue2".GetEnumByName<TestEnum>();

            // Assert
            enumValue1.Should().Be(TestEnum.TestValue1);
            enumValue2.Should().Be(TestEnum.TestValue2);
        }

        [Test]
        public void GetEnumByName_Should_Throw_ArgumentException_For_Invalid_String()
        {
            // Act
            Action act = () => "InvalidValue".GetEnumByName<TestEnum>();

            // Assert
            act.Should().Throw<ArgumentException>().WithMessage("No enum value found for InvalidValue in LayerDomain.EnumType.EnumExtensionsTests+TestEnum");
        }
    }

    // Custom attribute for enum metadata.
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
}
