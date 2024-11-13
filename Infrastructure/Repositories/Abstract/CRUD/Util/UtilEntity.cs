﻿namespace Infrastructure.Repositories.Abstract.CRUD.Util
{
    using Application.UseCases.Repository;
    using Application.UseCases.Repository.CRUD;
    using Domain.Interfaces.Entity;
    using Application.Result;
    using Application.UseCases.ExternalServices.Resorces;

    /// <summary>
    /// Utility class for entity validation operations.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    public class UtilEntity<T> : IUtilEntity<T> where T : class, IEntity
    {
        private readonly IResorcesProvider _provider;
        private IResourceHandler _handler;
        private readonly List<string> _resourceKeys;

        public UtilEntity(IResorcesProvider resourceProvider, IResourceHandler resourceHandler)
        {
            _provider = resourceProvider;
            _handler = resourceHandler;
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
        public async Task<Operation<T>> HasEntity(T entity)
        {
            await ResourceHandler.CreateAsync(_provider, _resourceKeys);
            var failedNecesaryData = _handler.GetResource("EntityFailedNecesaryData");
            if (entity is null)
            {
                // Return a failure result if the entity is null
                return OperationBuilder<T>.FailBusiness(failedNecesaryData);
            }

            // Return a success result if the entity is not null
            var utilGlobalOkMessage = _handler.GetResource("UtilGlobalOkMessage");
            return Operation<T>.Success(entity, utilGlobalOkMessage);
        }
    }
}
