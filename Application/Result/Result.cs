namespace Application.Result
{
    using Application.Result.Error;
    using Domain.EnumType.Extensions;

    /// <summary>
    /// Represents the result of an operation, encapsulating the outcome with data, success status, and error information.
    /// </summary>
    /// <typeparam name = "T">The type of data associated with the operation result.</typeparam>
    public class Result<T>
    {
        /// <summary>
        /// Specifies the type of error, if any, that occurred during the operation.
        /// Derived from <see cref = "ErrorTypes"/> enumeration.
        /// </summary>
        public ErrorTypes Type { get; set; }
        /// <summary>
        /// Indicates if the operation was successful.
        /// A value of true means the operation was successful, while false indicates failure.
        /// </summary>
        public bool IsSuccessful { get; protected set; }
        /// <summary>
        /// Contains the data associated with the operation result.
        /// This property holds the result data if the operation was successful, otherwise it may be null.
        /// </summary>
        public T? Data { get; protected set; }
        /// <summary>
        /// Provides additional details about the operation, such as error messages or success information.
        /// Can be used for logging or returning user-friendly messages.
        /// </summary>
        public string? Message { get; protected set; }
        /// <summary>
        /// Retrieves the custom string name of the error type, if any.
        /// Utilizes the extension method <see cref = "GetCustomName"/> to obtain a user-friendly representation of the error type.
        /// </summary>
        public string Error => this.Type.GetCustomName();
    }
}