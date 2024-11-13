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
    using Application.UseCases.ExternalServices.Resorces;

    /// <summary>
    /// Abstract repository class for reading and counting entities based on a filter.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    public abstract class ReadFilterCountRepository<T> : Read<T>, IReadFilterCount<T> where T : class
    {
        protected readonly ILogService _logService;
        private readonly IResorcesProvider _provider;
        private IResourceHandler _handler;
        private readonly List<string> _resourceKeys;

        /// <summary>
        /// Constructor with dependency injection.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="logService">The log service.</param>
        protected ReadFilterCountRepository(DbContext context, ILogService logService, IResorcesProvider provider, IResourceHandler handler) : base(context)
        {
            _logService = logService;
            _provider = provider;
            _handler = handler;
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
        public async Task<Operation<int>> ReadFilterCount(string filter)
        {
            try
            {
                Expression<Func<T, bool>> predicate = GetPredicate(filter);
                int result = await ReadCountFilter(predicate);
                await ResourceHandler.CreateAsync(_provider, _resourceKeys);
                var messageSuccessfully = _handler.GetResource("SuccessfullySearchGeneric");
                return Operation<int>.Success(result, messageSuccessfully);
            }
            catch (Exception ex)
            {
                // Create a log entry for the exception
                var log = Util.GetLogError(ex, filter, ActionType.GetCountFilter);
                var result = await _logService.CreateLog(log);

                // Handle logging failure
                if (!result.IsSuccessful)
                {
                    result.ConvertTo<int>();
                }

                // Return a failure operation result for database issues
                return OperationBuilder<int>.FailDatabase(Message.ErrorOccurredDataLayer);
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
