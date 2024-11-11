namespace Infrastructure.Repositories.Abstract.CRUD
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

    /// <summary>
    /// Abstract repository class for creating a new entity.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    public abstract class CreateRepository<T> : Repository<T>, ICreateRepository<T> where T : class, IEntity
    {
        private readonly ILogService _logService;
        private readonly IUtilEntity<T> _utilEntity;

        /// <summary>
        /// Constructor with dependency injection.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="logService">The log service.</param>
        public CreateRepository(DbContext context, ILogService logService, IUtilEntity<T> utilEntity) : base(context)
        {
            _logService = logService;
            _utilEntity = utilEntity;
        }

        /// <summary>
        /// Add a new entity to the database after performing validations.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>A task representing the asynchronous operation with the added entity's ID.</returns>
        public new async Task<OperationResult<string>> Create(T entity)
        {
            try
            {
                OperationResult<T> hasEntity = await _utilEntity.HasEntity(entity);
                if (!hasEntity.IsSuccessful)
                {
                    return hasEntity.ToResultWithStringType();
                }

                // Validate the entity
                OperationResult<T> validationResult = await CreateEntity(entity);
                if (!validationResult.IsSuccessful)
                {
                    return validationResult.ToResultWithStringType();
                }

                // If validation is successful, add the entity to the database
                var addedEntityResult = await base.Create(validationResult.Data);

                // Create a success message and return the success result
                var successMessage = string.Format(Resource.SuccessfullyGeneric, typeof(T).Name);
                return OperationResult<string>.Success(addedEntityResult, successMessage);
            }
            catch (Exception ex)
            {
                Log log = Util.GetLogError(ex, entity, OperationExecute.Add);
                OperationResult<string> result = await _logService.CreateLog(log);
                if (!result.IsSuccessful)
                {
                    result.ToResultWithStringType();
                }

                return OperationBuilder<string>.FailureDatabase(ExceptionMessages.FailedOccurredDataLayer);
            }
        }

        /// <summary>
        /// Abstract method to validate an entity, must be overridden in derived classes.
        /// </summary>
        /// <param name="entity">The entity to validate.</param>
        /// <returns>A task representing the asynchronous operation with the validation result.</returns>
        protected abstract Task<OperationResult<T>> CreateEntity(T entity);
    }
}
