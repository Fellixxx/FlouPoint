namespace Domain.EnumType.Extensions
{
    using Domain.Constants;
    using System;

    /// <summary>
    /// Attribute used to provide metadata for enum fields.
    /// This includes a name and a description for each enum field.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class EnumMetadata : Attribute
    {
        /// <summary>
        /// Gets the name representation of the enum field.
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Gets the descriptive text associated with the enum field.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref = "EnumMetadata"/> class
        /// with a specified name and description for an enum field.
        /// </summary>
        /// <param name = "name">The name representation of the enum field.</param>
        /// <param name = "description">The description of the enum field.</param>
        /// <exception cref = "ArgumentNullException">
        /// Thrown when the <paramref name = "name"/> or <paramref name = "description"/> is null or whitespace.
        /// </exception>
        public EnumMetadata(string name, string description)
        {
            // Validate that neither the name nor the description is null or whitespace.
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(description))
            {
                // Throw an ArgumentNullException with a specified message if validation fails.
                throw new ArgumentNullException(Messages.EnumMetadata.ForNameOrDescription);
            }

            // Assign values to the respective properties.
            Name = name;
            Description = description;
        }
    }
}