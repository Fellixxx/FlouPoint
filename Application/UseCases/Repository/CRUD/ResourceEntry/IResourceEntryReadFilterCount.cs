using Application.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Repository.CRUD.ResourceEntry
{
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
