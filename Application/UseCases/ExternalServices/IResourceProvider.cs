using Application.Result;
using Domain.Entities;

namespace Application.UseCases.ExternalServices
{
    public interface IResourceProvider
    {
        Task<OperationResult<ResourceEntry>> GetMessage(string key);
        Task<OperationResult<IQueryable<ResourceEntry>>> GetResourceEntries();
    }
}
