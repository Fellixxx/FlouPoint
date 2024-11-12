namespace Infrastructure.Repositories.Abstract.CRUD.Query
{
    using Application.Result;
    using Application.UseCases.ExternalServices;
    using Application.UseCases.Repository;
    using Newtonsoft.Json.Linq;
    using System.Text;

    /// <summary>
    /// Provides helper methods to work with JWT tokens.
    /// </summary>
    public static class JwtHelper
    {
        private const string BearerPrefix = "Bearer ";
        private const string UserData = "http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata";
        private const string Whitespace = "Token cannot be null or whitespace.";
        private const string StartBearer = "Token must start with 'Bearer ' prefix.";
        private const string LengthBearer = "The length of the 'bearer' string is less than the 'bearer token' string.";
        private const string InvalidJwt = "Invalid JWT payload format.";
        private const string InvalidBase64Url = "Invalid Base64Url format.";
        //private readonly IResourceProvider _resourceProvider;
        //private IResourceHandler _resourceHandler;
        //private readonly List<string> _resourceKeys;

        /// <summary>
        /// Extracts the user data ID from the JWT token.
        /// </summary>
        /// <param name="bearerToken">The JWT token with the Bearer prefix.</param>
        /// <returns>The user data ID or null if not found.</returns>
        public static OperationResult<string> ExtractJwtPayload(string bearerToken)
        {
            if (string.IsNullOrWhiteSpace(bearerToken))
            {
                return OperationBuilder<string>.FailureBusinessValidation(Whitespace);
            }

            if (!bearerToken.StartsWith(BearerPrefix, StringComparison.OrdinalIgnoreCase))
            {
                return OperationBuilder<string>.FailureBusinessValidation(StartBearer);
            }

            if (BearerPrefix.Length >= bearerToken.Length)
            {
                return OperationBuilder<string>.FailureBusinessValidation(LengthBearer);
            }

            var jwt = bearerToken[BearerPrefix.Length..].Trim();

            // Extract the payload part of the JWT
            var result = ExtractPayloadFromJwt(jwt);
            if (!result.IsSuccessful)
            {
                return result;
            }

            // Parse the payload to retrieve the user data ID
            var data = result.Data ?? string.Empty;
            return ParsePayloadForUserData(data);
        }

        /// <summary>
        /// Extracts the payload part of a JWT token.
        /// </summary>
        /// <param name="jwt">The JWT string.</param>
        /// <returns>The extracted payload or an error result.</returns>
        private static OperationResult<string> ExtractPayloadFromJwt(string jwt)
        {
            var tokenParts = jwt.Split('.');
            if (tokenParts.Length != 3)
            {
                return OperationBuilder<string>.FailureBusinessValidation(InvalidJwt);
            }

            return Base64UrlDecode(tokenParts[1]);
        }

        /// <summary>
        /// Parses the JWT payload to extract the user data ID.
        /// </summary>
        /// <param name="payload">The JWT payload string.</param>
        /// <returns>The user data ID or an error result.</returns>
        private static OperationResult<string> ParsePayloadForUserData(string payload)
        {
            try
            {
                var jsonObject = JObject.Parse(payload);
                var idPayload = jsonObject[UserData]?.ToString() ?? string.Empty;
                return OperationResult<string>.Success(RemoveFirsAndLastCharacters(idPayload), Resource.GlobalOkMessage);
            }
            catch
            {
                return OperationBuilder<string>.FailureBusinessValidation(InvalidJwt);
            }
        }

        /// <summary>
        /// Removes the first and last characters from a string.
        /// Useful for trimming quotations from serialized JSON values.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>The modified string.</returns>
        private static string RemoveFirsAndLastCharacters(string input)
        {
            return input.Substring(1, input.Length - 2);
        }

        /// <summary>
        /// Decodes a Base64Url encoded string.
        /// </summary>
        /// <param name="base64Url">The Base64Url encoded string.</param>
        /// <returns>The decoded string or an error result.</returns>
        private static OperationResult<string> Base64UrlDecode(string base64Url)
        {
            var padded = base64Url.Length % 4 == 0 ? base64Url : GetBase64Url(base64Url);
            var base64 = padded.Replace("_", "/").Replace("-", "+");

            byte[] bytes;
            try
            {
                bytes = Convert.FromBase64String(base64);
            }
            catch
            {
                return OperationBuilder<string>.FailureBusinessValidation(InvalidBase64Url);
            }

            return OperationResult<string>.Success(Encoding.UTF8.GetString(bytes), Resource.GlobalOkMessage);
        }

        /// <summary>
        /// Pads a Base64Url encoded string to ensure it's a valid length.
        /// </summary>
        /// <param name="base64Url">The Base64Url encoded string.</param>
        /// <returns>The padded string.</returns>
        private static string GetBase64Url(string base64Url)
        {
            return string.Concat(base64Url, "====".AsSpan(base64Url.Length % 4));
        }
    }
}
