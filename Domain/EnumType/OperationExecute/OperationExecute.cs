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
                throw new InvalidOperationException($"An operation with the name '{name}' already exists.");
            }

            var operation = new OperationExecute(name, description);
            _operations[name] = operation;
            return operation;
        }

        // Predefined Operations
        public static OperationExecute Add => new("Add", "Add a new record.");
        public static OperationExecute Modified => new("Modified", "Modify an existing record.");
        public static OperationExecute Remove => new("Remove", "Remove an existing record.");
        public static OperationExecute Deactivate => new("Deactivate", "Deactivate an existing record.");
        public static OperationExecute Activate => new("Activate", "Activate a deactivated record.");
        public static OperationExecute GetUserById => new("GetUserById", "Retrieve a user by their ID.");
        public static OperationExecute GetAllByFilter => new("GetAllByFilter", "Retrieve all records that match a given filter.");
        public static OperationExecute GetPageByFilter => new("GetPageByFilter", "Retrieve a page of records that match a given filter.");
        public static OperationExecute GetCountFilter => new("GetCountFilter", "Get the count of records that match a given filter.");

        public static OperationExecute Activate2 => new("Activate", "Activate a deactivated record.");

        // Allow for dynamic operations to be created
        public static OperationExecute CreateCustomOperation(string name, string description)
        {
            return RegisterOperation(name, description);
        }
        public static string? GetName(OperationExecute enumType)
        {
            ArgumentNullException.ThrowIfNull(enumType);
            return enumType.Name;
        }
    }
}
