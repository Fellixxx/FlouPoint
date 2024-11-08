namespace Persistence.Repositories.Interface
{
    using System.Linq.Expressions;
    using System.Threading.Tasks; // Assuming that the 'Task' type is from System.Threading.Tasks namespace

    // Interface for generic repository operations.
    // This interface defines common methods for interacting with the data in the repository.
    public interface IRead<T> where T : class
    {
        /// <summary>
        /// Returns a subset of entities of type T based on the provided predicate.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a sequence of elements satisfying the condition.</returns>
        Task<IQueryable<T>> ReadFilter(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Returns the count of entities of type T based on the provided predicate.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the count of entities satisfying the condition.</returns>
        Task<int> ReadCountFilter(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Returns a subset of entities of type T based on the provided predicate and pagination parameters.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="pageNumber">The page number starting from 1.</param>
        /// <param name="pageSize">The size of the page.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a sequence of elements satisfying the condition and limited by pagination parameters.</returns>
        Task<IQueryable<T>> ReadPageByFilter(Expression<Func<T, bool>> predicate, int pageNumber, int pageSize);
    }
}
