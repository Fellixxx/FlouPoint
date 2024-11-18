namespace Infrastructure.Repositories.Abstract.CRUD.Util
{
    using Application.UseCases.Repository.CRUD;
    using Domain.Interfaces.Entity;
    using Application.Result;
    using Application.UseCases.ExternalServices.Resources;
    using Application.UseCases.ExternalServices.Resources.Provider;
    using Application.UseCases.Repository.CRUD.Query;

    /// <summary>
    /// Utility class for performing validation operations on entities.
    /// </summary>
    /// <typeparam name = "T">The type of the entity.</typeparam>
    public class UtilEntity<T> : IUtilEntity<T> where T : class, IEntity
    {
        // Dependency injection for resource provider
        private readonly IResourcesProvider _provider;
        // Dependency injection for resource handler
        private IResourceHandler _handler;
        // Dependency injection for read exist entites
        IReadFilter<T> _readFilter;
        // List of resource keys used within the class
        private readonly List<string> _resourceKeys;
        /// <summary>
        /// Initializes a new instance of the <see cref = "UtilEntity{T}"/> class.
        /// </summary>
        /// <param name = "resourceProvider">The resource provider to be used for fetching resources.</param>
        /// <param name = "resourceHandler">The resource handler to handle resource logic.</param>
        public UtilEntity(IResourcesProvider resourceProvider, IResourceHandler resourceHandler, IReadFilter<T> readFilter)
        {
            _provider = resourceProvider;
            _handler = resourceHandler;
            _readFilter = readFilter;
            // Initialize resource keys with required keys
            _resourceKeys =
            [
                "UtilEntityFailedNecesaryData",
                "UtilEntitySuccess",
                "UtilEntityFailedUnique"
            ];
        }

        /// <summary>
        /// Determines if the specified entity exists by checking for null values.
        /// </summary>
        /// <param name = "entity">The entity to be checked.</param>
        /// <returns>A task that represents the asynchronous operation.
        ///  The task result contains an <see cref = "Operation{T}"/> indicating success or failure.</returns>
        public async Task<Operation<T>> HasEntity(T entity)
        {
            // Asynchronously create resources based on provided keys
            await ResourceHandler.CreateAsync(_provider, _resourceKeys);
            // Get specific resource message for missing necessary data
            var utilEntityFailedNecesaryData = _handler.GetResource("UtilEntityFailedNecesaryData");
            // Check if the entity is null and return a failure operation if so
            if (entity is null)
            {
                return OperationStrategy<T>.Fail(utilEntityFailedNecesaryData, new BusinessStrategy<T>());
            }

            // If the entity is not null, return a success operation with success message
            var utilEntitySuccess = _handler.GetResource("UtilEntitySuccess");
            return Operation<T>.Success(entity, utilEntitySuccess);
        }

        /// <summary>
        /// Determines if the specified entity exists by checking for null values.
        /// </summary>
        /// <param name = "entity">The entity to be checked.</param>
        /// <returns>A task that represents the asynchronous operation.
        ///  The task result contains an <see cref = "Operation{T}"/> indicating success or failure.</returns>
        public async Task<Operation<T>> HasUnique(T entity)
        {
            // Asynchronously create resources based on provided keys
            await ResourceHandler.CreateAsync(_provider, _resourceKeys);
            // Get specific resource message for missing necessary data
            var utilEntityFailedNecesaryData = _handler.GetResource("UtilEntityFailedUnique");
            // Check if the entity is null and return a failure operation if so
            var entityFound = await _readFilter.ReadFilter(E =>E.Id == entity.Id);
            if (
                entityFound.IsSuccessful && 
                entityFound is not null && 
                entityFound.Data is not null
                )
            {
                return OperationStrategy<T>.Fail(utilEntityFailedNecesaryData, new BusinessStrategy<T>());
            }

            // If the entity is not null, return a success operation with success message
            var utilEntitySuccess = _handler.GetResource("UtilEntitySuccess");
            return Operation<T>.Success(entity, utilEntitySuccess);
        }
    }
}