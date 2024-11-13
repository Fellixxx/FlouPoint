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
    /// <typeparam name = "T">The type of the entity to be managed. Must implement IEntity.</typeparam>
    public abstract class Repository<T> : Read<T>, IRepository<T> where T : class, IEntity
    {
        // Private fields to hold the DbContext and DbSet for entity operations.
        private new readonly DbContext _context;
        private new readonly DbSet<T> _dbSet;
        /// <summary>
        /// Initializes a new instance of the <see cref = "Repository{T}"/> class.
        /// </summary>
        /// <param name = "context">The DbContext to be used for operations. Ensures database interaction is possible.</param>
        public Repository(DbContext context) : base(context)
        {
            // Validate and initialize the context and the set of entities.
            _context = RepositoryHelper.ValidateArgument(context);
            _dbSet = context.Set<T>();
        }

        /// <summary>
        /// Adds a new entity of type T to the database.
        /// </summary>
        /// <param name = "entity">The entity to be added.</param>
        /// <returns>A task with the ID of the added entity. Assumes that the ID is a string.</returns>
        public async Task<string> Create(T? entity)
        {
            // Validate the entity and add it to the DbSet.
            entity = RepositoryHelper.ValidateArgument(entity);
            _dbSet.Add(entity);
            // Save changes and return the ID of the newly added entity.
            await SaveChangesAsync();
            return entity.Id;
        }

        /// <summary>
        /// Updates an existing entity of type T in the database.
        /// </summary>
        /// <param name = "entity">The entity with updated information.</param>
        /// <returns>A task indicating the success of the update operation. Returns true if one record was successfully updated.</returns>
        public async Task<bool> Update(T entity)
        {
            // Validate the entity and mark it as modified.
            RepositoryHelper.ValidateArgument(entity);
            _context.Entry(entity).State = EntityState.Modified;
            // Save changes and check if exactly one record was modified.
            return await SaveChangesAsync() == 1;
        }

        /// <summary>
        /// Deletes an entity of type T from the database.
        /// </summary>
        /// <param name = "entity">The entity to be deleted.</param>
        /// <returns>A task indicating the success of the delete operation. Returns true if one or more records were deleted.</returns>
        public async Task<bool> Delete(T entity)
        {
            // Validate the entity and remove it from the DbSet.
            RepositoryHelper.ValidateArgument(entity);
            _dbSet.Remove(entity);
            // Save changes and check if one or more records were affected.
            return await SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Saves changes made in the DbContext to the database.
        /// </summary>
        /// <returns>A task indicating the number of state entries written to the database.</returns>
        private async Task<int> SaveChangesAsync()
        {
            // Executes the SaveChangesAsync method on the context.
            return await _context.SaveChangesAsync();
        }
    }
}