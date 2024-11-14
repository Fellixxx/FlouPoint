namespace Application.UseCases.Repository.CRUD.Resource
{
    using Application.Result;
    public interface IResourceReadFilterCount
    {
        /// <summary>
        /// Retrieves the count of ResourceEntry entities that match the specified filter.
        /// </summary>
        /// <param name="filter">The filter string to apply to the count operation.</param>
        /// <returns>An <see cref="Operation{T}"/> containing the count of entities that match the filter criteria.</returns>
        Task<Operation<int>> ReadFilterCount(string filter);
    }
}
