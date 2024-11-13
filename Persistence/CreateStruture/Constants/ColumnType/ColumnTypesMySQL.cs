namespace Persistence.CreateStruture.Constants.ColumnType
{
    using MySql.EntityFrameworkCore.Metadata;

    /// <summary>
    /// Provides MySQL specific column types to be used during Entity Framework Core migrations.
    /// Implements the IColumnTypes interface to ensure a consistent set of column type properties.
    /// </summary>
    public class ColumnTypesMySQL : IColumnTypes
    {
        /// <summary>
        /// Represents a Boolean column type in MySQL.
        /// Uses TINYINT(1) where typically 1 is true and 0 is false.
        /// </summary>
        public string TypeBool => "TINYINT(1)";
        /// <summary>
        /// Represents a Date-Time column type in MySQL.
        /// Generally uses DATETIME, but TIMESTAMP can be considered based on specific needs and time zone requirements.
        /// </summary>
        public string TypeTime => "DATETIME";
        /// <summary>
        /// Represents a variable-length string column type with a maximum length of 100 characters.
        /// Commonly used for columns storing short strings or text.
        /// </summary>
        public string TypeVar => "VARCHAR(100)";
        /// <summary>
        /// Represents a VARCHAR column type in MySQL with a maximum length of 50 characters.
        /// Useful for storing small to medium-sized text fields.
        /// </summary>
        public string TypeVar50 => "VARCHAR(50)";
        /// <summary>
        /// Represents a fixed-length character column type with a length of 64 characters.
        /// Frequently used for storing fixed-size hashes or code values.
        /// </summary>
        public string TypeVar64 => "char(64)";
        /// <summary>
        /// Represents a BLOB (Binary Large Object) column type in MySQL.
        /// Suitable for storing binary data such as images, multimedia, or large text blocks.
        /// </summary>
        public string TypeBlob => "Blob";
        /// <summary>
        /// Represents an Integer column type in MySQL.
        /// Utilized for storing whole numbers, both positive and negative.
        /// </summary>
        public string Integer => "INT";
        /// <summary>
        /// Represents an unsigned BIGINT column type in MySQL.
        /// Used for storing large integers typically larger than those stored in an INT.
        /// </summary>
        public string Long => "bigint unsigned";
        /// <summary>
        /// Represents a string identifier for the strategy used for value generation in MySQL.
        /// </summary>
        public string Strategy => "MySql:ValueGenerationStrategy";
        /// <summary>
        /// Provides the specific MySQL value generation strategy.
        /// Typically used for defining columns with an auto-incrementing identity.
        /// </summary>
        public object? SqlStrategy => MySQLValueGenerationStrategy.IdentityColumn;
        /// <summary>
        /// Name of the property used to specify the character set for a MySQL column.
        /// </summary>
        public string Name => "MySQL:Charset";
        /// <summary>
        /// The character set value assigned to a MySQL column.
        /// 'utf8mb4' is preferred for full Unicode support, including special characters and emojis.
        /// </summary>
        public object? Value => "utf8mb4";
    }
}