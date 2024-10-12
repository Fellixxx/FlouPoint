namespace Infrastructure.Repositories.Abstract.Status
{
    using Application.Result;
    using Application.UseCases.ExternalServices;
    using Application.UseCases.Repository.Status.StatusChange;
    using Domain.Interfaces.Entity;
    using Domain.DTO.Log;
    using Infrastructure.Other;
    using Persistence.Repositories;
    using Microsoft.EntityFrameworkCore;
    using Domain.EnumType.OperationExecute;

    /// <summary>
    /// Repository class for managing the status of entities.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    public class StatusRepository<T> : Repository<T>, IStatusRepository where T : class, IEntity
    {
        private readonly ILogService _logService;

        /// <summary>
        /// Constructor with dependency injection.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="externalLogService">The external log service.</param>
        public StatusRepository(DbContext context, ILogService logService) : base(context)
        {
            _logService = logService;
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

                return OperationBuilder<bool>.FailureDatabase(Resource.FailedOccurredDataLayer);
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

                return OperationBuilder<bool>.FailureDatabase(Resource.FailedOccurredDataLayer);
            }
        }

        private async Task<OperationResult<T>> HasEntity(string id)
        {
            // Validate the provided ID
            if (string.IsNullOrWhiteSpace(id))
            {
                return OperationBuilder<T>.FailureBusinessValidation(Resource.FailedNecesaryData);
            }

            // Get the existing user from the repository
            IQueryable<T> entityRepo = await ReadFilter(e => e.Id.Equals(id));
            T? entityUnmodified = entityRepo?.FirstOrDefault();
            bool hasEntity = entityUnmodified is not null;
            if (!hasEntity)
            {
                string messageExist = string.Format(Resource.GenericExistValidation, typeof(T).Name);
                return OperationBuilder<T>.FailureBusinessValidation(messageExist);
            }

            // If the entity exists, return a success operation result
            return OperationResult<T>.Success(entityUnmodified, Resource.GlobalOkMessage);
        }
    }
}
