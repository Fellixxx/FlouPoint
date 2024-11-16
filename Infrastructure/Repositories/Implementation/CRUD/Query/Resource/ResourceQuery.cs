namespace Infrastructure.Repositories.Implementation.CRUD.Query.Resource
{
    using Application.Result;
    using Application.UseCases.CRUD.Query.Resource;
    using Application.UseCases.CRUD.Query.User;
    using Domain.Entities;
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public class ResourceQuery : IResourceQuery
    {
        private readonly IResourceReadFilter _resourceReadFilter;
        private readonly IResourceReadFilterCount _resourceReadFilterCount;
        private readonly IResourceReadFilterPage _resourceReadFilterPage;
        private readonly IResourceReadId _resourceReadId;

        public ResourceQuery(
            IResourceReadFilter resourceReadFilter,
            IResourceReadFilterCount resourceReadFilterCount,
            IResourceReadFilterPage resourceReadFilterPage,
            IResourceReadId resourceReadId
            )
        {
            _resourceReadFilter=resourceReadFilter;
            _resourceReadFilterCount=resourceReadFilterCount;
            _resourceReadFilterPage=resourceReadFilterPage;
            _resourceReadId=resourceReadId;
        }

        public Task<Operation<Resource>> ReadByBearer(string bearerToken)
        {
            return _resourceReadId.ReadByBearer(bearerToken);
        }

        public Task<Operation<IQueryable<Resource>>> ReadFilter(Expression<Func<Resource, bool>> predicate)
        {
            return _resourceReadFilter.ReadFilter(predicate);
        }

        public Task<Operation<int>> ReadFilterCount(string filter)
        {
            return _resourceReadFilterCount.ReadFilterCount(filter);
        }

        public Task<Operation<IQueryable<Resource>>> ReadFilterPage(int pageNumber, int pageSize, string filter)
        {
            return _resourceReadFilterPage.ReadFilterPage(pageNumber, pageSize, filter);
        }

        public Task<Operation<Resource>> ReadId(string id)
        {
            return _resourceReadId.ReadId(id);
        }
    }
}
