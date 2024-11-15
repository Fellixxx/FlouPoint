using Application.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.CRUD.Query.Resource
{
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
