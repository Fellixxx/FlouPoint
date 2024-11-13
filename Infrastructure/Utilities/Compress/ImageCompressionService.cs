namespace Infrastructure.Utilities.Compress
{
    using Application.Result;
    using Application.UseCases.ExternalServices;
    using Application.UseCases.ExternalServices.Resorces;
    using Application.UseCases.Repository;
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
    public class ImageCompressionService : IImageCompression
    {
        // Service for logging operations.
        private readonly ILogService _logService;
        private readonly IResorcesProvider _resourceProvider;
        private IResourceHandler _resourceHandler;
        private readonly List<string> _resourceKeys;

        /// <summary>
        /// Initializes a new instance of the ImageCompressionService class.
        /// </summary>
        /// <param name="logService">Service for logging operations.</param>
        public ImageCompressionService(ILogService logService, IResorcesProvider resourceProvider, IResourceHandler resourceHandler)
        {
            _logService = logService;
            _resourceProvider = resourceProvider;
            _resourceHandler = resourceHandler;
            _resourceKeys =
            [
                "FailedDataSizeCharacter",
                "FailedEmailInvalidFormat",
                "FailedAlreadyRegisteredEmail"
            ];
        }

        /// <summary>
        /// Compresses an image from a given stream.
        /// </summary>
        /// <param name="inputStream">The stream of the image to compress.</param>
        /// <param name="quality">The compression quality. Defaults to 75.</param>
        /// <returns>
        /// An OperationResult containing the compressed image stream if successful, 
        /// or error details if unsuccessful.
        /// </returns>
        public async Task<Operation<Stream>> CompressImage(Stream inputStream, int quality = 75)
        {
            try
            {
                var outputStream = new MemoryStream();
                using (var image = Image.Load(inputStream))
                {
                    var encoder = new JpegEncoder()
                    {
                        Quality = quality
                    };

                    image.Save(outputStream, encoder);
                }

                outputStream.Seek(0, SeekOrigin.Begin);
                await ResourceHandler.CreateAsync(_resourceProvider, _resourceKeys);
                var successCompressed = _resourceHandler.GetResource("SuccessCompressed");
                return Operation<Stream>.Success(outputStream, successCompressed); ;
            }
            catch (Exception ex)
            {
                Log log = Util.GetLogError(ex, inputStream, ActionType.CreateCustomOperation("Validate", "General validation operation."));
                Operation<string> result = await _logService.CreateLog(log);
                if (!result.IsSuccessful)
                {
                    return OperationBuilder<Stream>.FailUnexpected(Message.FailedToCompressImage);
                }
                return OperationBuilder<Stream>.FailUnexpected(Message.FailedToCompressImage);
            }
        }
    }
}
