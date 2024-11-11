﻿namespace Infrastructure.Repositories.Abstract.CRUD.Query
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
    using Infrastructure.Constants;
    using Application.UseCases.Repository;

    /// <summary>
    /// Abstract repository class for reading and filtering entities.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    public abstract class ReadFilterRepository<T> : Read<T>, IReadFilterRepository<T> where T : class
    {
        private readonly ILogService _logService;
        private readonly IResourceProvider _resourceProvider;
        private IResourceHandler _resourceHandler;
        private readonly List<string> _resourceKeys;

        /// <summary>
        /// Constructor with dependency injection.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="logService">The log service.</param>
        protected ReadFilterRepository(DbContext context, ILogService logService, IResourceProvider resourceProvider) : base(context)
        {
            _logService = logService;
            _resourceProvider = resourceProvider;
            _resourceKeys =
            [
                "SuccessfullySearchGeneric"
            ];
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
                IQueryable<T> result = await base.ReadFilter(predicate);
                await ResourceHandler.CreateAsync(_resourceProvider, _resourceKeys);
                var successfullySearchGeneric = _resourceHandler.GetResource("SuccessfullySearchGeneric");
                var messageSuccessfully = string.Format(successfullySearchGeneric, typeof(T).Name);
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
                return OperationBuilder<IQueryable<T>>.FailureDatabase(ExceptionMessages.FailedOccurredDataLayer);
            }
        }
    }
}
