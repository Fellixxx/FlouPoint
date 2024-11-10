namespace Infrastructure.Repositories.Abstract.CRUD.Query
{
    using Application.Result;
    using Application.UseCases.ExternalServices;
    using Application.UseCases.Repository.CRUD.Query;
    using Infrastructure.Other;
    using Persistence.Repositories;
    using System.Linq.Expressions;
    using Microsoft.EntityFrameworkCore;
    using Domain.DTO.Logging;
    using Domain.EnumType;

    /// <summary>
    /// Abstract repository class for reading and filtering entities.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    public abstract class ReadFilterRepository<T> : Read<T>, IReadFilterRepository<T> where T : class
    {
        private readonly ILogService _logService;

        /// <summary>
        /// Constructor with dependency injection.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="logService">The log service.</param>
        protected ReadFilterRepository(DbContext context, ILogService logService) : base(context)
        {
            _logService = logService;
        }

        /// <summary>
        /// Read and filter entities based on the provided predicate.
        /// </summary>
        /// <param name="predicate">The filter predicate.</param>
        /// <returns>A task representing the asynchronous operation with the filtered entities.</returns>
        public new async Task<OperationResult<IQueryable<T>>> ReadFilter(Expression<Func<T, bool>> predicate)
        {
            try
            {
                // Get entities from the database based on the provided filter expression
                IQueryable<T> result = await base.ReadFilter(predicate);

                // Custom success message
                string messageSuccessfully = string.Format(ResourceQuery.SuccessfullySearchGeneric, typeof(T).Name);

                // Return a success operation result
                return OperationResult<IQueryable<T>>.Success(result, messageSuccessfully);
            }
            catch (Exception ex)
            {
                // Create a log entry for the exception
                Log log = Util.GetLogError(ex, predicate, OperationExecute.GetAllByFilter);
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
    }
}
