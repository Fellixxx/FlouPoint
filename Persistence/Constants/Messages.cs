namespace Persistence.Constants
{
    /// <summary>
    /// This class holds constant message strings used throughout the persistence layer.
    /// </summary>
    public static class Messages
    {
        /// <summary>
        /// Messages related to operations within the data context, 
        /// such as database initialization and migration errors.
        /// </summary>
        public static class DataContext
        {
            /// <summary>
            /// Message indicating an error that occurred during the database migration or initialization process.
            /// </summary>
            public const string Initialize = "An error occurred while migrating or initializing the database.";
        }
    }
}