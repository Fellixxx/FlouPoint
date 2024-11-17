namespace Infrastructure.Repositories.Implementation.CRUD.Query.Resource
{
    using Application.UseCases.CRUD.Query.Resource;
    using Resource = Domain.Entities.Resource;
    using Application.UseCases.ExternalServices;
    using Persistence.BaseDbContext;
    using Application.UseCases.ExternalServices.Resources;
    using Infrastructure.Repositories.Abstract.CRUD.Query.ReadFilter;
    using Application.UseCases.ExternalServices.Resources.Provider;

    /// <summary>
    /// Represents a repository implementation for reading and filtering resources.
    /// Inherits from ReadFilterRepository and implements the IResourceReadFilter interface.
    /// </summary>
    public class ResourceReadFilter : ReadFilterRepository<Resource>, IResourceReadFilter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref = "ResourceReadFilter"/> class.
        /// </summary>
        /// <param name = "context">The database context to be used for data operations.</param>
        /// <param name = "logService">Service used for logging operations and error handling.</param>
        /// <param name = "provider">Resource provider service for additional resource handling.</param>
        /// <param name = "handler">Handler for processing specific resource operations.</param>
        public ResourceReadFilter(DataContext context, ILogService logService, IResourcesProvider provider, IResourceHandler handler) : base(context, logService, provider, handler)
        {
        }
    }
}