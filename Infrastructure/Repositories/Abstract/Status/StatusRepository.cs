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
    using Application.UseCases.ExternalServices.Resources;

    /// <summary>
    /// Repository class for managing the status of entities.
    /// </summary>
    /// <typeparam name = "T">The entity type.</typeparam>
    public class StatusRepository<T> : Repository<T>, IStatus where T : class, IEntity
    {
        private readonly ILogService _logService;
        private readonly IResourcesProvider _provider;
        private readonly IResourceHandler _handler;
        private readonly List<string> _resourceKeys;
        /// <summary>
        /// Initializes a new instance of the <see cref = "StatusRepository{T}"/> class with specified dependencies.
        /// </summary>
        /// <param name = "context">The database context to be used.</param>
        /// <param name = "logService">Service for logging actions and errors.</param>
        /// <param name = "resourceProvider">Provides resource keys for localization or customization.</param>
        /// <param name = "resourceHandler">Handles resource retrieval using the provided keys.</param>
        public StatusRepository(DbContext context, ILogService logService, IResourcesProvider resourceProvider, IResourceHandler resourceHandler) : base(context)
        {
            _logService = logService;
            _provider = resourceProvider;
            _handler = resourceHandler;
            _resourceKeys = new List<string>
            {
                "SuccessfullyGenericActiveated",
                "StatusFailedNecesaryData",
                "GenericExistValidation",
                "StatusSuccessfullyGenericDisabled"
            };
        }

        /// <summary>
        /// Activates an entity by setting its 'Active' status to true.
        /// </summary>
        /// <param name = "id">The ID of the entity to activate.</param>
        /// <returns>An operation result indicating success or failure of the operation.</returns>
        public async Task<Operation<bool>> Activate(string id)
        {
            try
            {
                // Validate the presence of the entity with the provided ID.
                Operation<T> validationResult = await HasEntity(id);
                if (!validationResult.IsSuccessful)
                {
                    return validationResult.ConvertTo<bool>();
                }

                // Mark the entity as active.
                T? entity = validationResult.Data;
                entity.Active = true;
                // Update the entity in the database.
                bool result = await Update(entity);
                await ResourceHandler.CreateAsync(_provider, _resourceKeys);
                var successfullyGenericActiveated = _handler.GetResource("SuccessfullyGenericActiveated");
                // Generate success message.
                var messageSuccess = string.Format(successfullyGenericActiveated, typeof(T).Name);
                // Return the success operation result.
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

                var strategy = new DatabaseStrategy<bool>();
                var errorOccurredDataLayer = Message.ErrorOccurredDataLayer;
                return OperationStrategy<bool>.Fail(errorOccurredDataLayer, strategy);
            }
        }

        /// <summary>
        /// Deactivates an entity by setting its 'Active' status to false.
        /// </summary>
        /// <param name = "id">The ID of the entity to deactivate.</param>
        /// <returns>An operation result indicating success or failure of the operation.</returns>
        public async Task<Operation<bool>> Deactivate(string id)
        {
            try
            {
                // Validate the presence of the entity with the provided ID.
                Operation<T> validationResult = await HasEntity(id);
                if (!validationResult.IsSuccessful)
                {
                    return validationResult.ConvertTo<bool>();
                }

                // Mark the entity as inactive.
                T? entity = validationResult.Data;
                entity.Active = false;
                // Update the entity in the database.
                bool result = await Update(entity);
                await ResourceHandler.CreateAsync(_provider, _resourceKeys);
                var successfullyGenericActiveated = _handler.GetResource("StatusSuccessfullyGenericDisabled");
                // Generate success message.
                string messageSuccess = string.Format(successfullyGenericActiveated, typeof(T).Name);
                // Return the success operation result.
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

                var strategy = new DatabaseStrategy<bool>();
                var errorOccurredDataLayer = Message.ErrorOccurredDataLayer;
                return OperationStrategy<bool>.Fail(errorOccurredDataLayer, strategy);
            }
        }

        /// <summary>
        /// Validates if an entity with the specified ID exists.
        /// </summary>
        /// <param name = "id">The ID of the entity to validate.</param>
        /// <returns>An operation result indicating the existence and success or failure of the validation.</returns>
        private async Task<Operation<T>> HasEntity(string id)
        {
            await ResourceHandler.CreateAsync(_provider, _resourceKeys);
            var statusFailedNecesaryData = _handler.GetResource("StatusFailedNecesaryData");
            // Validate the ID.
            if (string.IsNullOrWhiteSpace(id))
            {
                return OperationStrategy<T>.Fail(statusFailedNecesaryData, new BusinessStrategy<T>());
            }

            // Retrieve the entity from the repository.
            IQueryable<T> entityRepo = await ReadFilter(e => e.Id.Equals(id));
            T? entityUnmodified = entityRepo?.FirstOrDefault();
            bool hasEntity = entityUnmodified is not null;
            var genericExistValidation = _handler.GetResource("GenericExistValidation");
            if (!hasEntity)
            {
                string messageExist = string.Format(genericExistValidation, typeof(T).Name);
                return OperationStrategy<T>.Fail(messageExist, new BusinessStrategy<T>());
            }

            var statusGlobalOkMessage = _handler.GetResource("StatusGlobalOkMessage");
            // Return the success operation result indicating the entity exists.
            return Operation<T>.Success(entityUnmodified, "statusGlobalOkMessage");
        }
    }
}