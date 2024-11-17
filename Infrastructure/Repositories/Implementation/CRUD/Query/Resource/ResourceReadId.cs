namespace Infrastructure.Repositories.Implementation.CRUD.Query.Resource
{
    using Application.UseCases.CRUD.Query.User;
    using Resource = Domain.Entities.Resource;
    using Application.UseCases.ExternalServices;
    using Persistence.BaseDbContext;
    using Application.UseCases.ExternalServices.Resources;
    using Infrastructure.Repositories.Abstract.CRUD.Query.ReadId;
    using Application.UseCases.ExternalServices.Resources.Provider;

    /// <summary>
    /// Represents a repository class responsible for reading a Resource entity by its identifier.
    /// Inherits from ReadIdRepository and implements the IResourceReadId interface.
    /// </summary>
    public class ResourceReadId : ReadIdRepository<Resource>, IResourceReadId
    {
        /// <summary>
        /// Initializes a new instance of the <see cref = "ResourceReadId"/> class.
        /// </summary>
        /// <param name = "context">The data context used to interact with the database.</param>
        /// <param name = "logService">The logging service used to log messages and errors.</param>
        /// <param name = "provider">The resources provider used to manage resources.</param>
        /// <param name = "handler">The resource handler used to process resource-specific operations.</param>
        public ResourceReadId(DataContext context, ILogService logService, IResourcesProvider provider, IResourceHandler handler) : base(context, logService, provider, handler)
        {
        // Constructor logic (if needed) goes here.
        // Currently, it just calls the base constructor.
        }
    }
}