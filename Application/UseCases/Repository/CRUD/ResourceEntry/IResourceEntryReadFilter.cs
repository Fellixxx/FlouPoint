namespace Application.UseCases.Repository.CRUD.ResourceEntry
{
    using Application.Result;
    using System.Linq.Expressions;
    using ResourceEntry = Domain.Entities.ResourceEntry;
    public interface IResourceEntryReadFilter
    {
        /// <summary>
        /// Returns all ResourceEntry entities that satisfy the specified predicate.
        /// Takes a lambda expression representing the condition that the entities should meet.
        /// </summary>
        /// <param name="predicate">The predicate used to filter the entities.</param>
        /// <returns>An <see cref="Operation{T}"/> containing an IQueryable of ResourceEntry entities that meet the specified condition.</returns>
        Task<Operation<IQueryable<ResourceEntry>>> ReadFilter(Expression<Func<ResourceEntry, bool>> predicate);
    }
}
