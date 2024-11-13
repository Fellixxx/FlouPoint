
namespace Infrastructure.Repositories.Implementation.CRUD.Query.User
{
    using Application.UseCases.CRUD.Query.User;
    using Infrastructure.Repositories.Abstract.CRUD.Query;
    using System.Linq.Expressions;
    using Application.UseCases.ExternalServices;
    using User = Domain.Entities.User;
    using Persistence.BaseDbContext;
    using Microsoft.EntityFrameworkCore;
    using Application.UseCases.ExternalServices.Resources;

    /// <summary>
    /// Repository class for reading user entity counts with filtering.
    /// </summary>
    public class UserReadFilterCount : ReadFilterCountRepository<User>, IUserReadFilterCount
    {
        /// <summary>
        /// Constructor with dependency injection.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="logService">The log service.</param>
        public UserReadFilterCount(
            DataContext context, 
            ILogService logService, 
            IResourcesProvider provider, 
            IResourceHandler handler) : base(context, logService, provider, handler)
        {
        }

        /// <summary>
        /// Get the predicate for filtering user entities based on the provided filter.
        /// </summary>
        /// <param name="filter">The filter string.</param>
        /// <returns>The predicate expression for filtering.</returns>
        public override Expression<Func<User, bool>> GetPredicate(string filter)
        {
            if (string.IsNullOrWhiteSpace(filter))
            {
                return u => true; // No filtering if the filter is empty
            }

            // Escape wildcard characters in the filter
            filter = filter.Replace("[", "[[]").Replace("%", "[%]").Replace("_", "[_]");

            // Add wildcards for partial matching
            filter = $"%{filter}%";

            return u => EF.Functions.Like(u.Name, filter) || EF.Functions.Like(u.Email, filter);
        }
    }
}
