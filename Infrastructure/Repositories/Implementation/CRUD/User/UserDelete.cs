namespace Infrastructure.Repositories.Implementation.CRUD.User
{
    using Application.UseCases.CRUD.User;
    using Application.UseCases.ExternalServices;
    using Persistence.BaseDbContext;
    using Domain.Entities;
    using Application.UseCases.Repository.CRUD;
    using Infrastructure.Repositories.Abstract.CRUD.Delete;
    using Application.UseCases.ExternalServices.Resources;

    /// <summary>
    /// Repository class for deleting user entities.
    /// This class provides functionality to delete user records from the database.
    /// It inherits from a generic DeleteRepository class and implements the IUserDelete interface.
    /// </summary>
    public class UserDelete : DeleteRepository<User>, IUserDelete
    {
        /// <summary>
        /// Initializes a new instance of the <see cref = "UserDelete"/> class.
        /// Constructor with dependency injection, initializes the base DeleteRepository with required services.
        /// </summary>
        /// <param name = "context">The database context to interact with the data persistence layer.</param>
        /// <param name = "logService">The log service to handle logging of delete operations and errors.</param>
        /// <param name = "resourceProvider">The resource provider for accessing and managing external resources.</param>
        /// <param name = "resourceHandler">The resource handler for managing resource creation, deletion, or updates.</param>
        public UserDelete(DataContext context, ILogService logService, IResourcesProvider resourceProvider, IResourceHandler resourceHandler) : base(context, logService, resourceProvider, resourceHandler)
        {
        }
    }
}