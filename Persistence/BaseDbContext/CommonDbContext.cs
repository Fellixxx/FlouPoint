using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence.BaseDbContext.Interface;
using Persistence.CreateStruture.Constants.ColumnType;


namespace Persistence.BaseDbContext
{
    /// <summary>
    /// Represents a common database context coordinating Entity Framework functionality across projects.
    /// </summary>
    public class CommonDbContext : DbContext, ICommonDbContext
    {
        protected readonly IColumnTypes _columnTypes;
        protected readonly ILogger<CommonDbContext> _logger;

        /// <summary>
        /// Represents a collection of <see cref="ResourceEntry"/> entities in the database context.
        /// </summary>
        public virtual DbSet<ResourceEntry> Lists { get; set; }
        public virtual DbSet<User> Users { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommonDbContext"/> class with the specified options, column types, and logger.
        /// </summary>
        /// <param name="options">The options to be used by the database context.</param>
        /// <param name="columnTypes">The column types to be used by the database context.</param>
        /// <param name="logger">The logger instance.</param>
        public CommonDbContext(DbContextOptions options, IColumnTypes columnTypes, ILogger<CommonDbContext> logger) : base(options)
        {
            _columnTypes = columnTypes;
            _logger = logger;
        }

        /// <summary>
        /// Ensures the database is migrated to the latest version.
        /// </summary>
        public virtual void Initialize()
        {
            try
            {
                Database.Migrate();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while migrating or initializing the database.");
                throw;
            }
        }

        /// <summary>
        /// Configures the entity model using the Fluent API.
        /// </summary>
        /// <param name="modelBuilder">The builder used for configuring the entity model.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            CommonDbContextHelpers.SetTableResourceEntries(modelBuilder, _columnTypes);
        }
    }
}
