using Domain.Constants;

namespace Domain.EnumType
{
    /// <summary>
    /// Defines the types of operations that can be executed.
    /// This class holds a collection of predefined and custom action types that represent various operations within the domain.
    /// </summary>
    public class ActionType
    {
        /// <summary>
        /// Gets the name of the action type.
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Gets the description of the action type.
        /// </summary>
        public string Description { get; private set; }

        // Dictionary to store the registered action types.
        private static readonly Dictionary<string, ActionType> _operations = new();
        // Private constructor to prevent direct instantiation.
        private ActionType(string name, string description)
        {
            Name = name;
            Description = description;
        }

        /// <summary>
        /// Registers a new action type with the specified name and description.
        /// If an action type with the same name already exists, it returns the existing one.
        /// </summary>
        /// <param name = "name">The name of the action type.</param>
        /// <param name = "description">The description of the action type.</param>
        /// <returns>The registered ActionType instance.</returns>
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
        public static ActionType Add => Register(Messages.ActionType.KeyAdd, Messages.ActionType.Add);
        public static ActionType Modified => Register(Messages.ActionType.KeyModified, Messages.ActionType.Modified);
        public static ActionType Remove => Register(Messages.ActionType.KeyRemove, Messages.ActionType.Remove);
        public static ActionType Deactivate => Register(Messages.ActionType.KeyDeactivate, Messages.ActionType.Deactivate);
        public static ActionType Activate => Register(Messages.ActionType.KeyActivate, Messages.ActionType.Activate);
        public static ActionType GetUserById => Register(Messages.ActionType.KeyGetUserById, Messages.ActionType.GetUserById);
        public static ActionType GetAllByFilter => Register(Messages.ActionType.KeyGetAllByFilter, Messages.ActionType.GetAllByFilter);
        public static ActionType GetPageByFilter => Register(Messages.ActionType.KeyGetPageByFilter, Messages.ActionType.GetPageByFilter);
        public static ActionType GetCountFilter => Register(Messages.ActionType.KeyGetCountFilter, Messages.ActionType.GetCountFilter);

        /// <summary>
        /// Creates a custom operation with the specified name and description.
        /// Throws an exception if the name or description is null or if an operation with the same name already exists.
        /// </summary>
        /// <param name = "name">The name of the custom operation.</param>
        /// <param name = "description">The description of the custom operation.</param>
        /// <returns>The newly created ActionType instance.</returns>
        /// <exception cref = "ArgumentNullException">Thrown if the name or description is null or whitespace.</exception>
        /// <exception cref = "InvalidOperationException">Thrown if an operation with the same name already exists.</exception>
        public static ActionType CreateCustomOperation(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name), Messages.ActionType.ArgumentNullExceptionName);
            }

            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentNullException(nameof(description), Messages.ActionType.ArgumentNullExceptionDescription);
            }

            if (_operations.ContainsKey(name))
            {
                throw new InvalidOperationException(string.Format(Messages.ActionType.InvalidOperationException, name));
            }

            return Register(name, description);
        }

        /// <summary>
        /// Gets the name of the specified action type.
        /// </summary>
        /// <param name = "enumType">The ActionType instance.</param>
        /// <returns>The name of the action type, or null if the provided enumType is null.</returns>
        public static string? GetName(ActionType enumType)
        {
            ArgumentNullException.ThrowIfNull(enumType);
            return enumType.Name;
        }
    }
}