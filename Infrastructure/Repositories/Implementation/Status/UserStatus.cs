namespace Infrastructure.Repositories.Implementation.Status
{
    using Infrastructure.Repositories.Abstract.Status;
    using Application.UseCases.ExternalServices;
    using Domain.Entities;
    using Persistence.BaseDbContext;
    using Application.UseCases.Repository.Status.Status;
    using Application.UseCases.ExternalServices.Resources;
    using Application.UseCases.ExternalServices.Resources.Provider;

    /// <summary>
    /// Repository class for managing the status of user entities.
    /// </summary>
    public class UserStatus : StatusRepository<User>, IUserStatus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref = "UserStatus"/> class with specified dependencies.
        /// </summary>
        /// <param name = "context">The database context used for accessing user data.</param>
        /// <param name = "logService">The service used for logging information and errors.</param>
        /// <param name = "resourceProvider">The provider for accessing external resources.</param>
        /// <param name = "resourceHandler">The handler for processing resources.</param>
        public UserStatus(DataContext context, ILogService logService, IResourcesProvider resourceProvider, IResourceHandler resourceHandler) : base(context, logService, resourceProvider, resourceHandler)
        {
        // This constructor utilizes dependency injection to supply the necessary services and context
        // to the base StatusRepository class, ensuring that this UserStatus repository can 
        // effectively manage user status with the required external services.
        }
    }
}