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
    using Infrastructure.Constants;
    using Application.UseCases.ExternalServices.Resources;

    /// <summary>
    /// Abstract repository class for reading and filtering entities with pagination.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    public abstract class ReadFilterPageRepository<T> : Read<T>, IReadFilterPage<T> where T : class
    {
        private readonly ILogService _logService;
        private readonly IResourcesProvider _provider;
        private IResourceHandler _handler;
        private readonly List<string> _resourceKeys;

        /// <summary>
        /// Constructor with dependency injection.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="logService">The log service.</param>
        protected ReadFilterPageRepository(DbContext context, ILogService logService, IResourcesProvider provider) : base(context)
        {
            _logService = logService;
            _provider = provider;
            _resourceKeys =
            [
                "SuccessfullySearchGeneric"
            ];
        }

        /// <summary>
        /// Read and filter entities with pagination.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="filter">The filter expression.</param>
        /// <returns>A task representing the asynchronous operation with the filtered entities.</returns>
        public async Task<Operation<IQueryable<T>>> ReadFilterPage(int pageNumber, int pageSize, string filter)
        {
            try
            {
                Expression<Func<T, bool>> predicate = GetPredicate(filter);
                IQueryable<T> result = await ReadPageByFilter(predicate, pageNumber, pageSize);
                await ResourceHandler.CreateAsync(_provider, _resourceKeys);
                var successfullySearchGeneric = _handler.GetResource("SuccessfullySearchGeneric");
                var messageSuccessfully = string.Format(successfullySearchGeneric, typeof(T).Name);
                return Operation<IQueryable<T>>.Success(result, messageSuccessfully);
            }
            catch (Exception ex)
            {
                var filterValue = new
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    Filter = filter
                };
                Log log = Util.GetLogError(ex, filterValue, ActionType.GetPageByFilter);
                Operation<string> result = await _logService.CreateLog(log);
                if (!result.IsSuccessful)
                {
                    result.ConvertTo<IQueryable<T>>();
                }
                var strategy = new DatabaseStrategy<IQueryable<T>>();
                var errorOccurredDataLayer = Message.ErrorOccurredDataLayer;
                return OperationStrategy<IQueryable<T>>.Fail(errorOccurredDataLayer, strategy);
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
