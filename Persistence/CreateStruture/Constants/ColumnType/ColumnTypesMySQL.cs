namespace Persistence.CreateStruture.Constants.ColumnType
{
    using MySql.EntityFrameworkCore.Metadata;

    /// <summary>
    /// Provides MySQL specific column types to be used during migrations.
    /// Implements the IColumnTypes interface to guarantee a consistent set of properties.
    /// </summary>
    public class ColumnTypesMySQL : IColumnTypes
    {
        /// <summary>
        /// Represents a Boolean column in MySQL. Uses TINYINT(1) where 1 is true and 0 is false.
        /// </summary>
        public string TypeBool => "TINYINT(1)";

        /// <summary>
        /// Represents a Date-Time column in MySQL. Can also use TIMESTAMP based on specific needs.
        /// </summary>
        public string TypeTime => "DATETIME";

        /// <summary>
        /// Represents a VARCHAR column in MySQL with a max length of 100.
        /// </summary>
        public string TypeVar => "VARCHAR(100)";

        /// <summary>
        /// Represents a VARCHAR column in MySQL with a max length of 50.
        /// </summary>
        public string TypeVar50 => "VARCHAR(50)";

        /// <summary>
        /// Represents a VARCHAR column in MySQL with a max length of 50.
        /// </summary>
        public string TypeVar64 => "char(64)";

        //TypeBlob

        /// <summary>
        /// Represents a VARCHAR column in MySQL with a max length of 50.
        /// </summary>
        public string TypeBlob => "Blob";

        /// <summary>
        /// Represents an Integer column in MySQL.
        /// </summary>
        public string Integer => "INT";

        /// <summary>
        /// Represents an Integer column in MySQL.
        /// </summary>
        public string Long => "bigint unsigned";

        /// <summary>
        /// Represents the strategy for value generation in MySQL.
        /// </summary>
        public string Strategy => "MySql:ValueGenerationStrategy";

        /// <summary>
        /// Provides the specific MySQL value generation strategy. Typically used for auto-incrementing columns.
        /// </summary>
        public object? SqlStrategy => MySQLValueGenerationStrategy.IdentityColumn;

        /// <summary>
        /// Name property to set character set for a MySQL column.
        /// </summary>
        public string Name => "MySQL:Charset";

        /// <summary>
        /// The actual character set value to be used for a MySQL column. utf8mb4 supports a wide range of characters including emojis.
        /// </summary>
        public object? Value => "utf8mb4";
    }
}
