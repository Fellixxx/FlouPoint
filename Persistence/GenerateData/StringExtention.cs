namespace AuthFlowServices.Persistence.Data
{
    /// <summary>
    /// Provides an extension method for strings.
    /// </summary>
    internal static class StringExtention
    {
        /// <summary>
        /// Truncates the given string to a specified length.
        /// </summary>
        /// <param name="value">The string to be truncated.</param>
        /// <param name="maxLength">Maximum length for the truncated string.</param>
        /// <returns>The truncated string.</returns>
        internal static string Truncate(this string value, int maxLength) =>
           string.IsNullOrEmpty(value) ? value : value.Length <= maxLength ? value : value[..maxLength];
    }
}
