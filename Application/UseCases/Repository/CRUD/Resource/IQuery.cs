namespace Application.UseCases.Repository.CRUD.Resource
{
    /// <summary>
    /// The IResourceEntryQuery interface defines the contract for querying ResourceEntry entities.
    /// It extends multiple interfaces to provide filtering, counting, paginated retrieval, 
    /// and ID-based querying functionality for ResourceEntry entities.
    /// </summary>
    public interface IQuery : IReadFilter, IReadFilterCount, IReadFilterPage, IReadId
    {
    }
}
