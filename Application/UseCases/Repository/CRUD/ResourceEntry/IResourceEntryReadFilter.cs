using Application.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Repository.CRUD.ResourceEntry
{
    using ResourceEntry = Domain.Entities.ResourceEntry;
    public interface IResourceEntryReadFilter
    {
        /// <summary>
        /// Returns all ResourceEntry entities that satisfy the specified predicate.
        /// Takes a lambda expression representing the condition that the entities should meet.
        /// </summary>
        /// <param name="predicate">The predicate used to filter the entities.</param>
        /// <returns>An <see cref="OperationResult{T}"/> containing an IQueryable of ResourceEntry entities that meet the specified condition.</returns>
        Task<OperationResult<IQueryable<ResourceEntry>>> ReadFilter(Expression<Func<ResourceEntry, bool>> predicate);
    }
}
