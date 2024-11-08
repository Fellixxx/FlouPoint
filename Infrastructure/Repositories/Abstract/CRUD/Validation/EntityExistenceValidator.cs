namespace Infrastructure.Repositories.Abstract.CRUD.Validation
{
    using Application.Result;
    using Application.UseCases.Repository.CRUD.Validation;
    using Domain.Interfaces.Entity;
    using Infrastructure.Utilities;
    using Persistence.Repositories;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Abstract class for validating the existence of an entity.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    public abstract class EntityExistenceValidator<T> : Repository<T>, IEntityExistenceValidator<T> where T : class, IEntity
    {
        /// <summary>
        /// Constructor with dependency injection.
        /// </summary>
        /// <param name="context">The database context.</param>
        protected EntityExistenceValidator(DbContext context) : base(context)
        {
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
                return OperationBuilder<T>.FailureBusinessValidation(Resource.FailedNecesaryData);
            }

            // Get the existing user from the repository
            IQueryable<T> entityRepo = await ReadFilter(e => e.Id.Equals(id));
            T? entityUnmodified = entityRepo?.FirstOrDefault();
            bool hasEntity = entityUnmodified is not null;
            if (!hasEntity)
            {
                string messageExist = string.Format(Resource.GenericExistValidation, typeof(T).Name);
                return OperationBuilder<T>.FailureBusinessValidation(messageExist);
            }

            // If the entity exists, return a success operation result
            return OperationResult<T>.Success(entityUnmodified, Resource.GlobalOkMessage);
        }


        /// <summary>
        /// Checks whether an entity with the provided ID exists.
        /// </summary>
        /// <param name="id">The ID of the entity to validate.</param>
        /// <returns>Operation result indicating whether the entity exists or not.</returns>
        public virtual async Task<OperationResult<T>> HasId(string id)
        {

            // Validate the provided ID
            if (string.IsNullOrWhiteSpace(id))
            {
                return OperationBuilder<T>.FailureBusinessValidation(Resource.FailedNecesaryData);
            }
            var result = GuidValidator.HasGuid(id);
            if (!result.IsSuccessful)
            {
                return result.AsType<T>();
            }

            // If the entity exists, return a success operation result
            return await HasEntity(id);
        }
    }
}
