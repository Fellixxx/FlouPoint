namespace Infrastructure.Repositories.Abstract.CRUD.Delete
{
    using Application.Result;
    using Application.UseCases.ExternalServices;
    using Application.UseCases.Repository.CRUD;
    using Domain.Interfaces.Entity;
    using Infrastructure.Other;
    using Infrastructure.Repositories.Abstract.CRUD.Validation;
    using Persistence.Repositories;
    using Microsoft.EntityFrameworkCore;
    using Domain.DTO.Logging;
    using Domain.EnumType;
    using Infrastructure.Constants;
    using Application.UseCases.ExternalServices.Resources;
    using Application.UseCases.ExternalServices.Resources.Provider;

    /// <summary>
    /// Abstract repository class for deleting an entity.
    /// </summary>
    /// <typeparam name = "T">The entity type.</typeparam>
    public abstract class DeleteRepository<T> : EntityChecker<T>, IDelete<T> where T : class, IEntity
    {
        // Private fields for logging, resource management, and storing resource keys.
        private readonly ILogService _logService;
        private readonly IResourcesProvider _provider;
        private IResourceHandler _handler;
        private readonly List<string> _resourceKeys;
        /// <summary>
        /// Constructor with dependency injection to initialize necessary services.
        /// </summary>
        /// <param name = "context">The database context for accessing data storage.</param>
        /// <param name = "logService">The log service for recording log messages.</param>
        /// <param name = "provider">The resource provider for managing localization resources.</param>
        /// <param name = "handler">The resource handler for obtaining specific resources.</param>
        protected DeleteRepository(DbContext context, ILogService logService, IResourcesProvider provider, IResourceHandler handler) : base(context, provider, handler)
        {
            _logService = logService;
            _provider = provider;
            _handler = handler;
            _resourceKeys =
            [
                "DeletionSuccess"
            ]; // Initialize resource keys
        }

        /// <summary>
        /// Deletes an entity from the database by its ID.
        /// </summary>
        /// <param name = "id">The ID of the entity to delete.</param>
        /// <returns>A task representing the asynchronous deletion operation with the result of the operation.</returns>
        public async Task<Operation<bool>> Delete(string id)
        {
            try
            {
                // Validate whether the entity with the specified ID exists.
                Operation<T> validationResult = await HasId(id);
                // If validation fails, convert and return the failure operation result.
                if (!validationResult.IsSuccessful)
                {
                    return validationResult.ConvertTo<bool>();
                }

                // If validation is successful, proceed to delete the entity.
                var entity = RepositoryHelper.ValidateArgument(validationResult.Data);
                bool result = await Delete(entity);
                // Log a custom success message using resource management.
                await ResourceHandler.CreateAsync(_provider, _resourceKeys);
                var deletionSuccess = _handler.GetResource("DeletionSuccess");
                string messageSuccess = string.Format(deletionSuccess, typeof(T).Name);
                // Return a success operation result if deletion is successful.
                return Operation<bool>.Success(result, messageSuccess);
            }
            catch (Exception ex)
            {
                // In case of an exception, create and log an error entry.
                Log log = Util.GetLogError(ex, id, ActionType.Remove);
                Operation<string> result = await _logService.CreateLog(log);
                // If logging the error fails, convert the result to a boolean operation.
                if (!result.IsSuccessful)
                {
                    result.ConvertTo<bool>();
                }

                // Return a failure operation strategy with an error message.
                var strategy = new DatabaseStrategy<bool>();
                var errorOccurredDataLayer = Message.ErrorOccurredDataLayer;
                return OperationStrategy<bool>.Fail(errorOccurredDataLayer, strategy);
            }
        }
    }
}