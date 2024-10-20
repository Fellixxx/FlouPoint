using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Persistence.BaseDbContext.Interface
{
    /// <summary>
    /// Interface for the CommonDbContext class, representing a common database context coordinating Entity Framework functionality across projects.
    /// </summary>
    public interface ICommonDbContext
    {
        /// <summary>
        /// Ensures the database is migrated to the latest version.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Provides access to database-related information and operations.
        /// </summary>
        DatabaseFacade Database { get; }

        /// <summary>
        /// Gets the change tracker.
        /// </summary>
        ChangeTracker ChangeTracker { get; }

        /// <summary>
        /// Gets a DbSet for the specified entity type.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>A set for the specified entity type.</returns>
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        /// <summary>
        /// Saves all changes made in this context to the database.
        /// </summary>
        /// <returns>The number of state entries written to the database.</returns>
        int SaveChanges();

        /// <summary>
        /// Asynchronously saves all changes made in this context to the database.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>A task that represents the asynchronous save operation. The task result contains the number of state entries written to the database.</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
