using Application.Result;
using Domain.Entities;

namespace Application.UseCases.ExternalServices
{
    public interface IResourceProvider
    {
        Task<Operation<ResourceEntry>> GetMessage(string key);
        Task<Operation<IQueryable<ResourceEntry>>> GetResourceEntries();
        Task<string> GetMessageValueOrDefault(string key, string defaultValue = "Resource not found");
    }
}
