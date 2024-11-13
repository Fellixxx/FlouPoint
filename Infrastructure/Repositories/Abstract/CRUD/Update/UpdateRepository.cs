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
    using Application.UseCases.Repository;
    using Application.UseCases.ExternalServices.Resorces;

    /// <summary>
    /// Abstract repository class for updating an entity.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    public abstract class UpdateRepository<T> : EntityExistenceValidator<T>, IUpdate<T> where T : class, IEntity
    {
        private readonly ILogService _logService;
        private readonly IUtilEntity<T> _utilEntity;
        private readonly IResorcesProvider _resourceProvider;
        private IResourceHandler _resourceHandler;
        private readonly List<string> _resourceKeys;

        /// <summary>
        /// Constructor with dependency injection.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="logService">The log service.</param>
        protected UpdateRepository(DbContext context, ILogService logService, IUtilEntity<T> utilEntity, IResorcesProvider resourceProvider, IResourceHandler resourceHandler) : base(context, resourceProvider, resourceHandler)
        {
            _logService = logService;
            _utilEntity = utilEntity;
            _resourceProvider = resourceProvider;
            _resourceHandler = resourceHandler;
            _resourceKeys =
            [
                "SuccessfullyGenericUpdated"
            ];
        }

        /// <summary>
        /// Updates an entity in the database.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>A task representing the asynchronous operation with the update result.</returns>
        public new async Task<Operation<bool>> Update(T entity)
        {
            try
            {
                Operation<T> hasEntity = await _utilEntity.HasEntity(entity);
                if (!hasEntity.IsSuccessful)
                {
                    return hasEntity.ToResultWithBoolType();
                }

                Operation<T> resultExist = await HasId(entity.Id);
                if (!resultExist.IsSuccessful)
                {
                    return resultExist.ToResultWithBoolType();
                }

                Operation<T> resultModifyEntity = await UpdateEntity(entity, resultExist.Data);
                if (!resultModifyEntity.IsSuccessful)
                {
                    return resultModifyEntity.ToResultWithBoolType();
                }

                // If validation is successful, update the entity in the database
                bool updateResult = await base.Update(resultModifyEntity.Data);

                await ResourceHandler.CreateAsync(_resourceProvider, _resourceKeys);
                var successfullyGenericActiveated = _resourceHandler.GetResource("SuccessfullyGenericUpdated");
                // Custom success message
                string messageSuccess = string.Format(successfullyGenericActiveated, typeof(T).Name);

                // Return a success operation result
                return Operation<bool>.Success(updateResult, messageSuccess);

            }
            catch (Exception ex)
            {
                Log log = Other.Util.GetLogError(ex, entity, ActionType.Modified);
                Operation<string> result = await _logService.CreateLog(log);
                if (!result.IsSuccessful)
                {
                    result.ToResultWithBoolType();
                }

                return OperationBuilder<bool>.FailDatabase(MessageConstants.ErrorOccurredDataLayer);
            }
        }

        /// <summary>
        /// Internal method to update the entity in the database.
        /// </summary>
        /// <param name="entityModified">The modified entity.</param>
        /// <param name="entityUnmodified">The original unmodified entity.</param>
        /// <returns>A task representing the asynchronous operation with the update result.</returns>
        public virtual async Task<Operation<T>> UpdateEntity(T entityModified, T entityUnmodified)
        {
            // Custom success message
            string messageSuccessfully = string.Format(ResourceQuery.SuccessfullySearchGeneric, typeof(T).Name);
            return Operation<T>.Success(entityModified, messageSuccessfully);
        }
    }
}
