namespace Infrastructure.Repositories.Abstract.CRUD.Query
{
    using Application.Result;
    using Application.UseCases.ExternalServices;
    using Infrastructure.Other;
    using Persistence.Repositories;
    using System;
    using System.Linq.Expressions;
    using Application.UseCases.Repository.CRUD.Query;
    using Microsoft.EntityFrameworkCore;
    using Domain.DTO.Logging;
    using Domain.EnumType;

    /// <summary>
    /// Abstract repository class for reading and filtering entities with pagination.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    public abstract class ReadFilterPageRepository<T> : Read<T>, IReadFilterPageRepository<T> where T : class
    {
        private readonly ILogService _logService;
        private readonly IResourceProvider _resourceProvider;

        /// <summary>
        /// Constructor with dependency injection.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="logService">The log service.</param>
        protected ReadFilterPageRepository(DbContext context, ILogService logService, IResourceProvider resourceProvider) : base(context)
        {
            _logService = logService;
            _resourceProvider = resourceProvider;
        }

        /// <summary>
        /// Read and filter entities with pagination.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="filter">The filter expression.</param>
        /// <returns>A task representing the asynchronous operation with the filtered entities.</returns>
        public async Task<OperationResult<IQueryable<T>>> ReadFilterPage(int pageNumber, int pageSize, string filter)
        {
            try
            {
                Expression<Func<T, bool>> predicate = GetPredicate(filter);
                IQueryable<T> result = await ReadPageByFilter(predicate, pageNumber, pageSize);
                var resultResource = await _resourceProvider.GetMessage("SuccessfullySearchGeneric");
                if (!resultResource.IsSuccessful)
                {
                    return OperationBuilder<IQueryable<T>>.FailureBusinessValidation("Due to unknowlege reason, no resource exists with the specified key.");
                }
                var successfullySearchGeneric = resultResource.Data.Value;
                var messageSuccessfully = string.Format(successfullySearchGeneric, typeof(T).Name);
                return OperationResult<IQueryable<T>>.Success(result, messageSuccessfully);

            }
            catch (Exception ex)
            {
                // Create a filter value object for logging
                var filterValue = new
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    Filter = filter
                };

                // Create a log entry for the exception
                Log log = Util.GetLogError(ex, filterValue, OperationExecute.GetPageByFilter);
                OperationResult<string> result = await _logService.CreateLog(log);

                // Handle logging failure
                if (!result.IsSuccessful)
                {
                    result.ToResultWithXType<IQueryable<T>>();
                }

                // Return a failure operation result for database issues
                return OperationBuilder<IQueryable<T>>.FailureDatabase(Resource.FailedOccurredDataLayer);
            }
        }

        /// <summary>
        /// Get the predicate based on the provided filter.
        /// </summary>
        /// <param name="filter">The filter expression.</param>
        /// <returns>The predicate expression.</returns>
        public abstract Expression<Func<T, bool>> GetPredicate(string filter);
    }
}
