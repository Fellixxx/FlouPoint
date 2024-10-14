using Application.Result;

namespace FlouPoint.Application.UseCases.ExternalServices
{
    /// <summary>
    /// Provides an interface for retrieving environment variables.
    /// </summary>
    public interface IEnvironmentVariableProvider
    {
        /// <summary>
        /// Retrieves the value of an environment variable.
        /// </summary>
        /// <param name="key">The name of the environment variable.</param>
        /// <returns>The value of the environment variable, or <c>null</c> if it is not found.</returns>
        Task<OperationResult<string>> GetEnvironmentVariable(string key);
    }
}
