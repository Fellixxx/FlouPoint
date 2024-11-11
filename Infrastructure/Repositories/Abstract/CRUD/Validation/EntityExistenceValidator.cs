﻿namespace Infrastructure.Repositories.Abstract.CRUD.Validation
{
    using Application.Result;
    using Application.UseCases.Repository.CRUD.Validation;
    using Domain.Interfaces.Entity;
    using Infrastructure.Utilities;
    using Persistence.Repositories;
    using Microsoft.EntityFrameworkCore;
    using Application.UseCases.ExternalServices;
    using Application.UseCases.Repository;
    using Infrastructure.ExternalServices.LogExternal;

    /// <summary>
    /// Abstract class for validating the existence of an entity.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    public abstract class EntityExistenceValidator<T> : Repository<T>, IEntityExistenceValidator<T> where T : class, IEntity
    {
        private readonly IResourceProvider _resourceProvider;
        private IResourceHandler _resourceHandler;
        private readonly List<string> _resourceKeys;

        /// <summary>
        /// Constructor with dependency injection.
        /// </summary>
        /// <param name="context">The database context.</param>
        protected EntityExistenceValidator(DbContext context, IResourceProvider resourceProvider) : base(context)
        {
            _resourceProvider = resourceProvider;
            _resourceKeys =
            [
                "FailedNecesaryData",
                "GenericExistValidation",
                "ValidationGlobalOkMessage"
            ];
        }

        /// <summary>
        /// Checks whether an entity with the provided ID exists.
        /// </summary>
        /// <param name="id">The ID of the entity to validate.</param>
        /// <returns>Operation result indicating whether the entity exists or not.</returns>
        public virtual async Task<OperationResult<T>> HasEntity(string id)
        {
            // Validate the provided ID
            if (id.Equals(string.Empty))
            {
                await ResourceHandler.CreateAsync(_resourceProvider, _resourceKeys);
                var failedNecesaryData = _resourceHandler.GetResource("FailedNecesaryData");
                return OperationBuilder<T>.FailureBusinessValidation(failedNecesaryData);
            }

            // Get the existing user from the repository
            IQueryable<T> entityRepo = await ReadFilter(e => e.Id.Equals(id));
            T? entityUnmodified = entityRepo?.FirstOrDefault();
            bool hasEntity = entityUnmodified is not null;
            await ResourceHandler.CreateAsync(_resourceProvider, _resourceKeys);
            if (!hasEntity)
            {
               
                var genericExistValidation = _resourceHandler.GetResource("GenericExistValidation");
                string messageExist = string.Format(genericExistValidation, typeof(T).Name);
                return OperationBuilder<T>.FailureBusinessValidation(messageExist);
            }
            var validationGlobalOkMessage = _resourceHandler.GetResource("ValidationGlobalOkMessage");
            return OperationResult<T>.Success(entityUnmodified, validationGlobalOkMessage);
        }


        /// <summary>
        /// Checks whether an entity with the provided ID exists.
        /// </summary>
        /// <param name="id">The ID of the entity to validate.</param>
        /// <returns>Operation result indicating whether the entity exists or not.</returns>
        public virtual async Task<OperationResult<T>> HasId(string id)
        {
            await ResourceHandler.CreateAsync(_resourceProvider, _resourceKeys);
            var failedNecesaryData = _resourceHandler.GetResource("FailedNecesaryData");
            if (string.IsNullOrWhiteSpace(id))
            {
                return OperationBuilder<T>.FailureBusinessValidation(failedNecesaryData);
            }
            var result = GuidValidator.HasGuid(id);
            if (!result.IsSuccessful)
            {
                return result.AsType<T>();
            }
            return await HasEntity(id);
        }
    }
}
