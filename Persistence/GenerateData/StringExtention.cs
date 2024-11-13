namespace AuthFlowServices.Persistence.Data
{
    /// <summary>
    /// Provides an extension method for string manipulation.
    /// </summary>
    internal static class StringExtention
    {
        /// <summary>
        /// Truncates the given string to a specified length if it exceeds that length.
        /// Useful to ensure a string does not exceed a certain number of characters.
        /// </summary>
        /// <param name = "value">The input string to be truncated, if necessary.</param>
        /// <param name = "maxLength">The maximum number of characters the string should have after truncation.</param>
        /// <returns>
        /// The original string if it's null, empty, or its length is less than or equal to the maxLength;
        /// otherwise, the string truncated to the specified maxLength.
        /// </returns>
        internal static string Truncate(this string value, int maxLength) => string.IsNullOrEmpty(value) ? value : value.Length <= maxLength ? value : value[..maxLength];
    }
}