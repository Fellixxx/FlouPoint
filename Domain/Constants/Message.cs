namespace Domain.Constants
{
    /// <summary>
    /// Contains message constants used throughout the application.
    /// This class is designed to group messages into categories for better organization and access.
    /// </summary>
    public static class Messages
    {
        /// <summary>
        /// Messages related to enum extension methods.
        /// This includes messages used for handling scenarios where enum values may not directly match expectations or provide metadata.
        /// </summary>
        public static class EnumExtensions
        {
            /// <summary>
            /// Represents an unknown value in an enum.
            /// This is used as a placeholder when an enum value is not recognized.
            /// </summary>
            public const string Unknown = "UNKNOWN";
            /// <summary>
            /// Message displayed when an enum's description is not available.
            /// Useful for logging or displaying information when metadata is incomplete.
            /// </summary>
            public const string DescriptionNotAvailable = "Description not available.";
            /// <summary>
            /// Message template used when an enum value cannot be found in a given context.
            /// Includes placeholders for specifying the expected value and the enum type.
            /// </summary>
            public const string NoEnumValueFound = "No enum value found for {0} in {1}";
        }

        /// <summary>
        /// Messages related to the integrity of enum metadata.
        /// This typically includes validation messages for names and descriptions.
        /// </summary>
        public static class EnumMetadata
        {
            /// <summary>
            /// Message indicating constraints on null, empty, or whitespace values for names or descriptions.
            /// Ensures that important metadata fields are provided meaningful content.
            /// </summary>
            public const string ForNameOrDescription = "For name or description, null, empty, and whitespace are not allowed.";
        }

        /// <summary>
        /// Messages related to actions that can be performed on records.
        /// This class provides standard keys and descriptions for various CRUD operations.
        /// </summary>
        public static class ActionType
        {
            /// <summary>
            /// Message indicating constraints on null, empty, or whitespace values for names or descriptions.
            /// </summary>
            public const string ForNameOrDescription = "For name or description, null, empty, and whitespace are not allowed.";
            /// <summary>
            /// Key representing the action to add a new record.
            /// </summary>
            public const string KeyAdd = "Add";
            /// <summary>
            /// Description for adding a new record.
            /// </summary>
            public const string Add = "Add a new record.";
            /// <summary>
            /// Key representing the action to modify an existing record.
            /// </summary>
            public const string KeyModified = "Modified";
            /// <summary>
            /// Description for modifying an existing record.
            /// </summary>
            public const string Modified = "Modify an existing record.";
            /// <summary>
            /// Key representing the action to remove an existing record.
            /// </summary>
            public const string KeyRemove = "Remove";
            /// <summary>
            /// Description for removing an existing record.
            /// </summary>
            public const string Remove = "Remove an existing record.";
            /// <summary>
            /// Key representing the action to deactivate an existing record.
            /// </summary>
            public const string KeyDeactivate = "Deactivate";
            /// <summary>
            /// Description for deactivating an existing record.
            /// </summary>
            public const string Deactivate = "Deactivate an existing record.";
            /// <summary>
            /// Key representing the action to activate a deactivated record.
            /// </summary>
            public const string KeyActivate = "Activate";
            /// <summary>
            /// Description for activating a deactivated record.
            /// </summary>
            public const string Activate = "Activate a deactivated record.";
            /// <summary>
            /// Key representing the action to retrieve a user by their ID.
            /// </summary>
            public const string KeyGetUserById = "GetUserById";
            /// <summary>
            /// Description for retrieving a user by their ID.
            /// </summary>
            public const string GetUserById = "Retrieve a user by their ID.";
            /// <summary>
            /// Key representing the action to retrieve all records that match a given filter.
            /// </summary>
            public const string KeyGetAllByFilter = "GetAllByFilter";
            /// <summary>
            /// Description for retrieving all records matching a filter.
            /// </summary>
            public const string GetAllByFilter = "Retrieve all records that match a given filter.";
            /// <summary>
            /// Key representing the action to retrieve a page of records that match a given filter.
            /// </summary>
            public const string KeyGetPageByFilter = "GetPageByFilter";
            /// <summary>
            /// Description for retrieving a page of records matching a filter.
            /// </summary>
            public const string GetPageByFilter = "Retrieve a page of records that match a given filter.";
            /// <summary>
            /// Key representing the action to get the count of records matching a given filter.
            /// </summary>
            public const string KeyGetCountFilter = "GetCountFilter";
            /// <summary>
            /// Description for getting the count of records matching a filter.
            /// </summary>
            public const string GetCountFilter = "Get the count of records that match a given filter.";
            /// <summary>
            /// Message indicating that the 'name' parameter cannot be null, empty, or whitespace.
            /// </summary>
            public const string ArgumentNullExceptionName = "The 'name' parameter cannot be null, empty, or whitespace.";
            /// <summary>
            /// Message indicating that the 'description' parameter cannot be null, empty, or whitespace.
            /// </summary>
            public const string ArgumentNullExceptionDescription = "The 'description' parameter cannot be null, empty, or whitespace.";
            /// <summary>
            /// Message indicating that an operation with the same name already exists.
            /// This is used to prevent duplicate operations within the system.
            /// </summary>
            public const string InvalidOperationException = "An operation with the name '{0}' already exists.";
        }
    }
}