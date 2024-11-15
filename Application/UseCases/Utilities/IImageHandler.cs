namespace Application.UseCases.Utilities
{
    using Application.Result;

    /// <summary>
    /// Defines the contract for interacting with external drive storage services.
    /// </summary>
    public interface IImageHandler
    {
        /// <summary>
        /// Uploads a file to the external drive storage service asynchronously.
        /// </summary>
        /// <param name="stream">The stream of the file to upload.</param>
        /// <param name="filename">The name of the file.</param>
        /// <returns>An OperationResult indicating the success or failure of the upload operation.</returns>
        Task<Operation<bool>> UploadAsync(string base64String, string filename);
    }
}
