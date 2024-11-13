namespace Persistence.BaseDbContext
{
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Persistence.CreateStruture.Constants;
    using Persistence.CreateStruture.Constants.ColumnType;

    public static class DataContextHelpers
    {
        /// <summary>
        /// Configures properties and relationships for the ResourceEntry entity.
        /// </summary>
        /// <param name="modelBuilder">The model builder instance used for entity configuration.</param>
        /// <param name="columnTypes">Column type definitions to be applied to the entity properties.</param>
        public static void SetTableResourceEntries(ModelBuilder modelBuilder, IColumnTypes columnTypes)
        {
            modelBuilder.Entity<Resource>().ToTable(DatabaseNames.TableNameResourceEntries);
            modelBuilder.Entity<Resource>(entity =>
            {
                entity.Property(u => u.Id).HasColumnType(columnTypes.TypeVar);
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Name).HasColumnType(columnTypes.TypeVar50).IsRequired();
                entity.Property(u => u.Value).HasColumnType(columnTypes.TypeVar).IsRequired();
                entity.Property(u => u.Comment).HasColumnType(columnTypes.TypeVar).IsRequired();
                entity.Property(u => u.Active).HasColumnType(columnTypes.TypeBool).IsRequired();
            });
        }
    }
}