namespace Infrastructure.Repositories.Abstract.CRUD.Query
{
    using Application.Result;
    using Application.UseCases.ExternalServices;
    using Application.UseCases.Repository.CRUD.Query;
    using Domain.DTO.Log;
    using Infrastructure.Other;
    using Persistence.Repositories;
    using System;
    using System.Linq.Expressions;
    using Microsoft.EntityFrameworkCore;
    using Domain.EnumType.OperationExecute;

    /// <summary>
    /// Abstract repository class for reading and counting entities based on a filter.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    public abstract class ReadFilterCountRepository<T> : Read<T>, IReadFilterCountRepository<T> where T : class
    {
        protected readonly ILogService _logService;

        /// <summary>
        /// Constructor with dependency injection.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="logService">The log service.</param>
        protected ReadFilterCountRepository(DbContext context, ILogService logService) : base(context)
        {
            _logService = logService;
        }

        /// <summary>
        /// Read and count entities based on a filter.
        /// </summary>
        /// <param name="filter">The filter expression.</param>
        /// <returns>A task representing the asynchronous operation with the count result.</returns>
        public async Task<OperationResult<int>> ReadFilterCount(string filter)
        {
            try
            {
                // Get entities from the database based on the provided filter expression
                Expression<Func<T, bool>> predicate = GetPredicate(filter);
                int result = await ReadCountFilter(predicate);

                // Custom success message
                string messageSuccessfully = string.Format(Resource.SuccessfullySearchGeneric, typeof(T).Name);

                // Return a success operation result
                return OperationResult<int>.Success(result, messageSuccessfully);

            }
            catch (Exception ex)
            {
                // Create a log entry for the exception
                Log log = Util.GetLogError(ex, filter, OperationExecute.GetCountFilter);
                OperationResult<string> result = await _logService.CreateLog(log);

                // Handle logging failure
                if (!result.IsSuccessful)
                {
                    result.ToResultWithBoolType();
                }

                // Return a failure operation result for database issues
                return OperationBuilder<int>.FailureDatabase(Resource.FailedOccurredDataLayer);
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
