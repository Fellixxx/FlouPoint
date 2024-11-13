namespace Infrastructure.Repositories.Abstract.CRUD.Query
{
    using Application.Result;
    using Application.UseCases.ExternalServices;
    using Application.UseCases.Repository;
    using Infrastructure.Constants;
    using Newtonsoft.Json.Linq;
    using System.Text;

    /// <summary>
    /// Provides helper methods to work with JWT tokens.
    /// </summary>
    public static class JwtHelper
    {
        //private readonly IResourceProvider _resourceProvider;
        //private IResourceHandler _resourceHandler;
        //private readonly List<string> _resourceKeys;

        /// <summary>
        /// Extracts the user data ID from the JWT token.
        /// </summary>
        /// <param name="bearerToken">The JWT token with the Bearer prefix.</param>
        /// <returns>The user data ID or null if not found.</returns>
        public static Operation<string> ExtractJwtPayload(string bearerToken)
        {
            var strategy = new BusinessStrategy<string>();
            if (string.IsNullOrWhiteSpace(bearerToken))
            {
                var tokenCannotBeWhitespace = Message.JwtHelper.TokenCannotBeWhitespace;
                return OperationStrategy<string>.Fail(tokenCannotBeWhitespace, strategy);
            }

            if (!bearerToken.StartsWith(Message.JwtHelper.BearerPrefix, StringComparison.OrdinalIgnoreCase))
            {
                var tokenMustStartWithBearer = Message.JwtHelper.TokenMustStartWithBearer;
                return OperationStrategy<string>.Fail(tokenMustStartWithBearer, strategy);
            }

            if (Message.JwtHelper.BearerPrefix.Length >= bearerToken.Length)
            {
                var invalidBearerTokenLength = Message.JwtHelper.InvalidBearerTokenLength;
                return OperationStrategy<string>.Fail(invalidBearerTokenLength, strategy);
            }

            var jwt = bearerToken[Message.JwtHelper.BearerPrefix.Length..].Trim();

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
        private static Operation<string> ExtractPayloadFromJwt(string jwt)
        {
            var tokenParts = jwt.Split('.');
            if (tokenParts.Length != 3)
            {
                var invalidJwtPayloadFormat = Message.JwtHelper.InvalidJwtPayloadFormat;
                var strategy = new BusinessStrategy<string>();
                return OperationStrategy<string>.Fail(invalidJwtPayloadFormat, strategy);
            }

            return Base64UrlDecode(tokenParts[1]);
        }

        /// <summary>
        /// Parses the JWT payload to extract the user data ID.
        /// </summary>
        /// <param name="payload">The JWT payload string.</param>
        /// <returns>The user data ID or an error result.</returns>
        private static Operation<string> ParsePayloadForUserData(string payload)
        {
            try
            {
                var jsonObject = JObject.Parse(payload);
                var idPayload = jsonObject[Message.JwtHelper.UserDataClaim]?.ToString() ?? string.Empty;
                return Operation<string>.Success(RemoveFirsAndLastCharacters(idPayload), Message.JwtHelper.Success);
            }
            catch
            {
                var strategy = new BusinessStrategy<string>();
                var invalidBearerTokenLength = Message.JwtHelper.InvalidBearerTokenLength;
                return OperationStrategy<string>.Fail(invalidBearerTokenLength, strategy);
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
        private static Operation<string> Base64UrlDecode(string base64Url)
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
                var strategy = new BusinessStrategy<string>();
                return OperationStrategy<string>.Fail(Message.JwtHelper.InvalidBase64UrlFormat, strategy);
            }

            return Operation<string>.Success(Encoding.UTF8.GetString(bytes), Message.JwtHelper.Success);
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
