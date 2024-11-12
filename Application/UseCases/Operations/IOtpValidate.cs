namespace Application.UseCases.Operations
{
    using Application.Result;

    /// <summary>
    /// Defines the contract for validating a One-Time Password (OTP) against a stored OTP for a given email.
    /// </summary>
    public interface IOtpValidate
    {
        /// <summary>
        /// Validates the provided OTP against the stored OTP for the specified email address.
        /// </summary>
        /// <param name="email">The email address for which the OTP was generated.</param>
        /// <param name="otp">The OTP to validate.</param>
        /// <returns>An <see cref="Operation{T}"/> containing a boolean that indicates whether the OTP validation was successful.</returns>
        Task<Operation<bool>> ValidateOtp(string email, string otp);
    }
}
