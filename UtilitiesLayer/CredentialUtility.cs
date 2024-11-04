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
        /// Used for hashing passwords before storing in the database.
        /// </summary>
        public static string ComputeSha256Hash(string rawData)
        {
            // Validate input
            if (string.IsNullOrEmpty(rawData))
            {
                throw new ArgumentException("Input data cannot be null or empty.", nameof(rawData));
            }

            // ComputeHash - returns byte array  
            byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(rawData));

            // Convert byte array to a hexadecimal string   
            StringBuilder builder = new(64); // 256 bits == 32 bytes == 64 hexadecimal characters
            foreach (byte b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }

            return builder.ToString();
        }

        /// <summary>
        /// Generates a list of username suggestions based on a provided username.
        /// </summary>
        public static List<string> GenerateUsernameSuggestions(string username, ImmutableList<string> commonWords)
        {
            Random random = new();
            List<string> suggestions = new()
            {
                $"{username}.authflow"
            };

            do
            {
                bool isNumberSuffix = random.Next(0, 2).Equals(1);
                // Generate a 4-digit random number
                string suffix = isNumberSuffix ? GetNumber(random) : GetCommonWord(random, commonWords);
                // Append the suffix to the original username
                string suggestion = $"{username}.{suffix}";
                suggestions.Add(suggestion);
            } while (suggestions.Count < 100);

            return suggestions;
        }

        private static string GetCommonWord(Random random, ImmutableList<string> commonWords)
        {
            int indexCommonWord = random.Next(commonWords.Count);
            return commonWords[indexCommonWord].ToLower();
        }

        private static string GetNumber(Random random)
        {
            return random.Next(1000, 9999).ToString();
        }


        /// <summary>
        /// Validates if a given string follows the user pattern.
        /// </summary>
        public static bool IsUser(string user)
        {
            string emailPattern = @"^[a-zA-Z0-9.]{0,100}$";
            return Regex.IsMatch(user, emailPattern);
        }

        /// <summary>
        /// Validates if a given string is a valid email address.
        /// </summary>
        public static bool IsValidEmail(string email)
        {
            string emailPattern = @"^[\w-]+(\.[\w-]+)*@([\w-]+\.)*\w+[\w-]$";
            return Regex.IsMatch(email, emailPattern);
        }
    }
}
