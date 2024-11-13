namespace Domain.EnumType
{
    /// <summary>
    /// Defines the types of operations that can be executed.
    /// </summary>
    public class ActionType
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        private static readonly Dictionary<string, ActionType> _operations = new();

        private ActionType(string name, string description)
        {
            Name = name;
            Description = description;
        }

        private static ActionType Register(string name, string description)
        {
            if (_operations.ContainsKey(name))
            {
                return _operations[name];
            }

            var operation = new ActionType(name, description);
            _operations[name] = operation;
            return operation;
        }

        // Predefined Operations
        public static ActionType Add => Register("Add", "Add a new record.");
        public static ActionType Modified => Register("Modified", "Modify an existing record.");
        public static ActionType Remove => Register("Remove", "Remove an existing record.");
        public static ActionType Deactivate => Register("Deactivate", "Deactivate an existing record.");
        public static ActionType Activate => Register("Activate", "Activate a deactivated record.");
        public static ActionType GetUserById => Register("GetUserById", "Retrieve a user by their ID.");
        public static ActionType GetAllByFilter => Register("GetAllByFilter", "Retrieve all records that match a given filter.");
        public static ActionType GetPageByFilter => Register("GetPageByFilter", "Retrieve a page of records that match a given filter.");
        public static ActionType GetCountFilter => Register("GetCountFilter", "Get the count of records that match a given filter.");

        // Allow for dynamic operations to be created
        public static ActionType CreateCustomOperation(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name), "The 'name' parameter cannot be null, empty, or whitespace.");
            }
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentNullException(nameof(description), "The 'description' parameter cannot be null, empty, or whitespace.");
            }
            if (_operations.ContainsKey(name))
            {
                throw new InvalidOperationException($"An operation with the name '{name}' already exists.");
            }
            return Register(name, description);
        }

        public static string? GetName(ActionType enumType)
        {
            ArgumentNullException.ThrowIfNull(enumType);
            return enumType.Name;
        }
    }
}
