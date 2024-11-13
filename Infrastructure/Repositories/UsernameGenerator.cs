namespace Infrastructure.Repositories
{
    /// <summary>
    /// A utility class for generating username suggestions based on a given username.
    /// </summary>
    public static class UsernameGenerator
    {
        private static readonly Random RandomInstance = new(); // Random generator instance for suffix creation
        private const string DefaultSuffix = "flou"; // Default suffix used when generating username suggestions
        private const int RandomLimit = 2; // Limit for random selection between number suffix and word suffix
        /// <summary>
        /// Generate a list of username suggestions based on a given username.
        /// </summary>
        /// <param name = "username">The original username to base suggestions on.</param>
        /// <param name = "size">The desired number of suggestions to generate.</param>
        /// <returns>A list of generated username suggestions.</returns>
        public static IEnumerable<string> GenerateUsernameSuggestions(string username, int size = 10)
        {
            var suggestions = new HashSet<string>
            {
                $"{username}.{DefaultSuffix}" // Begin with a default suggestion
            };
            // Continue generating suggestions until the desired number is reached
            while (suggestions.Count < size)
            {
                string suffix = GenerateSuffix();
                suggestions.Add($"{username}.{suffix}"); // Add new unique suggestion with generated suffix
            }

            return suggestions.ToList(); // Convert HashSet to List and return
        }

        /// <summary>
        /// Generate a suffix for the username based on randomness.
        /// </summary>
        /// <returns>The generated suffix.</returns>
        private static string GenerateSuffix()
        {
            bool isNumberSuffix = RandomInstance.Next(0, RandomLimit) == 1; // Randomly decide if suffix should be numeric
            return isNumberSuffix ? GenerateRandomNumber() : GetCommonWord(); // Choose suffix type based on randomness
        }

        /// <summary>
        /// Generate a random 4-digit number as a suffix.
        /// </summary>
        /// <returns>The generated random number.</returns>
        private static string GenerateRandomNumber()
        {
            return RandomInstance.Next(1000, 10000).ToString(); // Generates a random 4-digit number
        }

        /// <summary>
        /// Get a common word as a suffix.
        /// </summary>
        /// <returns>The retrieved common word.</returns>
        private static string GetCommonWord()
        {
            int index = RandomInstance.Next(0, 19); // Select a random index for common word
            return CommonWords.GetCommonWords()[index]; // Fetch a common word from a predefined list
        }
    }
}