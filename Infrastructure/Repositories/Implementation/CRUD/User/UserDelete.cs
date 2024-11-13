
namespace Infrastructure.Repositories.Implementation.CRUD.User
{
    using Application.UseCases.CRUD.User;
    using Application.UseCases.ExternalServices;
    using Persistence.BaseDbContext;
    using Domain.Entities;
    using Application.UseCases.Repository.CRUD;
    using Application.UseCases.Repository;
    using Infrastructure.Repositories.Abstract.CRUD.Delete;
    using Application.UseCases.ExternalServices.Resorces;

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
        public UserDelete(DataContext context,ILogService logService, IResorcesProvider resourceProvider, IResourceHandler resourceHandler) : base(context, logService, resourceProvider, resourceHandler)
        {
        }
    }
}
