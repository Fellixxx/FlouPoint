namespace Infrastructure.Repositories
{
    using System.Collections.Immutable;

    /// <summary>
    /// A utility class providing common words used for naming entities or objects.
    /// </summary>
    internal static class CommonWords
    {
        /// <summary>
        /// Get a collection of common words that can be used for naming.
        /// </summary>
        /// <returns>An immutable list of common words.</returns>
        public static ImmutableList<string> GetCommonWords()
        {
            // Define an array of common words that can be used for naming purposes.
            // These words could be useful in various contexts like generating
            // default names or placeholders for entities.
            string[] name = new string[]
            {
                "User", // Can be used for naming a person or account-related entity
                "Star", // Suitable for astronomical or rating systems
                "Moon", // Could be used in contexts related to lunar themes
                "Sun", // Relevant for solar or bright-themed contexts
                "Planet", // Useful for naming in celestial or space themes
                "Light", // Can denote brightness or enlightenment themes
                "Night", // Suitable for dark or evening-related contexts
                "Day", // Can be used for daytime or light-related themes
                "Bright", // Indicative of vivid or light-filled themes
                "Dark", // Suitable for shadowy or night-themed contexts
                "Shadow", // Can be used in mysterious or contrasting themes
                "Dream", // Suitable for aspirations or imaginary contexts
                "Storm", // Can denote violent or turbulent themes
                "Ocean", // Suitable for vastness or marine-related contexts
                "Mountain", // Can be used in themes denoting stability or largeness
                "River", // Suitable for flow or natural-themed contexts
                "Cloud", // Can be used in contexts related to computing or weather
                "Spark", // Suitable for ideas or igniting themes
                "Fire", // Can denote warmth or destruction themes
                "Ice" // Suitable for cold or calm-themed contexts
            };
            // Convert the array of words to an immutable list, which ensures
            // the collection of common words cannot be modified after creation.
            return name.ToImmutableList();
        }
    }
}