namespace Application.UseCases.Repository.CRUD.Query
{
    using Application.Result;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines a contract for reading items based on a filter expression.
    /// </summary>
    /// <typeparam name="T">The type of the entity. Must implement IEntity.</typeparam>
    public interface IReadFilter<T> where T : class
    {
        /// <summary>
        /// Retrieves all items that satisfy the specified predicate.
        /// </summary>
        /// <param name="predicate">A lambda expression representing the filter criteria.</param>
        /// <returns>An OperationResult containing an IQueryable of items that satisfy the specified predicate.</returns>
        Task<Operation<IQueryable<T>>> ReadFilter(Expression<Func<T, bool>> predicate);
    }
}
