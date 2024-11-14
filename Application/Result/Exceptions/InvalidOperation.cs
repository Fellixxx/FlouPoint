namespace Application.Result.Exceptions
{
    using Application.Constants;

    /// <summary>
    /// Represents an exception that is thrown when an invalid operation result occurs.
    /// </summary>
    public class InvalidOperation : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref = "InvalidOperation"/> class with a specified error message.
        /// </summary>
        /// <param name = "message">The error message that explains the reason for the exception.</param>
        /// <exception cref = "ArgumentNullException">Thrown when the <paramref name = "message"/> is null, empty, or consists only of white-space characters.</exception>
        public InvalidOperation(string message) : base(message)
        {
            // Check if the provided message is null or consists only of white-space characters.
            // If so, throw an ArgumentNullException with a specific message from the Messages class.
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentNullException(nameof(message), Messages.InvalidOperation.NullMessage);
            }
        }
    }
}