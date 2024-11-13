using Application.Constants;
using Application.Result;
using Domain.Entities;

namespace Application.UseCases.ExternalServices.Resources
{
    public interface IResourcesProvider
    {
        Task<Operation<Resource>> GetMessage(string key);
        Task<Operation<IQueryable<Resource>>> GetResourceEntries();
        Task<string> GetMessageValueOrDefault(string key, string defaultValue = Messages.IResorcesProvider.ResourceNotFound);
    }
}
