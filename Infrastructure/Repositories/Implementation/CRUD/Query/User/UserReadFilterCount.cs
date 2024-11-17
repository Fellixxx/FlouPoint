namespace Infrastructure.Repositories.Implementation.CRUD.Query.User
{
    using Application.UseCases.CRUD.Query.User;
    using System.Linq.Expressions;
    using Application.UseCases.ExternalServices;
    using User = Domain.Entities.User;
    using Persistence.BaseDbContext;
    using Microsoft.EntityFrameworkCore;
    using Application.UseCases.ExternalServices.Resources;
    using Infrastructure.Repositories.Abstract.CRUD.Query.ReadFilterCount;
    using Application.UseCases.ExternalServices.Resources.Provider;

    /// <summary>
    /// Repository class for reading filtered user entity counts.
    /// </summary>
    public class UserReadFilterCount : ReadFilterCountRepository<User>, IUserReadFilterCount
    {
        /// <summary>
        /// Initializes a new instance of the <see cref = "UserReadFilterCount"/> class.
        /// </summary>
        /// <param name = "context">The database context.</param>
        /// <param name = "logService">The logging service used for capturing log information.</param>
        /// <param name = "provider">Resource provider for external services.</param>
        /// <param name = "handler">Resource handler for managing resource queries.</param>
        public UserReadFilterCount(DataContext context, ILogService logService, IResourcesProvider provider, IResourceHandler handler) : base(context, logService, provider, handler)
        {
        }

        /// <summary>
        /// Generates a predicate expression based on the provided filter for user entities.
        /// </summary>
        /// <param name = "filter">The filter string used to determine which users to include.</param>
        /// <returns>An expression used to filter user entities in the database.</returns>
        public override Expression<Func<User, bool>> GetPredicate(string filter)
        {
            if (string.IsNullOrWhiteSpace(filter))
            {
                // Return a predicate that includes all users since no specific filter is provided
                return u => true;
            }

            // Escape SQL wildcard characters to prevent unintended pattern matching
            filter = filter.Replace("[", "[[]").Replace("%", "[%]").Replace("_", "[_]");
            // Add SQL wildcards for matching any sequence of characters before and after the filter text
            filter = $"%{filter}%";
            // Return a predicate that matches user names or emails against the pattern provided
            return u => EF.Functions.Like(u.Name, filter) || EF.Functions.Like(u.Email, filter);
        }
    }
}