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
        // A protected field to store column type configurations.
        protected readonly IColumnTypes _columnTypes;
        /// <summary>
        /// Represents a collection of <see cref = "Resource"/> entities in the database context.
        /// </summary>
        public virtual DbSet<Resource> Lists { get; set; }
        /// <summary>
        /// Represents a collection of <see cref = "User"/> entities in the database context.
        /// </summary>
        public virtual DbSet<User> Users { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref = "DataContext"/> class with the specified options and column types.
        /// </summary>
        /// <param name = "options">The options to be used by the database context.</param>
        /// <param name = "columnTypes">The column types to be used by the database context.</param>
        public DataContext(DbContextOptions options, IColumnTypes columnTypes) : base(options)
        {
            _columnTypes = columnTypes; // Assigns the column type configurations to the field.
        }

        /// <summary>
        /// Ensures the database is migrated to the latest version.
        /// </summary>
        /// <exception cref = "Exception">Thrown when migration fails.</exception>
        public virtual void Initialize()
        {
            try
            {
                Database.Migrate(); // Applies pending migrations to the database.
            }
            catch (Exception ex)
            {
                // Throws a new exception with a specified message when migration fails.
                throw new Exception(Messages.DataContext.Initialize);
            }
        }

        /// <summary>
        /// Configures the entity model using the Fluent API.
        /// </summary>
        /// <param name = "modelBuilder">The builder used for configuring the entity model.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Calls the base method to apply default configurations.
            // Sets up additional entity configurations specific to the DataContext.
            DataContextHelpers.SetTableResourceEntries(modelBuilder, _columnTypes);
        }
    }
}