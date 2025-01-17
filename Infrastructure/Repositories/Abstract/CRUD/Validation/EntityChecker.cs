namespace Infrastructure.Repositories.Abstract.CRUD.Validation
{
    using Application.Result;
    using Application.UseCases.Repository.CRUD.Validation;
    using Domain.Interfaces.Entity;
    using Infrastructure.Utilities;
    using Persistence.Repositories;
    using Microsoft.EntityFrameworkCore;
    using Application.UseCases.ExternalServices.Resources;
    using Application.UseCases.ExternalServices.Resources.Provider;

    /// <summary>
    /// Abstract class responsible for validating the existence of an entity in the database.
    /// This serves as a base class for checking entity presence based on ID and providing operation results.
    /// </summary>
    /// <typeparam name = "T">The entity type that extends from class and implements IEntity interface.</typeparam>
    public abstract class EntityChecker<T> : Repository<T>, IEntityChecker<T> where T : class, IEntity
    {
        // Dependency for obtaining resource data
        private readonly IResourcesProvider _provider;
        // Handler for managing and retrieving resource strings
        private IResourceHandler _handler;
        // Key list for retrieving necessary resources
        protected List<string> _resourceKeys;
        /// <summary>
        /// Initializes a new instance of the EntityChecker class.
        /// </summary>
        /// <param name = "context">The database context for accessing the data store.</param>
        /// <param name = "provider">Resource provider for obtaining localized resources.</param>
        /// <param name = "handler">Handler for processing resource-related operations.</param>
        protected EntityChecker(DbContext context, IResourcesProvider provider, IResourceHandler handler) : base(context)
        {
            _provider = provider;
            _handler = handler;
            // Initialize the list with keys for required error/success messages
            SetResourceKeys();
        }
        protected virtual void SetResourceKeys()
        {
            // Initialize with predefined resource keys for successful operations.
            _resourceKeys = ["EntityCheckerFailedNecesaryData", "EntityCheckerValidation", "EntityCheckerSuccess"];
        }

        /// <summary>
        /// Validates whether an entity with the specified ID exists in the repository.
        /// </summary>
        /// <param name = "id">The ID of the entity whose presence needs to be validated.</param>
        /// <returns>An operation result object indicating the success or failure of the entity existence check.</returns>
        public virtual async Task<Operation<T>> HasEntity(string id)
        {
            // Check if the provided ID is empty
            if (id.Equals(string.Empty))
            {
                // Load necessary resources and return a failed operation if ID is empty
                await ResourceHandler.CreateAsync(_provider, _resourceKeys);
                var entityCheckerFailedNecesaryData = _handler.GetResource("EntityCheckerFailedNecesaryData");
                return OperationStrategy<T>.Fail(entityCheckerFailedNecesaryData, new BusinessStrategy<T>());
            }

            // Retrieves an entity from the data store using the ID filter
            IQueryable<T> entityRepo = await ReadFilter(e => e.Id.Equals(id));
            T? entityUnmodified = entityRepo?.FirstOrDefault();
            // Determines whether the entity was found
            bool hasEntity = entityUnmodified is not null;
            // Load resources again after attempting to find the entity
            await ResourceHandler.CreateAsync(_provider, _resourceKeys);
            // Case when the entity does not exist in the repository
            if (!hasEntity)
            {
                var entityCheckerValidation = _handler.GetResource("EntityCheckerValidation");
                var messageExist = string.Format(entityCheckerValidation, typeof(T).Name);
                return OperationStrategy<T>.Fail(messageExist, new BusinessStrategy<T>());
            }

            // Successful validation with a message confirming entity existence
            var entityCheckerSuccess = _handler.GetResource("EntityCheckerSuccess");
            return Operation<T>.Success(entityUnmodified, entityCheckerSuccess);
        }

        /// <summary>
        /// Validates the existence of an entity with the specified ID, ensuring the ID is not null or whitespace.
        /// </summary>
        /// <param name = "id">The ID of the entity to check for.</param>
        /// <returns>An operation result object representing whether the entity exists and if the ID format is valid.</returns>
        public virtual async Task<Operation<T>> HasId(string id)
        {
            // Preparing necessary resources
            await ResourceHandler.CreateAsync(_provider, _resourceKeys);
            var entityCheckerFailedNecesaryData = _handler.GetResource("EntityCheckerFailedNecesaryData");
            // Checks for null or whitespace in the provided ID
            if (string.IsNullOrWhiteSpace(id))
            {
                return OperationStrategy<T>.Fail(entityCheckerFailedNecesaryData, new BusinessStrategy<T>());
            }

            // Validates the GUID format of the given ID
            var result = GuidValidator.HasGuid(id);
            if (!result.IsSuccessful)
            {
                return result.AsType<T>();
            }

            // Final check for entity existence using the validated ID
            return await HasEntity(id);
        }
    }
}