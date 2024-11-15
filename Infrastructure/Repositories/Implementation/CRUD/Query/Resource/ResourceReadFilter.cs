namespace Infrastructure.Repositories.Implementation.CRUD.Query.Resource
{
    using Application.UseCases.CRUD.Query.Resource;
    using Resource = Domain.Entities.Resource;
    using Application.UseCases.ExternalServices;
    using Persistence.BaseDbContext;
    using Application.UseCases.ExternalServices.Resources;
    using Infrastructure.Repositories.Abstract.CRUD.Query.ReadFilter;

    public class ResourceReadFilter : ReadFilterRepository<Resource>, IResourceReadFilter
    {
        public ResourceReadFilter(DataContext context, ILogService logService, IResourcesProvider provider, IResourceHandler handler) : base(context, logService, provider, handler)
        {
        }
    }
}
