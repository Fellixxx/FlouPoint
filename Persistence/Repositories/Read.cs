namespace Persistence.Repositories
{
    using Persistence.Repositories.Interface;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    /// <summary>
    /// Abstract base class for a generic repository.
    /// Provides read operations on a database using Microsoft's Entity Framework Core.
    /// </summary>
    /// <typeparam name = "T">The type of the entity to be managed. Must be a class.</typeparam>
    public abstract class Read<T> : IRead<T> where T : class
    {
        // Instance of DbContext to interact with the database
        protected readonly DbContext _context;
        // DbSet representing the collection of entities in the context for the specific type T
        protected readonly DbSet<T> _dbSet;
        /// <summary>
        /// Initializes a new instance of the <see cref = "Read{T}"/> class.
        /// </summary>
        /// <param name = "context">The DbContext to be used for operations. It must not be null.</param>
        public Read(DbContext context)
        {
            // Validate and assign the DbContext to the class fields
            _context = RepositoryHelper.ValidateArgument(context);
            _dbSet = context.Set<T>();
        }

        /// <summary>
        /// Retrieves all entities of type T that match the given predicate.
        /// </summary>
        /// <param name = "predicate">A filter expression to match entities.</param>
        /// <returns>A task representing the asynchronous operation, with an <see cref = "IQueryable{T}"/> result containing the matching entities.</returns>
        public Task<IQueryable<T>> ReadFilter(Expression<Func<T, bool>> predicate)
        {
            // Validate the predicate and return the filtered entities as a queryable task
            RepositoryHelper.ValidateArgument(predicate);
            return Task.FromResult(_dbSet.Where(predicate));
        }

        /// <summary>
        /// Counts the entities of type T that match the given predicate.
        /// </summary>
        /// <param name = "predicate">A filter expression to count matching entities.</param>
        /// <returns>A task representing the asynchronous operation, with an integer result containing the count of matching entities.</returns>
        public Task<int> ReadCountFilter(Expression<Func<T, bool>> predicate)
        {
            // Validate the predicate and return the count of matching entities as a task
            RepositoryHelper.ValidateArgument(predicate);
            return Task.FromResult(_dbSet.Where(predicate).Count());
        }

        /// <summary>
        /// Retrieves a specific page of entities of type T that match the given predicate.
        /// </summary>
        /// <param name = "predicate">A filter expression to match entities.</param>
        /// <param name = "pageNumber">Zero-based page index indicating which page of results to return.</param>
        /// <param name = "pageSize">The number of items to include in each page.</param>
        /// <returns>A task representing the asynchronous operation, with an <see cref = "IQueryable{T}"/> result containing the entities on the specified page.</returns>
        public Task<IQueryable<T>> ReadPageByFilter(Expression<Func<T, bool>> predicate, int pageNumber, int pageSize)
        {
            // Validate the predicate and calculate the number of items to skip
            RepositoryHelper.ValidateArgument(predicate);
            int skip = pageNumber * pageSize;
            // Return the specified page of filtered entities as a queryable task
            return Task.FromResult(_dbSet.Where(predicate).Skip(skip).Take(pageSize));
        }
    }
}