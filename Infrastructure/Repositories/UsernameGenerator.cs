namespace Infrastructure.Repositories
{
    /// <summary>
    /// A utility class for generating username suggestions based on a given username.
    /// </summary>
    public static class UsernameGenerator
    {
        private static readonly Random RandomInstance = new Random();
        private const string DefaultSuffix = "authflow";
        private const int RandomLimit = 2;

        /// <summary>
        /// Generate a list of username suggestions based on a given username.
        /// </summary>
        /// <param name="username">The original username to base suggestions on.</param>
        /// <param name="size">The desired number of suggestions to generate.</param>
        /// <returns>A list of generated username suggestions.</returns>
        public static IEnumerable<string> GenerateUsernameSuggestions(string username, int size = 10)
        {
            var suggestions = new HashSet<string>
            {
                $"{username}.{DefaultSuffix}"
            };

            while (suggestions.Count < size)
            {
                string suffix = GenerateSuffix();
                suggestions.Add($"{username}.{suffix}");
            }

            return suggestions.ToList();
        }

        /// <summary>
        /// Generate a suffix for the username based on randomness.
        /// </summary>
        /// <returns>The generated suffix.</returns>
        private static string GenerateSuffix()
        {
            bool isNumberSuffix = RandomInstance.Next(0, RandomLimit) == 1;
            return isNumberSuffix ? GenerateRandomNumber() : GetCommonWord();
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
            int index = RandomInstance.Next(0, 19);
            return CommonWords.GetCommonWords()[index];
        }
    }
}
