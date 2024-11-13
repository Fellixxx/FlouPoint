namespace Infrastructure.Repositories.Implementation.Status
{
    using Infrastructure.Repositories.Abstract.Status;
    using Application.UseCases.ExternalServices;
    using Domain.Entities;
    using Persistence.BaseDbContext;
    using Application.UseCases.Repository.Status.Status;
    using Application.UseCases.Repository;
    using Application.UseCases.ExternalServices.Resorces;

    /// <summary>
    /// Repository class for managing the status of user entities.
    /// </summary>
    public class UserStatus : StatusRepository<User>, IUserStatus
    {
        /// <summary>
        /// Constructor with dependency injection.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="logService">The log service.</param>
        public UserStatus(DataContext context, ILogService logService, IResorcesProvider resourceProvider, IResourceHandler resourceHandler) : base(context, logService, resourceProvider, resourceHandler)
        {

        }
    }
}
