namespace Application.Result.Exceptions
{
    // Represents an exception for invalid operation results.
    public class InvalidOperation : Exception
    {
        /// <summary>
        // Constructor for the exception that accepts an error message.
        /// </summary>
        /// <param name="message">The message</param>
        public InvalidOperation(string message): base(message)  // Call the base exception class with the provided message.
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentNullException(nameof(message), "The 'message' parameter cannot be null, empty, or whitespace.");
            }
        }
    }
}
