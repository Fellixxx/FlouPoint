namespace Application.UseCases.Utilities
{
    using Application.Result;

    /// <summary>
    /// Defines the contract for image compression services.
    /// </summary>
    public interface IImageCompression
    {
        /// <summary>
        /// Compresses an image from the provided stream.
        /// </summary>
        /// <param name="inputStream">The stream containing the image to be compressed.</param>
        /// <param name="quality">The compression quality, ranging from 0 to 100. Defaults to 75.</param>
        /// <returns>
        /// An OperationResult containing the compressed image stream if successful, 
        /// or error details if unsuccessful.
        /// </returns>
        Task<Operation<Stream>> CompressImage(Stream inputStream, int quality = 75);
    }
}