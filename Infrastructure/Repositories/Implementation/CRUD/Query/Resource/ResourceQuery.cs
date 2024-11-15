namespace Infrastructure.Repositories.Implementation.CRUD.Query.Resource
{
    using Application.Result;
    using Application.UseCases.CRUD.Query.Resource;
    using Application.UseCases.CRUD.Query.User;
    using Application.UseCases.ExternalServices;
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Persistence.Repositories;
    using System.Linq.Expressions;

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
