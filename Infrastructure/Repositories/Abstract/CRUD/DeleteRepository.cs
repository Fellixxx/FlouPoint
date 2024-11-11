namespace Infrastructure.Repositories.Abstract.CRUD
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

    /// <summary>
    /// Abstract repository class for deleting an entity.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    public abstract class DeleteRepository<T> : EntityExistenceValidator<T>, IDeleteRepository<T> where T : class, IEntity
    {
        private readonly ILogService _logService;

        /// <summary>
        /// Constructor with dependency injection.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="logService">The log service.</param>
        protected DeleteRepository(DbContext context, ILogService logService, IResourceProvider resourceProvider) : base(context, resourceProvider)
        {
            _logService = logService;
        }

        /// <summary>
        /// Deletes an entity from the database.
        /// </summary>
        /// <param name="id">The ID of the entity to delete.</param>
        /// <returns>A task representing the asynchronous operation with the deletion result.</returns>
        public async Task<OperationResult<bool>> Delete(string id)
        {
            try
            {
                // Validate if the entity with the provided ID exists
                OperationResult<T> validationResult = await HasId(id);

                // If validation is not successful, return a failure operation result
                if (!validationResult.IsSuccessful)
                {
                    return validationResult.ToResultWithBoolType();
                }

                // If validation is successful, delete the entity from the database
                var entity = RepositoryHelper.ValidateArgument<T>(validationResult.Data);
                bool result = await Delete(entity);

                // Custom success message
                string messageSuccess = string.Format(Resource.SuccessfullyGenericDeleted, typeof(T).Name);

                // Return a success operation result
                return OperationResult<bool>.Success(result, messageSuccess);
            }
            catch (Exception ex)
            {
                Log log = Util.GetLogError(ex, id, OperationExecute.Remove);
                OperationResult<string> result = await _logService.CreateLog(log);
                if (!result.IsSuccessful)
                {
                    result.ToResultWithBoolType();
                }

                return OperationBuilder<bool>.FailureDatabase(ExceptionMessages.FailedOccurredDataLayer);
            }
        }
    }
}
