namespace Infrastructure.Repositories.Abstract.CRUD.Create
{
    using Application.Result;
    using Application.UseCases.ExternalServices;
    using Application.UseCases.Repository.CRUD;
    using Domain.Interfaces.Entity;
    using Infrastructure.Other;
    using Persistence.Repositories;
    using Microsoft.EntityFrameworkCore;
    using Domain.DTO.Logging;
    using Domain.EnumType;
    using Infrastructure.Constants;
    using Application.UseCases.ExternalServices.Resources;

    /// <summary>
    /// Abstract repository class for creating a new entity.
    /// </summary>
    /// <typeparam name = "T">The entity type.</typeparam>
    public abstract class CreateRepository<T> : Repository<T>, ICreate<T> where T : class, IEntity
    {
        // Private fields for dependencies
        private readonly ILogService _logService; // Service for logging information and errors
        private readonly IUtilEntity<T> _utilEntity; // Utility service for entity-related operations
        private readonly IResourcesProvider _provider; // Provider for resource handling
        private IResourceHandler _handler; // Resource handler
        private readonly List<string> _resourceKeys; // List of resource keys used for localization or messages
        /// <summary>
        /// Constructor with dependency injection.
        /// </summary>
        /// <param name = "context">The database context.</param>
        /// <param name = "logService">The log service.</param>
        /// <param name = "utilEntity">Utility for entity operations.</param>
        /// <param name = "provider">Resources provider.</param>
        /// <param name = "handler">Resource handler.</param>
        public CreateRepository(DbContext context, ILogService logService, IUtilEntity<T> utilEntity, IResourcesProvider provider, IResourceHandler handler) : base(context)
        {
            _logService = logService;
            _utilEntity = utilEntity;
            _provider = provider;
            _handler = handler;
            _resourceKeys = ["SuccessfullyGeneric"]; // Initialize resource keys
        }

        /// <summary>
        /// Add a new entity to the database after performing validations.
        /// </summary>
        /// <param name = "entity">The entity to add.</param>
        /// <returns>A task representing the asynchronous operation with the added entity's ID.</returns>
        public new async Task<Operation<string>> Create(T entity)
        {
            try
            {
                // Check if the entity already exists
                Operation<T> hasEntity = await _utilEntity.HasEntity(entity);
                if (!hasEntity.IsSuccessful)
                {
                    return hasEntity.ConvertTo<string>(); // Convert unsuccessful operation result to generic type
                }

                // Validate the entity
                Operation<T> validationResult = await CreateEntity(entity);
                if (!validationResult.IsSuccessful)
                {
                    return validationResult.ConvertTo<string>(); // Convert unsuccessful result to generic type
                }

                // If validation is successful, add the entity to the database
                var addedEntityResult = await base.Create(validationResult.Data);
                // Handle resources for successful addition
                await ResourceHandler.CreateAsync(_provider, _resourceKeys);
                var successfullyGeneric = _handler.GetResource("SuccessfullyGeneric");
                // Create a success message and return the success result
                var successMessage = string.Format(successfullyGeneric, typeof(T).Name);
                return Operation<string>.Success(addedEntityResult, successMessage);
            }
            catch (Exception ex)
            {
                // Handle exceptions by logging errors
                Log log = Util.GetLogError(ex, entity, ActionType.Add);
                Operation<string> result = await _logService.CreateLog(log);
                if (!result.IsSuccessful)
                {
                    result.ConvertTo<string>(); // Convert unsuccessful log result to generic type
                }

                // Return failure operation in case of an exception and log the necessary info
                var strategy = new DatabaseStrategy<string>();
                var errorOccurredDataLayer = Message.ErrorOccurredDataLayer;
                return OperationStrategy<string>.Fail(errorOccurredDataLayer, strategy);
            }
        }

        /// <summary>
        /// Abstract method to validate an entity, must be overridden in derived classes.
        /// </summary>
        /// <param name = "entity">The entity to validate.</param>
        /// <returns>A task representing the asynchronous operation with the validation result.</returns>
        protected abstract Task<Operation<T>> CreateEntity(T entity);
    }
}