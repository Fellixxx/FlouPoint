namespace Infrastructure.Utilities.Images
{
    using Application.Result;
    using Application.UseCases.ExternalServices;
    using Application.UseCases.ExternalServices.Resources;
    using Application.UseCases.Utilities;
    using Domain.DTO.Logging;
    using Domain.EnumType;
    using Infrastructure.Constants;
    using Infrastructure.Other;
    using Infrastructure.Repositories;

    /// <summary>
    /// Provides functionality for managing images, including interacting with external services like Google Drive.
    /// </summary>
    public class ImageHandler : IImageHandler
    {
        // Service responsible for logging activities and errors.
        private readonly ILogService _logService;
        // Service responsible for image compression operations.
        private readonly IImageCompressor _imageCompressionService;
        // Provider for external resources.
        private readonly IResourcesProvider _provider;
        // Handler for resource-related operations.
        private IResourceHandler _handler;
        // List of resource keys used for error handling and messaging.
        private readonly List<string> _resourceKeys;
        /// <summary>
        /// Initializes a new instance of the ManagementImage class.
        /// </summary>
        /// <param name = "logService">Service responsible for logging operations.</param>
        /// <param name = "imageCompressionService">Service responsible for image compression.</param>
        /// <param name = "resourceProvider">Provider for accessing external resources.</param>
        /// <param name = "resourceHandler">Handler for managing resources.</param>
        public ImageHandler(ILogService logService, IImageCompressor imageCompressionService, IResourcesProvider resourceProvider, IResourceHandler resourceHandler)
        {
            _logService = logService;
            _imageCompressionService = imageCompressionService;
            _provider = resourceProvider;
            _handler = resourceHandler;
            _resourceKeys = new List<string>
            {
                "ImageSuccessfullyUpload",
                "ImageConvertSuccess"
            };
        }

        /// <summary>
        /// Asynchronously uploads a given image, represented as a base64 string, after converting and compressing it.
        /// </summary>
        /// <param name = "base64String">The base64 representation of the image.</param>
        /// <param name = "filename">The name of the file being uploaded.</param>
        /// <returns>A result indicating the success or failure of the operation.</returns>
        public async Task<Operation<bool>> UploadAsync(string base64String, string filename)
        {
            try
            {
                // Convert the base64 string to a stream.
                var resultStream = await ConvertBase64ToStream(base64String);
                if (!resultStream.IsSuccessful)
                {
                    return resultStream.ConvertTo<bool>();
                }

                var stream = resultStream.Data ?? new MemoryStream();
                // Compress the image stream.
                var resultCompress = await _imageCompressionService.CompressImage(stream);
                if (!resultCompress.IsSuccessful)
                {
                    return resultCompress.ConvertTo<bool>();
                }

                Stream streamCompress = resultCompress.Data ?? new MemoryStream();
                // Prepare the resources needed for the operation.
                await ResourceHandler.CreateAsync(_provider, _resourceKeys);
                // Retrieve success message resource.
                var successfullyUpload = _handler.GetResource("ImageSuccessfullyUpload");
                // Upload operation commented out; possibly uploads to Google Drive or another location.
                // var result = UploadFileAsync(streamCompress);
                // Return success operation with the success message.
                return Operation<bool>.Success(true, successfullyUpload);
            }
            catch (Exception ex)
            {
                // Log the exception and return a failure result.
                Log log = Util.GetLogError(ex, filename, ActionType.CreateCustomOperation("Validate", Message.ImageManagement.GeneralValidation));
                Operation<string> result = await _logService.CreateLog(log);
                // Define a failure message and strategy.
                var failedToUploadImage = Message.FailedToUploadImage;
                var strategy = new ExternalServiceStrategy<bool>();
                if (!result.IsSuccessful)
                {
                    return OperationStrategy<bool>.Fail(failedToUploadImage, strategy);
                }

                return OperationStrategy<bool>.Fail(failedToUploadImage, strategy);
            }
        }

        /// <summary>
        /// Converts a base64 string to a memory stream.
        /// </summary>
        /// <param name = "base64String">The base64 representation of the data.</param>
        /// <returns>A result containing the converted stream or an error.</returns>
        private async Task<Operation<Stream>> ConvertBase64ToStream(string base64String)
        {
            try
            {
                if (base64String == null)
                {
                    throw new Exception(Message.ImageManagement.ParameterIsNull);
                }

                // Removing the base64 prefix if it exists.
                if (base64String.Contains(','))
                {
                    base64String = base64String.Split(',')[1];
                }

                // Convert the base64 string into a byte array and then create a memory stream from it.
                byte[] bytes = Convert.FromBase64String(base64String);
                MemoryStream memoryStream = new MemoryStream(bytes);
                // Prepare resources needed for the success operation.
                await ResourceHandler.CreateAsync(_provider, _resourceKeys);
                // Retrieve success message resource.
                var imageGlobalOkMessage = _handler.GetResource("ImageConvertSuccess");
                // Return a successful operation with the memory stream.
                return Operation<Stream>.Success(memoryStream, imageGlobalOkMessage);
            }
            catch (Exception ex)
            {
                // Log the exception and return a failure result.
                Log log = Util.GetLogError(ex, base64String, ActionType.CreateCustomOperation("Validate", "General validation operation."));
                Operation<string> result = await _logService.CreateLog(log);
                // Define a failure message and strategy.
                var failedToUploadImage = Message.FailedToUploadImage;
                var strategy = new ExternalServiceStrategy<Stream>();
                if (!result.IsSuccessful)
                {
                    return OperationStrategy<Stream>.Fail(failedToUploadImage, strategy);
                }

                return OperationStrategy<Stream>.Fail(failedToUploadImage, strategy);
            }
        }
    }
}