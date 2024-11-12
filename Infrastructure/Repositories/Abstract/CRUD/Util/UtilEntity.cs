namespace Infrastructure.Repositories.Abstract.CRUD.Util
{
    using Application.UseCases.ExternalServices;
    using Application.UseCases.Repository;
    using Application.UseCases.Repository.CRUD;
    using Domain.Interfaces.Entity;
    using Application.Result;

    /// <summary>
    /// Utility class for entity validation operations.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    public class UtilEntity<T> : IUtilEntity<T> where T : class, IEntity
    {
        private readonly IResourceProvider _resourceProvider;
        private IResourceHandler _resourceHandler;
        private readonly List<string> _resourceKeys;

        public UtilEntity(IResourceProvider resourceProvider, IResourceHandler resourceHandler)
        {
            _resourceProvider = resourceProvider;
            _resourceHandler = resourceHandler;
            _resourceKeys =
            [
                "EntityFailedNecesaryData"
            ];
        }

        /// <summary>
        /// Check if an entity exists.
        /// </summary>
        /// <param name="entity">The entity to be checked.</param>
        /// <returns>An operation result indicating whether the entity exists or not.</returns>
        public async Task<OperationResult<T>> HasEntity(T entity)
        {
            await ResourceHandler.CreateAsync(_resourceProvider, _resourceKeys);
            var failedNecesaryData = _resourceHandler.GetResource("EntityFailedNecesaryData");
            if (entity is null)
            {
                // Return a failure result if the entity is null
                return OperationBuilder<T>.FailureBusinessValidation(failedNecesaryData);
            }

            // Return a success result if the entity is not null
            var utilGlobalOkMessage = _resourceHandler.GetResource("UtilGlobalOkMessage");
            return OperationResult<T>.Success(entity, utilGlobalOkMessage);
        }
    }
}
