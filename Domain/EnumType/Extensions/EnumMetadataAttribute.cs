namespace Domain.EnumType.Extensions
{
    using System;

    /// <summary>
    /// Attribute used to provide metadata for enum fields.
    /// This includes a name and a description for each enum field.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class EnumMetadataAttribute : Attribute
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
        /// Initializes a new instance of the <see cref="EnumMetadataAttribute"/> class
        /// with a specified name and description for an enum field.
        /// </summary>
        /// <param name="name">The name representation of the enum field.</param>
        /// <param name="description">The description of the enum field.</param>
        public EnumMetadataAttribute(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentNullException("For name or description, null, empty, and whitespace are not allowed.");
            }

            Name = name;
            Description = description;
        }
    }
}
