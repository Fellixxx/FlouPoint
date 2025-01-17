namespace Infrastructure.Repositories.Abstract.CRUD.Query
{
    using Application.Result;
    using Infrastructure.Constants;
    using Newtonsoft.Json.Linq;
    using System.Text;

    /// <summary>
    /// Provides helper methods to work with JWT tokens.
    /// </summary>
    public static class JwtHelper
    {
        // Uncomment and use these fields if you need to work with resources.
        // private readonly IResourceProvider _resourceProvider;
        // private IResourceHandler _resourceHandler;
        // private readonly List<string> _resourceKeys;
        /// <summary>
        /// Extracts the user data ID from the JWT token.
        /// </summary>
        /// <param name = "bearerToken">The JWT token with the Bearer prefix.</param>
        /// <returns>The user data ID or null if not found.</returns>
        public static Operation<string> ExtractJwtPayload(string bearerToken)
        {
            var strategy = new BusinessStrategy<string>();
            // Validate the bearer token is not null, empty, or whitespace.
            if (string.IsNullOrWhiteSpace(bearerToken))
            {
                var tokenCannotBeWhitespace = Message.JwtHelper.TokenCannotBeWhitespace;
                return OperationStrategy<string>.Fail(tokenCannotBeWhitespace, strategy);
            }

            // Ensure bearer token starts with the expected Bearer prefix.
            if (!bearerToken.StartsWith(Message.JwtHelper.BearerPrefix, StringComparison.OrdinalIgnoreCase))
            {
                var tokenMustStartWithBearer = Message.JwtHelper.TokenMustStartWithBearer;
                return OperationStrategy<string>.Fail(tokenMustStartWithBearer, strategy);
            }

            // Check for valid length of the bearer token.
            if (Message.JwtHelper.BearerPrefix.Length >= bearerToken.Length)
            {
                var invalidBearerTokenLength = Message.JwtHelper.InvalidBearerTokenLength;
                return OperationStrategy<string>.Fail(invalidBearerTokenLength, strategy);
            }

            // Remove Bearer prefix and extract the payload of JWT
            var jwt = bearerToken[Message.JwtHelper.BearerPrefix.Length..].Trim();
            var result = ExtractPayloadFromJwt(jwt);
            if (!result.IsSuccessful)
            {
                return result;
            }

            // Parse the extracted payload to retrieve the user data ID.
            var data = result.Data ?? string.Empty;
            return ParsePayloadForUserData(data);
        }

        /// <summary>
        /// Extracts the payload part of a JWT token.
        /// </summary>
        /// <param name = "jwt">The JWT string.</param>
        /// <returns>The extracted payload or an error result.</returns>
        private static Operation<string> ExtractPayloadFromJwt(string jwt)
        {
            // Split the JWT into its parts and validate the format.
            var tokenParts = jwt.Split('.');
            if (tokenParts.Length != 3)
            {
                var invalidJwtPayloadFormat = Message.JwtHelper.InvalidJwtPayloadFormat;
                var strategy = new BusinessStrategy<string>();
                return OperationStrategy<string>.Fail(invalidJwtPayloadFormat, strategy);
            }

            // Decode the payload section using Base64Url.
            return Base64UrlDecode(tokenParts[1]);
        }

        /// <summary>
        /// Parses the JWT payload to extract the user data ID.
        /// </summary>
        /// <param name = "payload">The JWT payload string.</param>
        /// <returns>The user data ID or an error result.</returns>
        private static Operation<string> ParsePayloadForUserData(string payload)
        {
            try
            {
                // Convert the payload into a JSON object and extract the user data claim.
                var jsonObject = JObject.Parse(payload);
                var idPayload = jsonObject[Message.JwtHelper.UserDataClaim]?.ToString() ?? string.Empty;
                // Remove extraneous characters from the user data ID.
                return Operation<string>.Success(RemoveFirsAndLastCharacters(idPayload), Message.JwtHelper.Success);
            }
            catch
            {
                // Handle JSON parsing errors.
                var strategy = new BusinessStrategy<string>();
                var invalidBearerTokenLength = Message.JwtHelper.InvalidBearerTokenLength;
                return OperationStrategy<string>.Fail(invalidBearerTokenLength, strategy);
            }
        }

        /// <summary>
        /// Removes the first and last characters from a string.
        /// Useful for trimming quotations from serialized JSON values.
        /// </summary>
        /// <param name = "input">The input string.</param>
        /// <returns>The modified string.</returns>
        private static string RemoveFirsAndLastCharacters(string input)
        {
            return input.Substring(1, input.Length - 2);
        }

        /// <summary>
        /// Decodes a Base64Url encoded string.
        /// </summary>
        /// <param name = "base64Url">The Base64Url encoded string.</param>
        /// <returns>The decoded string or an error result.</returns>
        private static Operation<string> Base64UrlDecode(string base64Url)
        {
            // Ensure the encoded string length is valid and properly padded.
            var padded = base64Url.Length % 4 == 0 ? base64Url : GetBase64Url(base64Url);
            var base64 = padded.Replace("_", "/").Replace("-", "+");
            byte[] bytes;
            try
            {
                // Decode the Base64 string into bytes.
                bytes = Convert.FromBase64String(base64);
            }
            catch
            {
                // Handling decoding errors.
                var strategy = new BusinessStrategy<string>();
                return OperationStrategy<string>.Fail(Message.JwtHelper.InvalidBase64UrlFormat, strategy);
            }

            // Convert decoded bytes to a UTF-8 string.
            return Operation<string>.Success(Encoding.UTF8.GetString(bytes), Message.JwtHelper.Success);
        }

        /// <summary>
        /// Pads a Base64Url encoded string to ensure it's a valid length.
        /// </summary>
        /// <param name = "base64Url">The Base64Url encoded string.</param>
        /// <returns>The padded string.</returns>
        private static string GetBase64Url(string base64Url)
        {
            // Add padding characters to make the length a multiple of 4.
            return string.Concat(base64Url, "====".AsSpan(base64Url.Length % 4));
        }
    }
}