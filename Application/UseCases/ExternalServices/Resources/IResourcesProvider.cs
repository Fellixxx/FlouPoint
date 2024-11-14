namespace Application.UseCases.ExternalServices.Resources
{
    using Application.Constants;
    using Application.Result;
    using Domain.Entities;

    public interface IResourcesProvider
    {
        Task<Operation<Resource>> GetMessage(string key);
        Task<Operation<IQueryable<Resource>>> GetResourceEntries();
        Task<string> GetMessageValueOrDefault(string key, string defaultValue = Messages.ResorcesProvider.ResourceNotFound);
    }
}
