﻿namespace Infrastructure.Repositories.Abstract.CRUD.Query
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
    using Application.UseCases.Repository;

    /// <summary>
    /// Abstract repository class for reading an entity by its ID.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    public abstract class ReadIdRepository<T> : EntityExistenceValidator<T>, IReadIdRepository<T> where T : class, IEntity
    {
        private readonly ILogService _logService;
        private readonly IResourceProvider _resourceProvider;
        private IResourceHandler _resourceHandler;
        private readonly List<string> _resourceKeys;

        /// <summary>
        /// Constructor with dependency injection.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="logService">The log service.</param>
        protected ReadIdRepository(DbContext context, ILogService logService, IResourceProvider resourceProvider) : base(context, resourceProvider)
        {
            _logService = logService;
            _resourceProvider = resourceProvider;
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
        public async Task<OperationResult<T>> ReadId(string id)
        {
            try
            {
                OperationResult<T> validationResult = await HasId(id);
                if (!validationResult.IsSuccessful)
                {
                    return validationResult.ToResultWithGenericType();
                }
                T? entity = validationResult.Data;
                await ResourceHandler.CreateAsync(_resourceProvider, _resourceKeys);
                var successfullyFind = _resourceHandler.GetResource("SuccessfullySearchGeneric");
                return OperationResult<T>.Success(entity, successfullyFind);
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
                return OperationBuilder<T>.FailureDatabase(ExceptionMessages.FailedOccurredDataLayer);
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
                await ResourceHandler.CreateAsync(_resourceProvider, _resourceKeys);
                var successfullyFind = _resourceHandler.GetResource("SuccessfullyFind");
                return OperationResult<T>.Success(entity, successfullyFind);
            }
            catch (Exception ex)
            {
                // Create a log entry for the exception
                var log = Util.GetLogError(ex, bearerToken, OperationExecute.GetUserById);
                var result = await _logService.CreateLog(log);

                // Handle logging failure
                if (!result.IsSuccessful)
                {
                    result.ToResultWithXType<T>();
                }

                // Return a failure operation result for database issues
                return OperationBuilder<T>.FailureDatabase(ExceptionMessages.FailedOccurredDataLayer);
            }
        }
    }
}
