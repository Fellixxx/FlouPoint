namespace Infrastructure.Repositories.Implementation.Status
{
    using Infrastructure.Repositories.Abstract.Status;
    using Application.UseCases.ExternalServices;
    using Application.UseCases.Repository.Status.StatusChange;
    using Domain.Entities;
    using Persistence.BaseDbContext;

    /// <summary>
    /// Repository class for managing the status of user entities.
    /// </summary>
    public class UserStatus : StatusRepository<User>, IStatusRepository
    {
        /// <summary>
        /// Constructor with dependency injection.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="logService">The log service.</param>
        public UserStatus(CommonDbContext context, ILogService logService) : base(context, logService)
        {
        }
    }
}
