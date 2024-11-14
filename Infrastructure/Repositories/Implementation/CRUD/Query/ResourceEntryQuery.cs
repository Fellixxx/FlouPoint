namespace Infrastructure.Repositories.Implementation.CRUD.Query
{
    using Application.Result;
    using Application.UseCases.ExternalServices;
    using Application.UseCases.Repository.CRUD.Resource;
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Persistence.Repositories;
    using System.Linq.Expressions;

    public class ResourceEntryQuery : Read<Resource>, IQuery
    {
        protected readonly ILogService _logService;

        public ResourceEntryQuery(DbContext context, ILogService logService) : base(context)
        {
            //_authFlowDbContext = context ?? throw new ArgumentNullException(nameof(context));
            _logService = logService ?? throw new ArgumentNullException(nameof(logService));
        }
        public Task<Operation<Resource>> ReadByBearer(string bearerToken)
        {
            throw new NotImplementedException();
        }

        public Task<Operation<int>> ReadFilterCount(string filter)
        {
            throw new NotImplementedException();
        }

        public Task<Operation<IQueryable<Resource>>> ReadFilterPage(int pageNumber, int pageSize, string filter)
        {
            throw new NotImplementedException();
        }

        public Task<Operation<Resource>> ReadId(string id)
        {
            throw new NotImplementedException();
        }

        Task<Operation<IQueryable<Resource>>> IResourceReadFilter.ReadFilter(Expression<Func<Resource, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
