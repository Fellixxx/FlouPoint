namespace Domain.EnumType.OperationExecute
{
    /// <summary>
    /// Defines the types of operations that can be executed.
    /// </summary>
    public class OperationExecute
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        private static readonly Dictionary<string, OperationExecute> _operations = new();

        private OperationExecute(string name, string description)
        {
            Name = name;
            Description = description;
        }

        private static OperationExecute RegisterOperation(string name, string description)
        {
            if (_operations.ContainsKey(name))
            {
                return _operations[name];
            }

            var operation = new OperationExecute(name, description);
            _operations[name] = operation;
            return operation;
        }

        // Predefined Operations
        public static OperationExecute Add => RegisterOperation("Add", "Add a new record.");
        public static OperationExecute Modified => RegisterOperation("Modified", "Modify an existing record.");
        public static OperationExecute Remove => RegisterOperation("Remove", "Remove an existing record.");
        public static OperationExecute Deactivate => RegisterOperation("Deactivate", "Deactivate an existing record.");
        public static OperationExecute Activate => RegisterOperation("Activate", "Activate a deactivated record.");
        public static OperationExecute GetUserById => RegisterOperation("GetUserById", "Retrieve a user by their ID.");
        public static OperationExecute GetAllByFilter => RegisterOperation("GetAllByFilter", "Retrieve all records that match a given filter.");
        public static OperationExecute GetPageByFilter => RegisterOperation("GetPageByFilter", "Retrieve a page of records that match a given filter.");
        public static OperationExecute GetCountFilter => RegisterOperation("GetCountFilter", "Get the count of records that match a given filter.");

        public static OperationExecute Activate2 => RegisterOperation("Activate", "Activate a deactivated record.");

        // Allow for dynamic operations to be created
        public static OperationExecute CreateCustomOperation(string name, string description)
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
            return RegisterOperation(name, description);
        }

        public static string? GetName(OperationExecute enumType)
        {
            ArgumentNullException.ThrowIfNull(enumType);
            return enumType.Name;
        }
    }
}
