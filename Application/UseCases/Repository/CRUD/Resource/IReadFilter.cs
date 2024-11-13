namespace Application.UseCases.Repository.CRUD.Resource
{
    using Application.Result;
    using System.Linq.Expressions;
    using Resource = Domain.Entities.Resource;
    public interface IReadFilter
    {
        /// <summary>
        /// Returns all ResourceEntry entities that satisfy the specified predicate.
        /// Takes a lambda expression representing the condition that the entities should meet.
        /// </summary>
        /// <param name="predicate">The predicate used to filter the entities.</param>
        /// <returns>An <see cref="Operation{T}"/> containing an IQueryable of ResourceEntry entities that meet the specified condition.</returns>
        Task<Operation<IQueryable<Resource>>> ReadFilter(Expression<Func<Resource, bool>> predicate);
    }
}
