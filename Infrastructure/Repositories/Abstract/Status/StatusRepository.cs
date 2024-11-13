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
    using Application.UseCases.ExternalServices.Resorces;

    /// <summary>
    /// Repository class for managing the status of entities.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    public class StatusRepository<T> : Repository<T>, IStatus where T : class, IEntity
    {
        private readonly ILogService _logService;
        private readonly IResorcesProvider _provider;
        private IResourceHandler _handler;
        private readonly List<string> _resourceKeys;

        /// <summary>
        /// Constructor with dependency injection.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="externalLogService">The external log service.</param>
        public StatusRepository(DbContext context, ILogService logService, IResorcesProvider resourceProvider, IResourceHandler resourceHandler) : base(context)
        {
            _logService = logService;
            _provider = resourceProvider;
            _handler = resourceHandler;
            _resourceKeys =
            [
                "SuccessfullyGenericActiveated",
                "StatusFailedNecesaryData",
                "GenericExistValidation"
            ];
        }

        public async Task<Operation<bool>> Activate(string id)
        {
            try
            {
                // Validate if the entity with the provided ID exists
                Operation<T> validationResult = await HasEntity(id);

                // If validation is not successful, return a failure operation result
                if (!validationResult.IsSuccessful)
                {
                    return validationResult.ConvertTo<bool>();
                }

                // If validation is successful, set the entity as active
                T? entity = validationResult.Data;
                entity.Active = true;

                // Update the entity in the database
                bool result = await Update(entity);

                await ResourceHandler.CreateAsync(_provider, _resourceKeys);
                var successfullyGenericActiveated = _handler.GetResource("SuccessfullyGenericActiveated");
                // Custom success message
                var messageSuccess = string.Format(successfullyGenericActiveated, typeof(T).Name);

                // Return a success operation result
                return Operation<bool>.Success(result, messageSuccess);
            }
            catch (Exception ex)
            {
                Log log = Util.GetLogError(ex, id, ActionType.Activate);
                Operation<string> result = await _logService.CreateLog(log);
                if (!result.IsSuccessful)
                {
                    result.ConvertTo<bool>();
                }

                return OperationBuilder<bool>.FailDatabase(Message.ErrorOccurredDataLayer);
            }
        }



        // This method deactivates an entity by setting its 'Active' status to false.
        public async Task<Operation<bool>> Deactivate(string id)
        {
            try
            {
                // Validate if the entity with the provided ID exists
                Operation<T> validationResult = await HasEntity(id);

                // If validation is not successful, return a failure operation result
                if (!validationResult.IsSuccessful)
                {
                    return validationResult.ConvertTo<bool>();
                }

                // If validation is successful, set the entity as inactive
                T? entity = validationResult.Data;
                entity.Active = false;

                // Update the entity in the database
                bool result = await Update(entity);
                await ResourceHandler.CreateAsync(_provider, _resourceKeys);
                var successfullyGenericActiveated = _handler.GetResource("StatusSuccessfullyGenericDisabled");
                // Custom success message
                string messageSuccess = string.Format(successfullyGenericActiveated, typeof(T).Name);

                // Return a success operation result
                return Operation<bool>.Success(result, messageSuccess);
            }
            catch (Exception ex)
            {
                Log log = Util.GetLogError(ex, id, ActionType.Deactivate);
                Operation<string> result = await _logService.CreateLog(log);
                if (!result.IsSuccessful)
                {
                    result.ConvertTo<bool>();
                }

                return OperationBuilder<bool>.FailDatabase(Message.ErrorOccurredDataLayer);
            }
        }

        private async Task<Operation<T>> HasEntity(string id)
        {
            await ResourceHandler.CreateAsync(_provider, _resourceKeys);
            var statusFailedNecesaryData = _handler.GetResource("StatusFailedNecesaryData");
            // Validate the provided ID
            if (string.IsNullOrWhiteSpace(id))
            {
                return OperationBuilder<T>.FailBusiness(statusFailedNecesaryData);
            }

            // Get the existing user from the repository
            IQueryable<T> entityRepo = await ReadFilter(e => e.Id.Equals(id));
            T? entityUnmodified = entityRepo?.FirstOrDefault();
            bool hasEntity = entityUnmodified is not null;
            var genericExistValidation = _handler.GetResource("GenericExistValidation");
            if (!hasEntity)
            {
                string messageExist = string.Format(genericExistValidation, typeof(T).Name);
                return OperationBuilder<T>.FailBusiness(messageExist);
            }
            var statusGlobalOkMessage = _handler.GetResource("StatusGlobalOkMessage");
            // If the entity exists, return a success operation result
            return Operation<T>.Success(entityUnmodified, "statusGlobalOkMessage");
        }
    }
}
