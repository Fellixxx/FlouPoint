using Application.Result;
using Domain.Entities;

namespace Application.UseCases.ExternalServices
{
    public interface IResourceProvider
    {
        Task<OperationResult<ResourceEntry>> GetMessage(string key);
        Task<OperationResult<IQueryable<ResourceEntry>>> GetResourceEntries();
        Task<string> GetMessageValueOrDefault(string key, string defaultValue = "Resource not found");
    }
}
