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
            // List of common words that can be used for naming
            string[] name = new string[]
            {
                "User",
                "Star",
                "Moon",
                "Sun",
                "Planet",
                "Light",
                "Night",
                "Day",
                "Bright",
                "Dark",
                "Shadow",
                "Dream",
                "Storm",
                "Ocean",
                "Mountain",
                "River",
                "Cloud",
                "Spark",
                "Fire",
                "Ice"
            };

            // Convert the array of words to an immutable list
            return name.ToImmutableList();
        }
    }
}
