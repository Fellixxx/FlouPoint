namespace Persistence.BaseDbContext
{
    using Persistence.Constants;
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Persistence.BaseDbContext.Interface;
    using Persistence.CreateStruture.Constants.ColumnType;

    /// <summary>
    /// Represents a common database context coordinating Entity Framework functionality across projects.
    /// </summary>
    public class DataContext : DbContext, IDataContext
    {
        protected readonly IColumnTypes _columnTypes;

        /// <summary>
        /// Represents a collection of <see cref="Resource"/> entities in the database context.
        /// </summary>
        public virtual DbSet<Resource> Lists { get; set; }
        public virtual DbSet<User> Users { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataContext"/> class with the specified options, column types, and logger.
        /// </summary>
        /// <param name="options">The options to be used by the database context.</param>
        /// <param name="columnTypes">The column types to be used by the database context.</param>
        /// <param name="logger">The logger instance.</param>
        public DataContext(DbContextOptions options, IColumnTypes columnTypes) : base(options)
        {
            _columnTypes = columnTypes;
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
                throw new Exception(Messages.DataContext.Initialize);
            }
        }

        /// <summary>
        /// Configures the entity model using the Fluent API.
        /// </summary>
        /// <param name="modelBuilder">The builder used for configuring the entity model.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            DataContextHelpers.SetTableResourceEntries(modelBuilder, _columnTypes);
        }
    }
}
