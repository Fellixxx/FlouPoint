namespace Domain.EnumType.Extensions
{
    using Domain.Constants;
    using System;
    using System.Reflection;

    /// <summary>
    /// Provides extension methods for enums.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Retrieves a custom name for the enum value based on the EnumMetadataAttribute, if available.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="enumValue">The enum value to get the custom name for.</param>
        /// <returns>A custom name as defined in EnumMetadataAttribute, or "UNKNOWN" if not defined.</returns>
        public static string GetCustomName<TEnum>(this TEnum enumValue) where TEnum : struct, Enum
        {
            return GetEnumMetadata(enumValue)?.Name ?? Messages.EnumExtensions.Unknown;
        }

        /// <summary>
        /// Retrieves a description for the enum value based on the EnumMetadataAttribute, if available.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="enumValue">The enum value to get the description for.</param>
        /// <returns>A description as defined in EnumMetadataAttribute, or a default message if not defined.</returns>
        public static string GetDescription<TEnum>(this TEnum enumValue) where TEnum : struct, Enum
        {
            return GetEnumMetadata(enumValue)?.Description ?? Messages.EnumExtensions.DescriptionNotAvailable;
        }

        /// <summary>
        /// Parses a string to its corresponding enum value.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="_value">The string representation of the enum value to parse.</param>
        /// <returns>The corresponding enum value of type TEnum.</returns>
        /// <exception cref="ArgumentException">Thrown if the string does not match any enum value.</exception>
        public static TEnum GetEnumByName<TEnum>(this string _value) where TEnum : struct, Enum
        {
            if (Enum.TryParse(_value, out TEnum result))
            {
                return result;
            }
            else
            {
                throw new ArgumentException(string.Format(Messages.EnumExtensions.NoEnumValueFound, _value, typeof(TEnum)));
            }
        }

        /// <summary>
        /// Retrieves the EnumMetadataAttribute associated with a specific enum value, if available.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="enumValue">The enum value to get the metadata for.</param>
        /// <returns>The EnumMetadataAttribute associated with the enum value, or null if not available.</returns>
        private static EnumMetadata? GetEnumMetadata<TEnum>(TEnum enumValue) where TEnum : Enum
        {
            var type = enumValue.GetType();
            var name = Enum.GetName(type, enumValue);
            if (name is not null)
            {
                var field = type.GetField(name);
                if (field?.GetCustomAttribute<EnumMetadata>(false) is EnumMetadata attribute)
                {
                    return attribute;
                }
            }

            return null;
        }
    }
}
