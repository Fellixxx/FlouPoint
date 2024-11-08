namespace Application.UseCases.Repository.CRUD.ResourceEntry
{
    using Application.Result;
    public interface IResourceEntryReadFilterCount
    {
        /// <summary>
        /// Retrieves the count of ResourceEntry entities that match the specified filter.
        /// </summary>
        /// <param name="filter">The filter string to apply to the count operation.</param>
        /// <returns>An <see cref="OperationResult{T}"/> containing the count of entities that match the filter criteria.</returns>
        Task<OperationResult<int>> ReadFilterCount(string filter);
    }
}
