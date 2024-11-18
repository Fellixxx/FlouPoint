namespace Infrastructure.Repositories.Abstract.CRUD.Update
{
    using Application.Result;
    using Application.UseCases.ExternalServices;
    using Application.UseCases.Repository.CRUD;
    using Domain.Interfaces.Entity;
    using Infrastructure.Repositories.Abstract.CRUD.Validation;
    using Microsoft.EntityFrameworkCore;
    using Domain.DTO.Logging;
    using Domain.EnumType;
    using Infrastructure.Repositories.Abstract.CRUD.Query;
    using Infrastructure.Constants;
    using Application.UseCases.ExternalServices.Resources;
    using Application.UseCases.ExternalServices.Resources.Provider;

    /// <summary>
    /// Abstract repository class for updating an entity.
    /// This serves as a base class to provide updating functionality
    /// for more specialized repository implementations.
    /// </summary>
    /// <typeparam name = "T">The type of entity to be updated, must implement IEntity.</typeparam>
    public abstract class UpdateRepository<T> : EntityChecker<T>, IUpdate<T> where T : class, IEntity
    {
        /// <summary>
        /// Service for logging operations and errors.
        /// </summary>
        private readonly ILogService _logService;
        /// <summary>
        /// Utility service for entity operations.
        /// </summary>
        private readonly IUtilEntity<T> _utilEntity;
        /// <summary>
        /// Provides resource messages for feedback.
        /// </summary>
        private readonly IResourcesProvider _provider;
        /// <summary>
        /// Handler for managing resource-related operations.
        /// </summary>
        private IResourceHandler _handler;
        /// <summary>
        /// List of resource keys used for feedback messages.
        /// </summary>
        private readonly List<string> _resourceKeys;
        /// <summary>
        /// Constructor with dependency injection for dependencies.
        /// </summary>
        /// <param name = "context">The database context, typically an instance of DbContext.</param>
        /// <param name = "logService">Service for logging information and errors.</param>
        /// <param name = "utilEntity">Utility for entity verification and operations.</param>
        /// <param name = "provider">Service for providing resource strings and messages.</param>
        /// <param name = "handler">Handler for resource management.</param>
        protected UpdateRepository(DbContext context, ILogService logService, IUtilEntity<T> utilEntity, IResourcesProvider provider, IResourceHandler handler) : base(context, provider, handler)
        {
            _logService = logService;
            _utilEntity = utilEntity;
            _provider = provider;
            _handler = handler;
            _resourceKeys =
            [
                "UpdateSuccess",
                "UpdateEntitySearchSuccess"
            ];
        }

        /// <summary>
        /// Updates an entity asynchronously in the database.
        /// Ensures the entity exists and modifies it if validations are successful.
        /// </summary>
        /// <param name = "entity">The entity to be updated.</param>
        /// <returns>A task representing the asynchronous operation with the result of the update.</returns>
        public new async Task<Operation<bool>> Update(T entity)
        {
            try
            {
                // Check if the entity exists in the database
                Operation<T> hasEntity = await _utilEntity.HasEntity(entity);
                if (!hasEntity.IsSuccessful)
                {
                    return hasEntity.ConvertTo<bool>();
                }

                // Verify if the entity ID exists in the current context
                Operation<T> resultExist = await HasId(entity.Id);
                if (!resultExist.IsSuccessful)
                {
                    return resultExist.ConvertTo<bool>();
                }

                // Perform the update logic to modify the entity properties
                Operation<T> resultModifyEntity = await UpdateEntity(entity, resultExist.Data);
                if (!resultModifyEntity.IsSuccessful)
                {
                    return resultModifyEntity.ConvertTo<bool>();
                }

                // Update the entity in the database if the previous operations were successful
                bool updateResult = await base.Update(resultModifyEntity.Data);
                await ResourceHandler.CreateAsync(_provider, _resourceKeys);
                var updateSuccess = _handler.GetResource("UpdateSuccess");
                // Generate a custom success message
                string messageSuccess = string.Format(updateSuccess, typeof(T).Name);
                // Return a successful operation result, indicating the update was successful
                return Operation<bool>.Success(updateResult, messageSuccess);
            }
            catch (Exception ex)
            {
                // Log the error in case of exceptions and generate an error feedback
                Log log = Other.Util.GetLogError(ex, entity, ActionType.Modified);
                Operation<string> result = await _logService.CreateLog(log);
                if (!result.IsSuccessful)
                {
                    result.ConvertTo<bool>();
                }

                // Implement a strategy pattern for failure handling
                var strategy = new DatabaseStrategy<bool>();
                var errorOccurredDataLayer = Message.ErrorOccurredDataLayer;
                return OperationStrategy<bool>.Fail(errorOccurredDataLayer, strategy);
            }
        }

        /// <summary>
        /// Internal method to carry out the entity's update within the database context.
        /// Provides a default implementation that can be overridden by derived classes.
        /// </summary>
        /// <param name = "entityModified">The modified version of the entity.</param>
        /// <param name = "entityUnmodified">The original entity before modification.</param>
        /// <returns>A task result of the update operation, returned as an Operation of type T.</returns>
        public virtual async Task<Operation<T>> UpdateEntity(T entityModified, T entityUnmodified)
        {
            // Success message generation for a successfully modified entity
            
            await ResourceHandler.CreateAsync(_provider, _resourceKeys);
            var updateEntitySearchSuccess = _handler.GetResource("UpdateEntitySearchSuccess");
            string messageSuccessfully = string.Format(updateEntitySearchSuccess, typeof(T).Name);
            return Operation<T>.Success(entityModified, messageSuccessfully);
        }
    }
}