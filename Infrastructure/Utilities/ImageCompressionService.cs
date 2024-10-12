namespace Infrastructure.Utilities
{
    using Application.Result;
    using Application.UseCases.ExternalServices;
    using Application.UseCases.Utilities;
    using Domain.DTO.Log;
    using Domain.EnumType.OperationExecute;
    using Infrastructure.Other;
    using SixLabors.ImageSharp.Formats.Jpeg;

    /// <summary>
    /// Provides functionality for compressing images.
    /// </summary>
    public class ImageCompressionService : IImageCompressionService
    {
        // Service for logging operations.
        private readonly ILogService _logService;

        /// <summary>
        /// Initializes a new instance of the ImageCompressionService class.
        /// </summary>
        /// <param name="logService">Service for logging operations.</param>
        public ImageCompressionService(ILogService logService)
        {
            _logService = logService;
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
                return OperationResult<Stream>.Success(outputStream, Resource.SuccessCompressed); ;
            }
            catch (Exception ex)
            {
                Log log = Util.GetLogError(ex, inputStream, OperationExecute.Validate);
                OperationResult<string> result = await _logService.CreateLog(log);
                if (!result.IsSuccessful)
                {
                    return OperationBuilder<Stream>.FailureUnexpectedError(Resource.FailedCompress);
                }
                return OperationBuilder<Stream>.FailureUnexpectedError(Resource.FailedCompress);
            }
        }
    }
}
