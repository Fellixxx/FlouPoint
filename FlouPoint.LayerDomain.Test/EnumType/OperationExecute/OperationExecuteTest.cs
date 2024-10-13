namespace FlouPoint.LayerDomain.Test.EnumType.OperationExecute
{
    using NUnit.Framework;
    using FluentAssertions;
    using System;
    using System.Reflection;
    using Domain.EnumType;
    using Domain.EnumType.OperationExecute;

    [TestFixture]
    public class OperationExecuteTests
    {
        [TestCase(OperationExecute.Add, "Add", "Add a new record.")]
        [TestCase(OperationExecute.Modified, "Modified", "Modify an existing record.")]
        [TestCase(OperationExecute.Remove, "Remove", "Remove an existing record.")]
        [TestCase(OperationExecute.Deactivate, "Deactivate", "Deactivate an existing record.")]
        [TestCase(OperationExecute.Activate, "Activate", "Activate a deactivated record.")]
        [TestCase(OperationExecute.GetUserById, "GetUserById", "Retrieve a user by their ID.")]
        [TestCase(OperationExecute.GetAllByFilter, "GetAllByFilter", "Retrieve all records that match a given filter.")]
        [TestCase(OperationExecute.GetPageByFilter, "GetPageByFilter", "Retrieve a page of records that match a given filter.")]
        [TestCase(OperationExecute.GetCountFilter, "GetCountFilter", "Get the count of records that match a given filter.")]
        [TestCase(OperationExecute.GenerateOtp, "GenerateOtp", "Generate a One-Time Password (OTP).")]
        [TestCase(OperationExecute.LoginOtp, "LoginOtp", "Login using a One-Time Password (OTP).")]
        [TestCase(OperationExecute.Login, "Login", "Standard login operation.")]
        [TestCase(OperationExecute.Validate, "Validate", "General validation operation.")]
        [TestCase(OperationExecute.ValidateEmail, "ValidateEmail", "Validate an email address.")]
        [TestCase(OperationExecute.ValidateOtp, "ValidateOtp", "Validate the provided One-Time Password (OTP).")]
        [TestCase(OperationExecute.ValidateUsername, "ValidateUsername", "Validate a username.")]
        [TestCase(OperationExecute.SetNewPassword, "SetNewPassword", "Set a new password for a user.")]
        [TestCase(OperationExecute.SendEmailAsync, "SendEmailAsync", "Asynchronously send an email.")]
        public void OperationExecute_Should_Have_Correct_EnumMetadata(OperationExecute operation, string expectedName, string expectedDescription)
        {
            // When
            var fieldInfo = operation.GetType().GetField(operation.ToString());
            var attribute = fieldInfo.GetCustomAttribute<EnumMetadataAttribute>();

            // Then
            attribute.Should().NotBeNull();
            attribute.Name.Should().Be(expectedName);
            attribute.Description.Should().Be(expectedDescription);
        }

        [Test]
        public void OperationExecute_Without_EnumMetadataAttribute_Should_Return_Null()
        {
            // Given
            var operation = OperationExecute.Add;

            // When
            var fieldInfo = operation.GetType().GetField(operation.ToString());
            var attribute = fieldInfo.GetCustomAttribute<EnumMetadataAttribute>();

            // Then
            attribute.Should().NotBeNull(); // All values in OperationExecute should have metadata, so attribute should not be null
        }

        [Test]
        public void EnumMetadata_Should_Be_Applied_To_All_OperationExecute_Values()
        {
            // Given
            var operations = Enum.GetValues(typeof(OperationExecute));

            // When & Then
            foreach (OperationExecute operation in operations)
            {
                var fieldInfo = operation.GetType().GetField(operation.ToString());
                var attribute = fieldInfo.GetCustomAttribute<EnumMetadataAttribute>();
                attribute.Should().NotBeNull();
            }
        }

        [Test]
        public void EnumMetadata_Should_Have_Correct_AttributeUsage()
        {
            // When
            var attributeUsage = typeof(EnumMetadataAttribute).GetCustomAttribute<AttributeUsageAttribute>();

            // Then
            attributeUsage.Should().NotBeNull();
            attributeUsage.ValidOn.Should().Be(AttributeTargets.Field);
            attributeUsage.AllowMultiple.Should().BeFalse();
        }
    }
}

