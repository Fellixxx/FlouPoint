namespace Persistence.Repositories
{
    using Domain.Interfaces.Entity;
    using Persistence.Repositories.Interface;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    /// <summary>
    /// Abstract base class for a generic repository.
    /// Provides CRUD operations on a database using Microsoft's Entity Framework Core.
    /// </summary>
    /// <typeparam name="T">The type of the entity to be managed. Must implement IEntity.</typeparam>
    public abstract class Repository<T> : Read<T>, IRepository<T> where T : class, IEntity
    {
        // Instances for database operations
        private new readonly DbContext _context;
        private new readonly DbSet<T> _dbSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{T}"/> class.
        /// </summary>
        /// <param name="context">The DbContext to be used for operations.</param>
        public Repository(DbContext context) : base(context)
        {
            _context = RepositoryHelper.ValidateArgument(context);
            _dbSet = context.Set<T>();
        }

        /// <summary>
        /// Adds a new entity of type T to the database.
        /// </summary>
        /// <param name="entity">The entity to be added.</param>
        /// <returns>A task with the ID of the added entity.</returns>
        public async Task<string> Create(T? entity)
        {
            entity = RepositoryHelper.ValidateArgument(entity);
            _dbSet.Add(entity);
            await SaveChangesAsync();
            return entity.Id;
        }

        /// <summary>
        /// Updates an existing entity of type T in the database.
        /// </summary>
        /// <param name="entity">The entity with updated information.</param>
        /// <returns>A task indicating the success of the update operation.</returns>
        public async Task<bool> Update(T entity)
        {
            RepositoryHelper.ValidateArgument(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return await SaveChangesAsync() == 1;
        }

        /// <summary>
        /// Deletes an entity of type T from the database.
        /// </summary>
        /// <param name="entity">The entity to be deleted.</param>
        /// <returns>A task indicating the success of the delete operation.</returns>
        public async Task<bool> Delete(T entity)
        {
            RepositoryHelper.ValidateArgument(entity);
            _dbSet.Remove(entity);
            return await SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Saves changes made in the DbContext to the database.
        /// </summary>
        /// <returns>A task indicating the number of state entries written to the database.</returns>
        private async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
