using Application.UseCases.CRUD.Query.User;
using Application.UseCases.ExternalServices.Resources;
using Application.UseCases.ExternalServices;
using Infrastructure.Repositories.Abstract.CRUD.Query.ReadFilterPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Implementation.CRUD.Query.Resource
{
    using Application.UseCases.CRUD.Query.User;
    using System.Linq.Expressions;
    using Resource = Domain.Entities.Resource;
    using Application.UseCases.ExternalServices;
    using Persistence.BaseDbContext;
    using Application.UseCases.ExternalServices.Resources;
    using Infrastructure.Repositories.Abstract.CRUD.Query.ReadFilterPage;

    public class ResourceReadFilterPage : ReadFilterPageRepository<Resource>, IResourceReadFilterPage
    {
        public ResourceReadFilterPage(DataContext context, ILogService logService, IResourcesProvider provider) : base(context, logService, provider)
        {
        }

        public override Expression<Func<Resource, bool>> GetPredicate(string key)
        {
            return u => u.Name == key;
        }
    }
}
