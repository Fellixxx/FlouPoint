namespace Infrastructure.Repositories.Abstract.Status
{
    using Application.Result;
    using Application.UseCases.ExternalServices;
    using Application.UseCases.Repository.Status.StatusChange;
    using Domain.Interfaces.Entity;
    using Infrastructure.Other;
    using Persistence.Repositories;
    using Microsoft.EntityFrameworkCore;
    using Domain.DTO.Logging;
    using Domain.EnumType;
    using Infrastructure.Constants;
    using Application.UseCases.Repository;

    /// <summary>
    /// Repository class for managing the status of entities.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    public class StatusRepository<T> : Repository<T>, IStatusRepository where T : class, IEntity
    {
        private readonly ILogService _logService;
        private readonly IResourceProvider _resourceProvider;
        private IResourceHandler _resourceHandler;
        private readonly List<string> _resourceKeys;

        /// <summary>
        /// Constructor with dependency injection.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="externalLogService">The external log service.</param>
        public StatusRepository(DbContext context, ILogService logService, IResourceProvider resourceProvider, IResourceHandler resourceHandler) : base(context)
        {
            _logService = logService;
            _resourceProvider = resourceProvider;
            _resourceHandler = resourceHandler;
            _resourceKeys =
            [
                "SuccessfullyGenericActiveated",
                "StatusFailedNecesaryData",
                "GenericExistValidation"
            ];
        }

        public async Task<OperationResult<bool>> Activate(string id)
        {
            try
            {
                // Validate if the entity with the provided ID exists
                OperationResult<T> validationResult = await HasEntity(id);

                // If validation is not successful, return a failure operation result
                if (!validationResult.IsSuccessful)
                {
                    return validationResult.ToResultWithBoolType();
                }

                // If validation is successful, set the entity as active
                T? entity = validationResult.Data;
                entity.Active = true;

                // Update the entity in the database
                bool result = await Update(entity);

                await ResourceHandler.CreateAsync(_resourceProvider, _resourceKeys);
                var failedNecesaryData = _resourceHandler.GetResource("SuccessfullyGenericActiveated");
                // Custom success message
                string messageSuccess = string.Format(Resource.SuccessfullyGenericActiveated, typeof(T).Name);

                // Return a success operation result
                return OperationResult<bool>.Success(result, messageSuccess);
            }
            catch (Exception ex)
            {
                Log log = Util.GetLogError(ex, id, OperationExecute.Activate);
                OperationResult<string> result = await _logService.CreateLog(log);
                if (!result.IsSuccessful)
                {
                    result.ToResultWithBoolType();
                }

                return OperationBuilder<bool>.FailureDatabase(ExceptionMessages.FailedOccurredDataLayer);
            }
        }



        // This method deactivates an entity by setting its 'Active' status to false.
        public async Task<OperationResult<bool>> Deactivate(string id)
        {
            try
            {
                // Validate if the entity with the provided ID exists
                OperationResult<T> validationResult = await HasEntity(id);

                // If validation is not successful, return a failure operation result
                if (!validationResult.IsSuccessful)
                {
                    return validationResult.ToResultWithBoolType();
                }

                // If validation is successful, set the entity as inactive
                T? entity = validationResult.Data;
                entity.Active = false;

                // Update the entity in the database
                bool result = await Update(entity);

                // Custom success message
                string messageSuccess = string.Format(Resource.SuccessfullyGenericDisabled, typeof(T).Name);

                // Return a success operation result
                return OperationResult<bool>.Success(result, messageSuccess);
            }
            catch (Exception ex)
            {
                Log log = Util.GetLogError(ex, id, OperationExecute.Deactivate);
                OperationResult<string> result = await _logService.CreateLog(log);
                if (!result.IsSuccessful)
                {
                    result.ToResultWithBoolType();
                }

                return OperationBuilder<bool>.FailureDatabase(ExceptionMessages.FailedOccurredDataLayer);
            }
        }

        private async Task<OperationResult<T>> HasEntity(string id)
        {
            await ResourceHandler.CreateAsync(_resourceProvider, _resourceKeys);
            var statusFailedNecesaryData = _resourceHandler.GetResource("StatusFailedNecesaryData");
            // Validate the provided ID
            if (string.IsNullOrWhiteSpace(id))
            {
                return OperationBuilder<T>.FailureBusinessValidation(statusFailedNecesaryData);
            }

            // Get the existing user from the repository
            IQueryable<T> entityRepo = await ReadFilter(e => e.Id.Equals(id));
            T? entityUnmodified = entityRepo?.FirstOrDefault();
            bool hasEntity = entityUnmodified is not null;
            var genericExistValidation = _resourceHandler.GetResource("GenericExistValidation");
            if (!hasEntity)
            {
                string messageExist = string.Format(genericExistValidation, typeof(T).Name);
                return OperationBuilder<T>.FailureBusinessValidation(messageExist);
            }
            var statusGlobalOkMessage = _resourceHandler.GetResource("StatusGlobalOkMessage");
            // If the entity exists, return a success operation result
            return OperationResult<T>.Success(entityUnmodified, "statusGlobalOkMessage");
        }
    }
}
