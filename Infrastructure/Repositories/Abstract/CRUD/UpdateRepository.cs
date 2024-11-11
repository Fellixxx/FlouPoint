namespace Infrastructure.Repositories.Abstract.CRUD
{
    using Application.Result;
    using Application.UseCases.ExternalServices;
    using Application.UseCases.Repository.CRUD;
    using Domain.Interfaces.Entity;
    using Infrastructure.Other;
    using Infrastructure.Repositories.Abstract.CRUD.Validation;
    using Microsoft.EntityFrameworkCore;
    using Domain.DTO.Logging;
    using Domain.EnumType;
    using Infrastructure.Repositories.Abstract.CRUD.Query;
    using Infrastructure.Constants;
    using Application.UseCases.Repository;

    /// <summary>
    /// Abstract repository class for updating an entity.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    public abstract class UpdateRepository<T> : EntityExistenceValidator<T>, IUpdateRepository<T> where T : class, IEntity
    {
        private readonly ILogService _logService;
        private readonly IUtilEntity<T> _utilEntity;

        /// <summary>
        /// Constructor with dependency injection.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="logService">The log service.</param>
        protected UpdateRepository(DbContext context, ILogService logService, IUtilEntity<T> utilEntity, IResourceProvider resourceProvider, IResourceHandler resourceHandler) : base(context, resourceProvider, resourceHandler)
        {
            _logService = logService;
            _utilEntity = utilEntity;
        }

        /// <summary>
        /// Updates an entity in the database.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>A task representing the asynchronous operation with the update result.</returns>
        public new async Task<OperationResult<bool>> Update(T entity)
        {
            try
            {
                OperationResult<T> hasEntity = await _utilEntity.HasEntity(entity);
                if (!hasEntity.IsSuccessful)
                {
                    return hasEntity.ToResultWithBoolType();
                }

                OperationResult<T> resultExist = await HasId(entity.Id);
                if (!resultExist.IsSuccessful)
                {
                    return resultExist.ToResultWithBoolType();
                }

                OperationResult<T> resultModifyEntity = await UpdateEntity(entity, resultExist.Data);
                if (!resultModifyEntity.IsSuccessful)
                {
                    return resultModifyEntity.ToResultWithBoolType();
                }

                // If validation is successful, update the entity in the database
                bool updateResult = await base.Update(resultModifyEntity.Data);

                // Custom success message
                string messageSuccess = string.Format(Resource.SuccessfullyGenericUpdated, typeof(T).Name);

                // Return a success operation result
                return OperationResult<bool>.Success(updateResult, messageSuccess);

            }
            catch (Exception ex)
            {
                Log log = Util.GetLogError(ex, entity, OperationExecute.Modified);
                OperationResult<string> result = await _logService.CreateLog(log);
                if (!result.IsSuccessful)
                {
                    result.ToResultWithBoolType();
                }

                return OperationBuilder<bool>.FailureDatabase(ExceptionMessages.FailedOccurredDataLayer);
            }
        }

        /// <summary>
        /// Internal method to update the entity in the database.
        /// </summary>
        /// <param name="entityModified">The modified entity.</param>
        /// <param name="entityUnmodified">The original unmodified entity.</param>
        /// <returns>A task representing the asynchronous operation with the update result.</returns>
        public virtual async Task<OperationResult<T>> UpdateEntity(T entityModified, T entityUnmodified)
        {
            // Custom success message
            string messageSuccessfully = string.Format(ResourceQuery.SuccessfullySearchGeneric, typeof(T).Name);
            return OperationResult<T>.Success(entityModified, messageSuccessfully);
        }
    }
}
