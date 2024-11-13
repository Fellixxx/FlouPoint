namespace Infrastructure.Utilities
{
    using System; // This is required for Guid.TryParse method.
    using Application.Result;
    using Infrastructure.Constants;

    /// <summary>
    /// Provides utility functions to validate GUIDs (Globally Unique Identifiers).
    /// This class encapsulates the logic needed to verify if a given string is a valid GUID.
    /// </summary>
    public class GuidValidator
    {
        /// <summary>
        /// Validates if the provided string is a valid GUID.
        /// </summary>
        /// <param name = "id">The string representation of a GUID to validate.</param>
        /// <returns>
        /// An <see cref = "Operation{T}"/> which indicates the result of the validation.
        /// If the validation is successful, it contains the valid GUID as a string.
        /// If the validation fails, it contains an error message.
        /// </returns>
        public static Operation<string> HasGuid(string id)
        {
            // Attempt to parse the provided string to check if it's a valid GUID.
            bool resultConversion = Guid.TryParse(id, out _);
            // If the parsing fails, meaning the string is not a valid GUID
            if (!resultConversion)
            {
                // Create a new BusinessStrategy object, which might handle other business logic or configuration.
                var business = new BusinessStrategy<string>();
                // Retrieve a predefined error message for an invalid GUID.
                var invalidGuid = Message.GuidValidator.InvalidGuid;
                // Return a failure operation result including the error message and business strategy.
                // Note: The error message is expected to relate to GUID validation failure.
                return OperationStrategy<string>.Fail(invalidGuid, business);
            }

            // If the string is successfully parsed as a valid GUID,
            // Return a success operation result containing the original GUID string
            // and a success message indicating that the GUID validation is successful.
            return Operation<string>.Success(id, Message.GuidValidator.Success);
        }
    }
}