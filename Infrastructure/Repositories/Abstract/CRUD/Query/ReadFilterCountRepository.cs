namespace Infrastructure.Repositories.Abstract.CRUD.Query
{
    using Application.Result;
    using Application.UseCases.ExternalServices;
    using Application.UseCases.Repository.CRUD.Query;
    using Infrastructure.Other;
    using Persistence.Repositories;
    using System;
    using System.Linq.Expressions;
    using Microsoft.EntityFrameworkCore;
    using Domain.DTO.Logging;
    using Domain.EnumType;
    using Application.UseCases.Repository;
    using Infrastructure.Constants;

    /// <summary>
    /// Abstract repository class for reading and counting entities based on a filter.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    public abstract class ReadFilterCountRepository<T> : Read<T>, IReadFilterCountRepository<T> where T : class
    {
        protected readonly ILogService _logService;
        private readonly IResourceProvider _resourceProvider;
        private IResourceHandler _resourceHandler;
        private readonly List<string> _resourceKeys;

        /// <summary>
        /// Constructor with dependency injection.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="logService">The log service.</param>
        protected ReadFilterCountRepository(DbContext context, ILogService logService, IResourceProvider resourceProvider, IResourceHandler resourceHandler) : base(context)
        {
            _logService = logService;
            _resourceProvider = resourceProvider;
            _resourceHandler = resourceHandler;
            _resourceKeys =
            [
                "SuccessfullySearchGeneric"
            ];
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
                Expression<Func<T, bool>> predicate = GetPredicate(filter);
                int result = await ReadCountFilter(predicate);
                await ResourceHandler.CreateAsync(_resourceProvider, _resourceKeys);
                var messageSuccessfully = _resourceHandler.GetResource("SuccessfullySearchGeneric");
                return OperationResult<int>.Success(result, messageSuccessfully);
            }
            catch (Exception ex)
            {
                // Create a log entry for the exception
                var log = Util.GetLogError(ex, filter, OperationExecute.GetCountFilter);
                var result = await _logService.CreateLog(log);

                // Handle logging failure
                if (!result.IsSuccessful)
                {
                    result.ToResultWithIntType();
                }

                // Return a failure operation result for database issues
                return OperationBuilder<int>.FailureDatabase(ExceptionMessages.ErrorOccurredDataLayer);
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
