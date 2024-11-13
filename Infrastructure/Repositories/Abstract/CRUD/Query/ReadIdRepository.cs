namespace Infrastructure.Repositories.Abstract.CRUD.Query
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
    /// Abstract repository class for reading an entity by its ID.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    public abstract class ReadIdRepository<T> : EntityChecker<T>, IReadId<T> where T : class, IEntity
    {
        private readonly ILogService _logService;
        private readonly IResourcesProvider _provider;
        private IResourceHandler _handler;
        private readonly List<string> _resourceKeys;

        /// <summary>
        /// Constructor with dependency injection.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="logService">The log service.</param>
        protected ReadIdRepository(DbContext context, ILogService logService, IResourcesProvider provider, IResourceHandler handler) : base(context, provider, handler)
        {
            _logService = logService;
            _provider = provider;
            _resourceKeys =
            [
                "SuccessfullyFind"
            ];
        }

        /// <summary>
        /// Read an entity from the database based on its ID.
        /// </summary>
        /// <param name="id">The ID of the entity to read.</param>
        /// <returns>A task representing the asynchronous operation with the read entity.</returns>
        public async Task<Operation<T>> ReadId(string id)
        {
            try
            {
                Operation<T> validationResult = await HasId(id);
                if (!validationResult.IsSuccessful)
                {
                    return validationResult.ConvertTo<T>();
                }
                T? entity = validationResult.Data;
                await ResourceHandler.CreateAsync(_provider, _resourceKeys);
                var successfullyFind = _handler.GetResource("SuccessfullySearchGeneric");
                return Operation<T>.Success(entity, successfullyFind);
            }
            catch (Exception ex)
            {
                // Create a log entry for the exception
                Log log = Util.GetLogError(ex, id, ActionType.GetUserById);
                Operation<string> result = await _logService.CreateLog(log);

                // Handle logging failure
                if (!result.IsSuccessful)
                {
                    result.ConvertTo<T>();
                }

                var strategy = new DatabaseStrategy<T>();
                var errorOccurredDataLayer = Message.ErrorOccurredDataLayer;
                return OperationStrategy<T>.Fail(errorOccurredDataLayer, strategy);
            }
        }

        /// <summary>
        /// Read an entity from the database based on its ID.
        /// </summary>
        /// <param name="id">The ID of the entity to read.</param>
        /// <returns>A task representing the asynchronous operation with the read entity.</returns>
        public async Task<Operation<T>> ReadByBearer(string bearerToken)
        {
            try
            {
                var resultbearer = JwtHelper.ExtractJwtPayload(bearerToken);

                if (!resultbearer.IsSuccessful)
                {
                    var strategy = new DatabaseStrategy<T>();
                    return OperationStrategy<T>.Fail(resultbearer.Message, strategy);
                }

                // Get entities from the database based on the provided filter expression
                Operation<T> validationResult = await HasId(resultbearer.Data);

                // If validation is not successful, return a failure operation result
                if (!validationResult.IsSuccessful)
                {
                    return validationResult.ConvertTo<T>();
                }

                T? entity = validationResult.Data;
                await ResourceHandler.CreateAsync(_provider, _resourceKeys);
                var successfullyFind = _handler.GetResource("SuccessfullyFind");
                return Operation<T>.Success(entity, successfullyFind);
            }
            catch (Exception ex)
            {
                // Create a log entry for the exception
                var log = Util.GetLogError(ex, bearerToken, ActionType.GetUserById);
                var result = await _logService.CreateLog(log);

                // Handle logging failure
                if (!result.IsSuccessful)
                {
                    result.ConvertTo<T>();
                }

                // Return a failure operation result for database issues
                var strategy = new DatabaseStrategy<T>();
                var errorOccurredDataLayer = Message.ErrorOccurredDataLayer;
                return OperationStrategy<T>.Fail(errorOccurredDataLayer, strategy);
            }
        }
    }
}
