using Application.Result;
using Application.UseCases.ExternalServices;
using Application.UseCases.Repository.CRUD.ResourceEntry;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Implemntation.CRUD.Query
{
    public class ResourceEntryQuery : Read<ResourceEntry>, IResourceEntryQuery
    {
        protected readonly ILogService _logService;

        public ResourceEntryQuery(DbContext context, ILogService logService) : base(context)
        {
            //_authFlowDbContext = context ?? throw new ArgumentNullException(nameof(context));
            _logService = logService ?? throw new ArgumentNullException(nameof(logService));
        }
        public Task<OperationResult<ResourceEntry>> ReadByBearer(string bearerToken)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult<int>> ReadFilterCount(string filter)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult<IQueryable<ResourceEntry>>> ReadFilterPage(int pageNumber, int pageSize, string filter)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult<ResourceEntry>> ReadId(string id)
        {
            throw new NotImplementedException();
        }

        Task<OperationResult<IQueryable<ResourceEntry>>> IResourceEntryReadFilter.ReadFilter(Expression<Func<ResourceEntry, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
