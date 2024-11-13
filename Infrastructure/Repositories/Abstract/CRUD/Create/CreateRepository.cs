namespace Infrastructure.Repositories.Abstract.CRUD.Create
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
    using Application.UseCases.Repository;
    using Application.UseCases.ExternalServices.Resorces;

    /// <summary>
    /// Abstract repository class for creating a new entity.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    public abstract class CreateRepository<T> : Repository<T>, ICreate<T> where T : class, IEntity
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
        public CreateRepository(DbContext context, ILogService logService, IUtilEntity<T> utilEntity, IResorcesProvider resourceProvider, IResourceHandler resourceHandler) : base(context)
        {
            _logService = logService;
            _utilEntity = utilEntity;
            _resourceProvider = resourceProvider;
            _resourceHandler = resourceHandler;
            _resourceKeys =
            [
                "SuccessfullyGeneric"
            ];
        }

        /// <summary>
        /// Add a new entity to the database after performing validations.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>A task representing the asynchronous operation with the added entity's ID.</returns>
        public new async Task<Operation<string>> Create(T entity)
        {
            try
            {
                Operation<T> hasEntity = await _utilEntity.HasEntity(entity);
                if (!hasEntity.IsSuccessful)
                {
                    return hasEntity.ToResultWithStringType();
                }

                // Validate the entity
                Operation<T> validationResult = await CreateEntity(entity);
                if (!validationResult.IsSuccessful)
                {
                    return validationResult.ToResultWithStringType();
                }

                // If validation is successful, add the entity to the database
                var addedEntityResult = await base.Create(validationResult.Data);
                await ResourceHandler.CreateAsync(_resourceProvider, _resourceKeys);
                var successfullyGeneric = _resourceHandler.GetResource("SuccessfullyGeneric");

                // Create a success message and return the success result
                var successMessage = string.Format(successfullyGeneric, typeof(T).Name);
                return Operation<string>.Success(addedEntityResult, successMessage);
            }
            catch (Exception ex)
            {
                Log log = Util.GetLogError(ex, entity, ActionType.Add);
                Operation<string> result = await _logService.CreateLog(log);
                if (!result.IsSuccessful)
                {
                    result.ToResultWithStringType();
                }

                return OperationBuilder<string>.FailDatabase(Message.ErrorOccurredDataLayer);
            }
        }

        /// <summary>
        /// Abstract method to validate an entity, must be overridden in derived classes.
        /// </summary>
        /// <param name="entity">The entity to validate.</param>
        /// <returns>A task representing the asynchronous operation with the validation result.</returns>
        protected abstract Task<Operation<T>> CreateEntity(T entity);
    }
}
