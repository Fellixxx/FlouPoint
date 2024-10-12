namespace FlouPoint.Application.Test.Core.DTOs
{
    using FluentAssertions;
    using NUnit.Framework;
    using System.Threading.Tasks;
    using global::Application.Result.Error;

    [TestFixture]
    public class ErrorTypesTests
    {
        [Test]
        public Task When_ErrorTypesValuesChecked_Then_ValuesAsExpected()
        {
            // Given & When & Then
            ErrorTypes.None.Should().Be(0);
            ErrorTypes.BusinessValidationError.Should().Be(ErrorTypes.BusinessValidationError);
            ErrorTypes.DatabaseError.Should().Be(ErrorTypes.DatabaseError);
            ErrorTypes.ExternalServicesError.Should().Be(ErrorTypes.ExternalServicesError);
            ErrorTypes.UnexpectedError.Should().Be(ErrorTypes.UnexpectedError);
            ErrorTypes.DataSubmittedInvalid.Should().Be(ErrorTypes.DataSubmittedInvalid);
            ErrorTypes.ConfigurationMissingError.Should().Be(ErrorTypes.ConfigurationMissingError);
            ErrorTypes.NetworkError.Should().Be(ErrorTypes.NetworkError);
            ErrorTypes.UserInputError.Should().Be(ErrorTypes.UserInputError);
            ErrorTypes.NotFoundError.Should().Be(ErrorTypes.NotFoundError);
            ErrorTypes.AuthenticationError.Should().Be(ErrorTypes.AuthenticationError);
            ErrorTypes.AuthorizationError.Should().Be(ErrorTypes.AuthorizationError);
            ErrorTypes.ResourceError.Should().Be(ErrorTypes.ResourceError);
            ErrorTypes.TimeoutError.Should().Be(ErrorTypes.TimeoutError);

            return Task.CompletedTask;
        }
    }
}
