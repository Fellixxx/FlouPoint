namespace Persistence.Repositories
{
    using Persistence.Repositories.Interface;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    /// <summary>
    /// Abstract base class for a generic repository.
    /// Provides CRUD operations on a database using Microsoft's Entity Framework Core.
    /// </summary>
    /// <typeparam name="T">The type of the entity to be managed. Must implement IEntity.</typeparam>
    public abstract class Read<T> : IRead<T> where T : class
    {
        // Instances for database operations
        protected readonly DbContext _context;
        protected readonly DbSet<T> _dbSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{T}"/> class.
        /// </summary>
        /// <param name="context">The DbContext to be used for operations.</param>
        public Read(DbContext context)
        {
            _context = RepositoryHelper.ValidateArgument(context);
            _dbSet = context.Set<T>();
        }

        /// <summary>
        /// Retrieves all entities of type T that match the given predicate.
        /// </summary>
        /// <param name="predicate">A filter expression.</param>
        /// <returns>A task with the matching entities.</returns>
        public Task<IQueryable<T>> ReadFilter(Expression<Func<T, bool>> predicate)
        {
            RepositoryHelper.ValidateArgument(predicate);
            return Task.FromResult(_dbSet.Where(predicate));
        }

        /// <summary>
        /// Counts the entities of type T that match the given predicate.
        /// </summary>
        /// <param name="predicate">A filter expression.</param>
        /// <returns>A task with the count of matching entities.</returns>
        public Task<int> ReadCountFilter(Expression<Func<T, bool>> predicate)
        {
            RepositoryHelper.ValidateArgument(predicate);
            return Task.FromResult(_dbSet.Where(predicate).Count());
        }

        /// <summary>
        /// Retrieves paginated entities of type T that match the given predicate.
        /// </summary>
        /// <param name="predicate">A filter expression.</param>
        /// <param name="pageNumber">Zero-based page index.</param>
        /// <param name="pageSize">Number of items per page.</param>
        /// <returns>A task with the matching entities for the specified page.</returns>
        public Task<IQueryable<T>> ReadPageByFilter(Expression<Func<T, bool>> predicate, int pageNumber, int pageSize)
        {
            RepositoryHelper.ValidateArgument(predicate);
            int skip = pageNumber * pageSize;
            return Task.FromResult(_dbSet.Where(predicate)
                              .Skip(skip)
                              .Take(pageSize));
        }
    }
}
