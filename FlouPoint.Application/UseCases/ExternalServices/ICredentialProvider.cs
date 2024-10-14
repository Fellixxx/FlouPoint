using FlouPoint.GitHub;

namespace FlouPoint.Application.UseCases.ExternalServices
{
    /// <summary>
    /// Represents a provider that retrieves credentials.
    /// </summary>
    public interface ICredentialProvider
    {
        /// <summary>
        /// Retrieves credentials.
        /// </summary>
        /// <returns>An instance of <see cref="Credential"/> containing the credentials.</returns>
        Credential GetCredentials();
    }
}
