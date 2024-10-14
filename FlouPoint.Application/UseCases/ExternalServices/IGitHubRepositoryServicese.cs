using Application.Result;
using FlouPoint.GitHub;

namespace FlouPoint.Application.UseCases.ExternalServices
{
    internal interface IGitHubRepositoryServicese
    {
        Task<OperationResult<bool>> CreateRepositoryAsync(RepositoryInfo repositoryInfo);
    }
}
