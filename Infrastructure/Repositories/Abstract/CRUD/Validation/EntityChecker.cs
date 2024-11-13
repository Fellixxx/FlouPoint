﻿namespace Infrastructure.Repositories.Abstract.CRUD.Validation
{
    using Application.Result;
    using Application.UseCases.Repository.CRUD.Validation;
    using Domain.Interfaces.Entity;
    using Infrastructure.Utilities;
    using Persistence.Repositories;
    using Microsoft.EntityFrameworkCore;
    using Application.UseCases.Repository;
    using Infrastructure.ExternalServices.LogExternal;
    using Application.UseCases.ExternalServices.Resorces;

    /// <summary>
    /// Abstract class for validating the existence of an entity.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    public abstract class EntityChecker<T> : Repository<T>, IEntityChecker<T> where T : class, IEntity
    {
        private readonly IResorcesProvider _provider;
        private IResourceHandler _handler;
        private readonly List<string> _resourceKeys;

        /// <summary>
        /// Constructor with dependency injection.
        /// </summary>
        /// <param name="context">The database context.</param>
        protected EntityChecker(DbContext context, IResorcesProvider provider, IResourceHandler handler) : base(context)
        {
            _provider = provider;
            _handler = handler;
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
        public virtual async Task<Operation<T>> HasEntity(string id)
        {
            // Validate the provided ID
            if (id.Equals(string.Empty))
            {
                await ResourceHandler.CreateAsync(_provider, _resourceKeys);
                var failedNecesaryData = _handler.GetResource("FailedNecesaryData");
                return OperationBuilder<T>.FailBusiness(failedNecesaryData);
            }

            // Get the existing user from the repository
            IQueryable<T> entityRepo = await ReadFilter(e => e.Id.Equals(id));
            T? entityUnmodified = entityRepo?.FirstOrDefault();
            bool hasEntity = entityUnmodified is not null;
            await ResourceHandler.CreateAsync(_provider, _resourceKeys);
            if (!hasEntity)
            {
               
                var genericExistValidation = _handler.GetResource("GenericExistValidation");
                string messageExist = string.Format(genericExistValidation, typeof(T).Name);
                return OperationBuilder<T>.FailBusiness(messageExist);
            }
            var validationGlobalOkMessage = _handler.GetResource("ValidationGlobalOkMessage");
            return Operation<T>.Success(entityUnmodified, validationGlobalOkMessage);
        }


        /// <summary>
        /// Checks whether an entity with the provided ID exists.
        /// </summary>
        /// <param name="id">The ID of the entity to validate.</param>
        /// <returns>Operation result indicating whether the entity exists or not.</returns>
        public virtual async Task<Operation<T>> HasId(string id)
        {
            await ResourceHandler.CreateAsync(_provider, _resourceKeys);
            var failedNecesaryData = _handler.GetResource("FailedNecesaryData");
            if (string.IsNullOrWhiteSpace(id))
            {
                return OperationBuilder<T>.FailBusiness(failedNecesaryData);
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