﻿namespace Infrastructure.Utilities.Compress
{
    using Application.Result;
    using Application.UseCases.ExternalServices;
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
    public class ImageCompressionService : IImageCompressionService
    {
        // Service for logging operations.
        private readonly ILogService _logService;
        private readonly IResourceProvider _resourceProvider;
        private IResourceHandler _resourceHandler;
        private readonly List<string> _resourceKeys;

        /// <summary>
        /// Initializes a new instance of the ImageCompressionService class.
        /// </summary>
        /// <param name="logService">Service for logging operations.</param>
        public ImageCompressionService(ILogService logService, IResourceProvider resourceProvider, IResourceHandler resourceHandler)
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
        public async Task<OperationResult<Stream>> CompressImage(Stream inputStream, int quality = 75)
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
                return OperationResult<Stream>.Success(outputStream, successCompressed); ;
            }
            catch (Exception ex)
            {
                Log log = Util.GetLogError(ex, inputStream, OperationExecute.CreateCustomOperation("Validate", "General validation operation."));
                OperationResult<string> result = await _logService.CreateLog(log);
                if (!result.IsSuccessful)
                {
                    return OperationBuilder<Stream>.FailureUnexpectedError(ExceptionMessages.FailedCompress);
                }
                return OperationBuilder<Stream>.FailureUnexpectedError(ExceptionMessages.FailedCompress);
            }
        }
    }
}