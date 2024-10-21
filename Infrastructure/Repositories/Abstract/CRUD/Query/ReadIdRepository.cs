namespace Infrastructure.Repositories.Abstract.CRUD.Query
{
    using Application.Result;
    using Application.UseCases.ExternalServices;
    using Application.UseCases.Repository.CRUD.Query;
    using Domain.Interfaces.Entity;
    using Domain.DTO.Log;
    using Infrastructure.Other;
    using Infrastructure.Repositories.Abstract.CRUD.Validation;
    using Microsoft.EntityFrameworkCore;
    using Domain.EnumType.OperationExecute;

    /// <summary>
    /// Abstract repository class for reading an entity by its ID.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    public abstract class ReadIdRepository<T> : EntityExistenceValidator<T>, IReadIdRepository<T> where T : class, IEntity
    {
        private readonly ILogService _logService;

        /// <summary>
        /// Constructor with dependency injection.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="logService">The log service.</param>
        protected ReadIdRepository(DbContext context, ILogService logService) : base(context)
        {
            _logService = logService;
        }

        /// <summary>
        /// Read an entity from the database based on its ID.
        /// </summary>
        /// <param name="id">The ID of the entity to read.</param>
        /// <returns>A task representing the asynchronous operation with the read entity.</returns>
        public async Task<OperationResult<T>> ReadId(string id)
        {
            try
            {
                // Get entities from the database based on the provided filter expression
                OperationResult<T> validationResult = await HasId(id);

                // If validation is not successful, return a failure operation result
                if (!validationResult.IsSuccessful)
                {
                    return validationResult.ToResultWithGenericType();
                }

                T? entity = validationResult.Data;
                // Return a success operation result
                string messageSuccessfully = Resource.SuccessfullyFind;
                return OperationResult<T>.Success(entity, messageSuccessfully);
            }
            catch (Exception ex)
            {
                // Create a log entry for the exception
                Log log = Util.GetLogError(ex, id, OperationExecute.GetUserById);
                OperationResult<string> result = await _logService.CreateLog(log);

                // Handle logging failure
                if (!result.IsSuccessful)
                {
                    result.ToResultWithXType<T>();
                }

                // Return a failure operation result for database issues
                return OperationBuilder<T>.FailureDatabase(Resource.FailedOccurredDataLayer);
            }
        }

        /// <summary>
        /// Read an entity from the database based on its ID.
        /// </summary>
        /// <param name="id">The ID of the entity to read.</param>
        /// <returns>A task representing the asynchronous operation with the read entity.</returns>
        public async Task<OperationResult<T>> ReadByBearer(string bearerToken)
        {
            try
            {
                var resultbearer = JwtHelper.ExtractJwtPayload(bearerToken);

                if (!resultbearer.IsSuccessful)
                {
                    return OperationBuilder<T>.FailureDatabase(resultbearer.Message);
                }

                // Get entities from the database based on the provided filter expression
                OperationResult<T> validationResult = await HasId(resultbearer.Data);

                // If validation is not successful, return a failure operation result
                if (!validationResult.IsSuccessful)
                {
                    return validationResult.ToResultWithGenericType();
                }

                T? entity = validationResult.Data;
                // Return a success operation result
                string messageSuccessfully = Resource.SuccessfullyFind;
                return OperationResult<T>.Success(entity, messageSuccessfully);
            }
            catch (Exception ex)
            {
                // Create a log entry for the exception
                Log log = Util.GetLogError(ex, bearerToken, OperationExecute.GetUserById);
                OperationResult<string> result = await _logService.CreateLog(log);

                // Handle logging failure
                if (!result.IsSuccessful)
                {
                    result.ToResultWithXType<T>();
                }

                // Return a failure operation result for database issues
                return OperationBuilder<T>.FailureDatabase(Resource.FailedOccurredDataLayer);
            }
        }
    }
}
