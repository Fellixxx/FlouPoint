namespace Persistence.CreateStruture.Constants.ColumnType
{
    using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

    /// <summary>
    /// Provides the column types specific to PostgreSQL.
    /// </summary>
    public class ColumnTypesPosgresql : IColumnTypes
    {
        /// <summary>
        /// Represents a boolean type in PostgreSQL.
        /// </summary>
        public string TypeBool => "boolean";
        /// <summary>
        /// Represents a timestamp with time zone type in PostgreSQL.
        /// </summary>
        public string TypeTime => "TIMESTAMPTZ";
        /// <summary>
        /// Represents a variable character type with a maximum length of 100 in PostgreSQL.
        /// </summary>
        public string TypeVar => "character varying(100)";
        /// <summary>
        /// Represents a variable character type with a maximum length of 50 in PostgreSQL.
        /// </summary>
        public string TypeVar50 => "character varying(50)";
        /// <summary>
        /// Represents a variable character type with a maximum length of 64 in PostgreSQL.
        /// </summary>
        public string TypeVar64 => "character char(64)";
        /// <summary>
        /// Represents a VARCHAR column in MySQL with a max length of 50.
        /// </summary>
        public string TypeBlob => "Blob";
        /// <summary>
        /// Represents an integer type in PostgreSQL.
        /// </summary>
        public string Integer => "integer";
        /// <summary>
        /// Represents an Integer column in MySQL.
        /// </summary>
        public string Long => "bigint unsigned DEFAULT NULL";
        /// <summary>
        /// Represents the Npgsql value generation strategy key.
        /// </summary>
        public string Strategy => "Npgsql:ValueGenerationStrategy";
        /// <summary>
        /// Represents the Npgsql identity column value generation strategy.
        /// </summary>
        public object SqlStrategy => NpgsqlValueGenerationStrategy.IdentityByDefaultColumn;
        /// <summary>
        /// Represents the encoding name specific key for PostgreSQL.
        /// </summary>
        public string Name => "PostgreSQL:Encoding";
        /// <summary>
        /// Represents the UTF8 encoding value for PostgreSQL.
        /// </summary>
        public object Value => "UTF8";
    }
}