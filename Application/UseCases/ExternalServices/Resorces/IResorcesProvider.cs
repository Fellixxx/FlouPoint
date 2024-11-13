using Application.Result;
using Domain.Entities;

namespace Application.UseCases.ExternalServices.Resorces
{
    public interface IResorcesProvider
    {
        Task<Operation<Resource>> GetMessage(string key);
        Task<Operation<IQueryable<Resource>>> GetResourceEntries();
        Task<string> GetMessageValueOrDefault(string key, string defaultValue = "Resource not found");
    }
}
