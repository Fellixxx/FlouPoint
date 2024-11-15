namespace Infrastructure.Repositories.Abstract.CRUD.Query.ReadId
{
    using Application.Result;
    using Application.UseCases.ExternalServices;
    using Application.UseCases.Repository.CRUD.Query;
    using Domain.Interfaces.Entity;
    using Infrastructure.Other;
    using Infrastructure.Repositories.Abstract.CRUD.Validation;
    using Microsoft.EntityFrameworkCore;
    using Domain.DTO.Logging;
    using Domain.EnumType;
    using Infrastructure.Constants;
    using Application.UseCases.ExternalServices.Resources;

    /// <summary>
    /// Abstract repository class that provides functionality to read an entity by its ID.
    /// </summary>
    /// <typeparam name = "T">The type of the entity.</typeparam>
    public abstract class ReadIdRepository<T> : EntityChecker<T>, IReadId<T> where T : class, IEntity
    {
        private readonly ILogService _logService; // Service used for logging errors and information
        private readonly IResourcesProvider _provider; // Provider for various resources
        private IResourceHandler _handler; // Handles resource retrieval
        private readonly List<string> _resourceKeys; // List of keys used for resource fetching
        /// <summary>
        /// Constructor that initializes the ReadIdRepository with necessary dependencies.
        /// </summary>
        /// <param name = "context">The database context.</param>
        /// <param name = "logService">The service used for logging.</param>
        /// <param name = "provider">The provider for resources.</param>
        /// <param name = "handler">The handler for managing resource operations.</param>
        protected ReadIdRepository(DbContext context, ILogService logService, IResourcesProvider provider, IResourceHandler handler) : base(context, provider, handler)
        {
            _logService = logService;
            _provider = provider;
            _resourceKeys = new List<string>
            {
                "ReadIdSuccess",
                "ReadByBearerSuccess"
            }; // Initialize keys for resources
        }

        /// <summary>
        /// Reads an entity from the database based on its ID.
        /// </summary>
        /// <param name = "id">The ID of the entity to read.</param>
        /// <returns>A task representing the asynchronous operation, which contains the operation result with the read entity.</returns>
        public async Task<Operation<T>> ReadId(string id)
        {
            try
            {
                // Validate the presence of the entity with given ID
                Operation<T> validationResult = await HasId(id);
                // Return failure result if validation fails
                if (!validationResult.IsSuccessful)
                {
                    return validationResult.ConvertTo<T>();
                }

                // Get the entity after successful validation
                T? entity = validationResult.Data;
                // Fetch relevant resources after validation
                await ResourceHandler.CreateAsync(_provider, _resourceKeys);
                // Retrieve success message resource
                var readIdSuccess = _handler.GetResource("ReadIdSuccess");
                // Return success operation with entity
                return Operation<T>.Success(entity, readIdSuccess);
            }
            catch (Exception ex)
            {
                // Log the exception for further analysis
                Log log = Util.GetLogError(ex, id, ActionType.GetUserById);
                Operation<string> result = await _logService.CreateLog(log);
                // Check if logging was successful, handle failure if not
                if (!result.IsSuccessful)
                {
                    result.ConvertTo<T>();
                }

                // Failure operation strategy for data layer errors
                var strategy = new DatabaseStrategy<T>();
                var errorOccurredDataLayer = Message.ErrorOccurredDataLayer;
                return OperationStrategy<T>.Fail(errorOccurredDataLayer, strategy);
            }
        }

        /// <summary>
        /// Reads an entity from the database based on the bearer token.
        /// </summary>
        /// <param name = "bearerToken">The JWT bearer token.</param>
        /// <returns>A task representing the asynchronous operation, which contains the operation result with the read entity.</returns>
        public async Task<Operation<T>> ReadByBearer(string bearerToken)
        {
            try
            {
                // Extract the payload from the JWT token
                var resultbearer = JwtHelper.ExtractJwtPayload(bearerToken);
                // Return failure if token extraction was unsuccessful
                if (!resultbearer.IsSuccessful)
                {
                    var strategy = new DatabaseStrategy<T>();
                    return OperationStrategy<T>.Fail(resultbearer.Message, strategy);
                }

                // Validate the presence of the entity using extracted ID from token
                Operation<T> validationResult = await HasId(resultbearer.Data);
                // Return failure if validation fails
                if (!validationResult.IsSuccessful)
                {
                    return validationResult.ConvertTo<T>();
                }

                // Get the entity after successful validation
                T? entity = validationResult.Data;
                // Fetch relevant resources after validation
                await ResourceHandler.CreateAsync(_provider, _resourceKeys);
                // Retrieve success message resource
                var readByBearerSuccess = _handler.GetResource("ReadByBearerSuccess");
                // Return success operation with entity
                return Operation<T>.Success(entity, readByBearerSuccess);
            }
            catch (Exception ex)
            {
                // Log the exception for further analysis
                var log = Util.GetLogError(ex, bearerToken, ActionType.GetUserById);
                var result = await _logService.CreateLog(log);
                // Check if logging was successful, handle failure if not
                if (!result.IsSuccessful)
                {
                    result.ConvertTo<T>();
                }

                // Return failure operation result for database issues
                var strategy = new DatabaseStrategy<T>();
                var errorOccurredDataLayer = Message.ErrorOccurredDataLayer;
                return OperationStrategy<T>.Fail(errorOccurredDataLayer, strategy);
            }
        }
    }
}