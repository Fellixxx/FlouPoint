using Application.UseCases.CRUD.Query.User;
using Application.UseCases.ExternalServices.Resources;
using Application.UseCases.ExternalServices;
using Infrastructure.Repositories.Abstract.CRUD.Query.ReadFilterCount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Implementation.CRUD.Query.Resource
{
    using System.Linq.Expressions;
    using Application.UseCases.ExternalServices;
    using Resource = Domain.Entities.Resource;
    using Persistence.BaseDbContext;
    using Application.UseCases.ExternalServices.Resources;
    using Infrastructure.Repositories.Abstract.CRUD.Query.ReadFilterCount;
    using Application.UseCases.CRUD.Query.Resource;

    public class ResourceReadFilterCount : ReadFilterCountRepository<Resource>, IResourceReadFilterCount
    {
        public ResourceReadFilterCount(DataContext context, ILogService logService, IResourcesProvider provider, IResourceHandler handler) : base(context, logService, provider, handler)
        {
        }

        public override Expression<Func<Resource, bool>> GetPredicate(string key)
        {
            return u => u.Name == key;
        }
    }
}
