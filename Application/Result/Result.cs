namespace Application.Result
{
    using Application.Result.Error;
    using global::Domain.EnumType;

    /// <summary>
    /// Represents the result of an operation.
    /// </summary>
    /// <typeparam name="T">The type of data associated with the operation result.</typeparam>
    public class Result<T>
    {
        /// <summary>
        /// Specifies the type of error, if any, that occurred during the operation.
        /// </summary>
        protected ErrorTypes ErrorType { get; set; }

        /// <summary>
        /// Indicates if the operation was successful.
        /// </summary>
        public bool IsSuccessful { get; protected set; }

        /// <summary>
        /// Contains the data associated with the operation result.
        /// </summary>
        public T? Data { get; protected set; }

        /// <summary>
        /// Provides additional details about the operation, such as error messages or success information.
        /// </summary>
        public string? Message { get; protected set; }

        /// <summary>
        /// Specifies the type of error, if any, that occurred during the operation as a string.
        /// </summary>
        public string Error => this.ErrorType.GetCustomName();
    }
}