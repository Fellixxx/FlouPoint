namespace Application.UseCases.CRUD.Query.Resource
{
    using Application.Result;
    public interface IResourceReadFilterCount
    {

        /// <summary>
        /// Read the count of the items of by filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>The result of the operation.</returns>
        Task<Operation<int>> ReadFilterCount(string filter);
    }
}
