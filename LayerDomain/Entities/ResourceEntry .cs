using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayerDomain.Entities
{
    using Domain.Entities;
    using FluentAssertions;
    using NUnit.Framework;

    [TestFixture]
    public class ResourceEntryTests
    {
        [Test]
        public void ResourceEntry_Should_Have_Default_Empty_Properties_And_Inactive_Status()
        {
            // Act
            var resourceEntry = new ResourceEntry();

            // Assert
            resourceEntry.Id.Should().BeEmpty();
            resourceEntry.Name.Should().BeEmpty();
            resourceEntry.Value.Should().BeEmpty();
            resourceEntry.Comment.Should().BeEmpty();
            resourceEntry.Active.Should().BeFalse();
        }

        [Test]
        public void ResourceEntry_Should_Store_Id_Property_Correctly()
        {
            // Arrange
            string expectedId = "12345";

            // Act
            var resourceEntry = new ResourceEntry
            {
                Id = expectedId
            };

            // Assert
            resourceEntry.Id.Should().Be(expectedId);
        }

        [Test]
        public void ResourceEntry_Should_Store_Name_Property_Correctly()
        {
            // Arrange
            string expectedName = "SampleResource";

            // Act
            var resourceEntry = new ResourceEntry
            {
                Name = expectedName
            };

            // Assert
            resourceEntry.Name.Should().Be(expectedName);
        }

        [Test]
        public void ResourceEntry_Should_Store_Value_Property_Correctly()
        {
            // Arrange
            string expectedValue = "ResourceValue";

            // Act
            var resourceEntry = new ResourceEntry
            {
                Value = expectedValue
            };

            // Assert
            resourceEntry.Value.Should().Be(expectedValue);
        }

        [Test]
        public void ResourceEntry_Should_Store_Comment_Property_Correctly()
        {
            // Arrange
            string expectedComment = "This is a test comment.";

            // Act
            var resourceEntry = new ResourceEntry
            {
                Comment = expectedComment
            };

            // Assert
            resourceEntry.Comment.Should().Be(expectedComment);
        }

        [Test]
        public void ResourceEntry_Should_Store_Active_Property_Correctly()
        {
            // Arrange
            bool expectedActive = true;

            // Act
            var resourceEntry = new ResourceEntry
            {
                Active = expectedActive
            };

            // Assert
            resourceEntry.Active.Should().BeTrue();
        }
    }
}
