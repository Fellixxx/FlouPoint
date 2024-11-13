namespace UtilitiesLayer
{
    using System.Collections.Immutable;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Utility class for various login-related operations.
    /// </summary>
    public static class CredentialUtility
    {
        /// <summary>
        /// Computes the SHA256 hash of the provided string.
        /// Used for securely hashing passwords before storing them in a database.
        /// </summary>
        /// <param name = "rawData">The string data to be hashed.</param>
        /// <returns>A hexadecimal string representation of the SHA256 hash.</returns>
        /// <exception cref = "ArgumentException">Thrown when the input data is null or empty.</exception>
        public static string ComputeSha256Hash(string rawData)
        {
            // Validate input
            if (string.IsNullOrEmpty(rawData))
            {
                throw new ArgumentException("Input data cannot be null or empty.", nameof(rawData));
            }

            // Compute the hash as a byte array
            byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(rawData));
            // Convert byte array to a hexadecimal string
            StringBuilder builder = new(64); // 256 bits == 32 bytes == 64 hexadecimal characters
            foreach (byte b in bytes)
            {
                // Append each byte as a two-digit hexadecimal string
                builder.Append(b.ToString("x2"));
            }

            return builder.ToString();
        }

        /// <summary>
        /// Generates a list of username suggestions based on a provided username.
        /// </summary>
        /// <param name = "username">The base username to generate suggestions from.</param>
        /// <param name = "commonWords">A list of common words to use as suffix suggestions.</param>
        /// <returns>A list of suggested usernames.</returns>
        public static List<string> GenerateUsernameSuggestions(string username, ImmutableList<string> commonWords)
        {
            Random random = new();
            List<string> suggestions = new()
            {
                $"{username}.authflow"};
            do
            {
                // Randomly decide whether to use a number suffix
                bool isNumberSuffix = random.Next(0, 2).Equals(1);
                // Generate a suffix using either a number or a common word
                string suffix = isNumberSuffix ? GetNumber(random) : GetCommonWord(random, commonWords);
                // Create a suggestion by appending the suffix to the original username
                string suggestion = $"{username}.{suffix}";
                suggestions.Add(suggestion);
            }
            while (suggestions.Count < 100); // Generate until we have 100 suggestions
            return suggestions;
        }

        /// <summary>
        /// Selects a random common word from the list provided.
        /// </summary>
        /// <param name = "random">The Random instance used to generate random numbers.</param>
        /// <param name = "commonWords">A list of common words.</param>
        /// <returns>A random common word in lowercase.</returns>
        private static string GetCommonWord(Random random, ImmutableList<string> commonWords)
        {
            // Select a random index in the common words list
            int indexCommonWord = random.Next(commonWords.Count);
            return commonWords[indexCommonWord].ToLower(); // Return the word in lowercase
        }

        /// <summary>
        /// Generates a random 4-digit number as a string.
        /// </summary>
        /// <param name = "random">The Random instance used to generate random numbers.</param>
        /// <returns>A string representing a random 4-digit number.</returns>
        private static string GetNumber(Random random)
        {
            return random.Next(1000, 9999).ToString();
        }

        /// <summary>
        /// Validates if a given string follows the user pattern (criteria for usernames).
        /// </summary>
        /// <param name = "user">The user string to validate.</param>
        /// <returns>True if the string matches the user pattern; otherwise, false.</returns>
        public static bool IsUser(string user)
        {
            string emailPattern = @"^[a-zA-Z0-9.]{0,100}$"; // Allows alphanumeric and dot characters, up to 100 chars
            return Regex.IsMatch(user, emailPattern);
        }

        /// <summary>
        /// Validates if a given string is a valid email address.
        /// </summary>
        /// <param name = "email">The email string to validate.</param>
        /// <returns>True if the string is a valid email; otherwise, false.</returns>
        public static bool IsValidEmail(string email)
        {
            // Simple email pattern to check basic email structure
            string emailPattern = @"^[\w-]+(\.[\w-]+)*@([\w-]+\.)*\w+[\w-]$";
            return Regex.IsMatch(email, emailPattern);
        }
    }
}