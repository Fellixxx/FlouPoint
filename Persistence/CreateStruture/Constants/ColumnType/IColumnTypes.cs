namespace Persistence.CreateStruture.Constants.ColumnType
{
    /// <summary>
    /// Represents a set of column types to be used in database migrations.
    /// </summary>
    public interface IColumnTypes
    {
        /// <summary>
        /// Gets the type representation for a boolean column.
        /// </summary>
        string TypeBool { get; }

        /// <summary>
        /// Gets the type representation for a time column.
        /// </summary>
        string TypeTime { get; }

        /// <summary>
        /// Gets the type representation for a variable column.
        /// This might typically be used for strings or other variable-length data types.
        /// </summary>
        string TypeVar { get; }

        /// <summary>
        /// Gets the type representation for a variable column with a maximum length of 50.
        /// </summary>
        string TypeVar50 { get; }

        /// <summary>
        /// Gets the type representation for a variable column with a maximum length of 50.
        /// </summary>
        string TypeBlob { get; }

        /// <summary>
        /// Gets the type representation for a variable column with a maximum length of 64.
        /// </summary>
        string TypeVar64 { get; }

        /// <summary>
        /// Gets the type representation for an integer column.
        /// </summary>
        string Integer { get; }

        /// <summary>
        /// Gets the type representation for an integer column.
        /// </summary>
        string Long { get; }

        /// <summary>
        /// Gets the type or strategy name associated with certain column operations or data types.
        /// </summary>
        string Strategy { get; }

        /// <summary>
        /// Gets the SQL-specific strategy object or representation for certain column operations or data types.
        /// This can be null if not applicable.
        /// </summary>
        object? SqlStrategy { get; }

        /// <summary>
        /// Gets the name of the column or a related attribute.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets a generic value associated with a column type or operation.
        /// This can be null if not applicable.
        /// </summary>
        object? Value { get; }
    }
}
