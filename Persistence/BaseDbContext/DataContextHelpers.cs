namespace Persistence.BaseDbContext
{
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Persistence.CreateStruture.Constants;
    using Persistence.CreateStruture.Constants.ColumnType;

    /// <summary>
    /// Provides helper methods for configuring the database context, particularly
    /// for setting up tables and entity configurations.
    /// </summary>
    public static class DataContextHelpers
    {
        /// <summary>
        /// Configures properties and relationships for the <see cref = "Resource"/> entity,
        /// mapping it to the corresponding database table and specifying column types.
        /// </summary>
        /// <param name = "modelBuilder">The model builder instance used for entity configuration.</param>
        /// <param name = "columnTypes">Column type definitions to be applied to the entity properties.</param>
        public static void SetTableResourceEntries(ModelBuilder modelBuilder, IColumnTypes columnTypes)
        {
            // Maps the Resource entity to the "ResourceEntries" table.
            modelBuilder.Entity<Resource>().ToTable(DatabaseNames.TableNameResourceEntries);
            modelBuilder.Entity<Resource>(entity =>
            {
                // Configures the 'Id' property with a specified column type and sets it as the primary key.
                entity.Property(u => u.Id).HasColumnType(columnTypes.TypeVar);
                entity.HasKey(u => u.Id);
                // Configures the 'Name' property with a specific column type and flags it as required.
                entity.Property(u => u.Name).HasColumnType(columnTypes.TypeVar50).IsRequired();
                // Configures the 'Value' property with a specific column type and flags it as required.
                entity.Property(u => u.Value).HasColumnType(columnTypes.TypeVar).IsRequired();
                // Configures the 'Comment' property with a specific column type and flags it as required.
                entity.Property(u => u.Comment).HasColumnType(columnTypes.TypeVar).IsRequired();
                // Configures the 'Active' property with a specific column type and flags it as required.
                entity.Property(u => u.Active).HasColumnType(columnTypes.TypeBool).IsRequired();
            });
        }
    }
}