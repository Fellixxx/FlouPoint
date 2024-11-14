namespace Infrastructure.Repositories.Implementation.CRUD.Query.User
{
    using Application.UseCases.CRUD.Query.User;
    using Infrastructure.Repositories.Abstract.CRUD.Query;
    using User = Domain.Entities.User;
    using Application.UseCases.ExternalServices;
    using Persistence.BaseDbContext;
    using Application.UseCases.ExternalServices.Resources;

    /// <summary>
    /// Repository class for reading a user entity by its ID.
    /// </summary>
    public class UserReadId : ReadIdRepository<User>, IUserReadId
    {
        /// <summary>
        /// Initializes a new instance of the <see cref = "UserReadId"/> class with the specified dependencies.
        /// </summary>
        /// <param name = "context">The database context used for accessing the user entities.</param>
        /// <param name = "logService">The logging service used for tracking and logging operations.</param>
        /// <param name = "provider">The resource provider used for managing external resources.</param>
        /// <param name = "handler">The resource handler used for processing resource-related tasks.</param>
        public UserReadId(DataContext context, ILogService logService, IResourcesProvider provider, IResourceHandler handler) : base(context, logService, provider, handler)
        {
        }
    }
}