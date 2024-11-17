namespace Infrastructure.Repositories.Implementation.CRUD.Resource
{
    using Application.UseCases.CRUD.User;
    using Application.UseCases.ExternalServices.Resources;
    using Application.UseCases.ExternalServices;
    using Infrastructure.Repositories.Abstract.CRUD.Delete;
    using Persistence.BaseDbContext;
    using Resource = Domain.Entities.Resource;
    using Application.UseCases.ExternalServices.Resources.Provider;

    /// <summary>
    /// A repository class responsible for deleting resources from the data store.
    /// Implements the <see cref = "IUserDelete"/> interface which may specify user-specific delete operations.
    /// </summary>
    public class ResourceDelete : DeleteRepository<Resource>, IUserDelete
    {
        /// <summary>
        /// Initializes a new instance of the <see cref = "ResourceDelete"/> class.
        /// </summary>
        /// <param name = "context">The database context to interact with the data persistence layer.</param>
        /// <param name = "logService">The log service to handle logging of delete operations and errors.</param>
        /// <param name = "resourceProvider">The resource provider for accessing and managing external resources.</param>
        /// <param name = "resourceHandler">The resource handler for managing resource creation, deletion, or updates.</param>
        public ResourceDelete(DataContext context, ILogService logService, IResourcesProvider resourceProvider, IResourceHandler resourceHandler) : base(context, logService, resourceProvider, resourceHandler)
        {
        }
    }
}