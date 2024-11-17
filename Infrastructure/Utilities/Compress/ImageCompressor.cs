namespace Infrastructure.Utilities.Compress
{
    using Application.Result;
    using Application.UseCases.ExternalServices;
    using Application.UseCases.ExternalServices.Resources;
    using Application.UseCases.ExternalServices.Resources.Provider;
    using Application.UseCases.Utilities;
    using Domain.DTO.Logging;
    using Domain.EnumType;
    using Infrastructure.Constants;
    using Infrastructure.Other;
    using Infrastructure.Repositories;
    using SixLabors.ImageSharp.Formats.Jpeg;

    /// <summary>
    /// Provides functionality for compressing images.
    /// </summary>
    public class ImageCompressor : IImageCompressor
    {
        // Service for logging operations.
        private readonly ILogService _logService;
        // Provider for fetching resources.
        private readonly IResourcesProvider _provider;
        // Handler for managing resources.
        private IResourceHandler _handler;
        // List of keys to access specific resource strings.
        private readonly List<string> _resourceKeys;
        /// <summary>
        /// Initializes a new instance of the ImageCompressionService class.
        /// </summary>
        /// <param name = "logService">Service for logging operations.</param>
        /// <param name = "resourceProvider">Provider for fetching resources.</param>
        /// <param name = "resourceHandler">Handler for managing resources.</param>
        public ImageCompressor(ILogService logService, IResourcesProvider resourceProvider, IResourceHandler resourceHandler)
        {
            _logService = logService;
            _provider = resourceProvider;
            _handler = resourceHandler;
            // Initialize predefined keys for resource messages.
            _resourceKeys = ["CompressedSuccess"];
        }

        /// <summary>
        /// Compresses an image from a given stream.
        /// </summary>
        /// <param name = "inputStream">The stream of the image to compress.</param>
        /// <param name = "quality">The compression quality. Defaults to 75 (range: 1-100).</param>
        /// <returns>
        /// An OperationResult containing the compressed image stream if successful, 
        /// or error details if unsuccessful.
        /// </returns>
        public async Task<Operation<Stream>> CompressImage(Stream inputStream, int quality = 75)
        {
            try
            {
                // Create a new memory stream to store the compressed image.
                var outputStream = new MemoryStream();
                // Load the image from the input stream using ImageSharp.
                using (var image = Image.Load(inputStream))
                {
                    // Configure JPEG encoder with the specified quality setting.
                    var encoder = new JpegEncoder()
                    {
                        Quality = quality
                    };
                    // Save the compressed image to the output stream.
                    image.Save(outputStream, encoder);
                }

                // Reset the output stream position to the beginning.
                outputStream.Seek(0, SeekOrigin.Begin);
                // Asynchronously create resources using the resource handler.
                await ResourceHandler.CreateAsync(_provider, _resourceKeys);
                var compressedSuccess = _handler.GetResource("CompressedSuccess");
                // Return a successful operation result with the compressed image stream.
                return Operation<Stream>.Success(outputStream, compressedSuccess);
            }
            catch (Exception ex)
            {
                // Log the error with details of the exception and input stream.
                Log log = Util.GetLogError(ex, inputStream, ActionType.CreateCustomOperation("Validate", "General validation operation."));
                Operation<string> result = await _logService.CreateLog(log);
                // Define a strategy for handling unexpected errors.
                var strategy = new UnexpectedErrorStrategy<Stream>();
                // Define the message to be returned on failure.
                var message = Message.FailedToCompressImage;
                // Return a failed operation result if log creation was unsuccessful.
                if (!result.IsSuccessful)
                {
                    return OperationStrategy<Stream>.Fail(message, strategy);
                }

                // Return a failed operation result for other failures.
                return OperationStrategy<Stream>.Fail(message, strategy);
            }
        }
    }
}