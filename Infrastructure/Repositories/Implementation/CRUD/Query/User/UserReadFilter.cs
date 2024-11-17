namespace Infrastructure.Repositories.Implementation.CRUD.Query.User
{
    using Application.UseCases.CRUD.Query.User;
    using User = Domain.Entities.User;
    using Application.UseCases.ExternalServices;
    using Persistence.BaseDbContext;
    using Application.UseCases.ExternalServices.Resources;
    using Infrastructure.Repositories.Abstract.CRUD.Query.ReadFilter;
    using Application.UseCases.ExternalServices.Resources.Provider;

    /// <summary>
    /// Repository class for reading user entities with filtering.
    /// This class extends the generic ReadFilterRepository, specializing it for the User entity type.
    /// </summary>
    public class UserReadFilter : ReadFilterRepository<User>, IUserReadFilter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref = "UserReadFilter"/> class.
        /// </summary>
        /// <param name = "context">The database context used for accessing the data store.</param>
        /// <param name = "logService">The logging service used to log information, warnings, and errors.</param>
        /// <param name = "provider">The resources provider for accessing external resources or configurations.</param>
        /// <param name = "handler">The resource handler responsible for processing resources from the provider.</param>
        /// <remarks>
        /// This constructor allows for dependency injection, which facilitates testing and improves code maintainability.
        /// It passes the required dependencies to the base class constructor.
        /// </remarks>
        public UserReadFilter(DataContext context, ILogService logService, IResourcesProvider provider, IResourceHandler handler) : base(context, logService, provider, handler)
        {
        }
    }
}