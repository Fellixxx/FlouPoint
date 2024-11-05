
namespace Infrastructure.Repositories.Implementation.CRUD.User
{
    using Application.UseCases.CRUD.User;
    using Infrastructure.Repositories.Abstract.CRUD;
    using Application.UseCases.ExternalServices;
    using Persistence.BaseDbContext;
    using Domain.Entities;

    /// <summary>
    /// Repository class for deleting user entities.
    /// </summary>
    public class UserDelete : DeleteRepository<User>, IUserDelete
    {
        /// <summary>
        /// Constructor with dependency injection.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="logService">The log service.</param>
        public UserDelete(CommonDbContext context,ILogService logService) : base(context, logService)
        {
        }
    }
}
