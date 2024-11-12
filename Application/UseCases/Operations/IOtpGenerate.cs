namespace Application.UseCases.Operations
{
    using Application.Result;

    /// <summary>
    /// Defines the contract for generating a One-Time Password (OTP) and sending it via email.
    /// </summary>
    public interface IOtpGenerate
    {
        /// <summary>
        /// Asynchronously generates a One-Time Password (OTP) and sends it to the specified email address.
        /// </summary>
        /// <param name="email">The email address to which the OTP should be sent.</param>
        /// <returns>An <see cref="Operation{T}"/> containing a boolean that indicates whether the OTP generation and sending was successful.</returns>
        Task<Operation<bool>> GenerateOtp(string email);
    }
}
