using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayerDomain.EnumType.OperationExecute
{
    using global::Domain.EnumType.OperationExecute;
    using FluentAssertions;
    using NUnit.Framework;

    [TestFixture]
    internal class OperationExecuteTests
    {
        [Test]
        public void When_AddOperation_Then_NameAndDescriptionShouldMatch()
        {
            // Given
            var operation = OperationExecute.Add;

            // Then
            operation.Should().NotBeNull();
            operation.Name.Should().Be("Add");
            operation.Description.Should().Be("Add a new record.");
        }

        [Test]
        public void When_ModifiedOperation_Then_NameAndDescriptionShouldMatch()
        {
            // Given
            var operation = OperationExecute.Modified;

            // Then
            operation.Should().NotBeNull();
            operation.Name.Should().Be("Modified");
            operation.Description.Should().Be("Modify an existing record.");
        }

        [Test]
        public void When_RemoveOperation_Then_NameAndDescriptionShouldMatch()
        {
            // Given
            var operation = OperationExecute.Remove;

            // Then
            operation.Should().NotBeNull();
            operation.Name.Should().Be("Remove");
            operation.Description.Should().Be("Remove an existing record.");
        }

        [Test]
        public void When_DeactivateOperation_Then_NameAndDescriptionShouldMatch()
        {
            // Given
            var operation = OperationExecute.Deactivate;

            // Then
            operation.Should().NotBeNull();
            operation.Name.Should().Be("Deactivate");
            operation.Description.Should().Be("Deactivate an existing record.");
        }

        [Test]
        public void When_ActivateOperation_Then_NameAndDescriptionShouldMatch()
        {
            // Given
            var operation = OperationExecute.Activate;

            // Then
            operation.Should().NotBeNull();
            operation.Name.Should().Be("Activate");
            operation.Description.Should().Be("Activate a deactivated record.");
        }

        [Test]
        public void When_GetUserByIdOperation_Then_NameAndDescriptionShouldMatch()
        {
            // Given
            var operation = OperationExecute.GetUserById;

            // Then
            operation.Should().NotBeNull();
            operation.Name.Should().Be("GetUserById");
            operation.Description.Should().Be("Retrieve a user by their ID.");
        }

        [Test]
        public void When_GetAllByFilterOperation_Then_NameAndDescriptionShouldMatch()
        {
            // Given
            var operation = OperationExecute.GetAllByFilter;

            // Then
            operation.Should().NotBeNull();
            operation.Name.Should().Be("GetAllByFilter");
            operation.Description.Should().Be("Retrieve all records that match a given filter.");
        }

        [Test]
        public void When_GetPageByFilterOperation_Then_NameAndDescriptionShouldMatch()
        {
            // Given
            var operation = OperationExecute.GetPageByFilter;

            // Then
            operation.Should().NotBeNull();
            operation.Name.Should().Be("GetPageByFilter");
            operation.Description.Should().Be("Retrieve a page of records that match a given filter.");
        }

        [Test]
        public void When_GetCountFilterOperation_Then_NameAndDescriptionShouldMatch()
        {
            // Given
            var operation = OperationExecute.GetCountFilter;

            // Then
            operation.Should().NotBeNull();
            operation.Name.Should().Be("GetCountFilter");
            operation.Description.Should().Be("Get the count of records that match a given filter.");
        }

        [Test]
        public void When_CreateCustomOperation_Then_NameAndDescriptionShouldMatch()
        {
            // Given
            var operation = OperationExecute.CreateCustomOperation("CustomOp", "This is a custom operation");

            // Then
            operation.Should().NotBeNull();
            operation.Name.Should().Be("CustomOp");
            operation.Description.Should().Be("This is a custom operation");
        }

        [Test]
        public void When_GetNameForPredefinedOperation_Then_NameShouldMatch()
        {
            // Given
            var operation = OperationExecute.Add;

            // When
            var name = OperationExecute.GetName(operation);

            // Then
            name.Should().Be("Add");
        }

        [Test]
        public void When_NullOperationPassedToGetName_Then_ArgumentNullExceptionShouldBeThrown()
        {
            // Given
            OperationExecute? operation = null;

            // When
            Action act = () => OperationExecute.GetName(operation);

            // Then
            act.Should().Throw<ArgumentNullException>();
        }
    }
}
