
namespace Infrastructure.Repositories.Implementation.CRUD.Query.User
{
    using Application.UseCases.CRUD.Query.User;
    using Infrastructure.Repositories.Abstract.CRUD.Query;
    using System.Linq.Expressions;
    using Application.UseCases.ExternalServices;
    using User = Domain.Entities.User;
    using Persistence.BaseDbContext;

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
        public UserReadFilterCount(CommonDbContext context, ILogService logService) : base(context, logService)
        {
        }

        /// <summary>
        /// Get the predicate for filtering user entities based on the provided filter.
        /// </summary>
        /// <param name="filter">The filter string.</param>
        /// <returns>The predicate expression for filtering.</returns>
        public override Expression<Func<User, bool>> GetPredicate(string filter)
        {
            // Convert the filter to lowercase for case-insensitive comparison
            filter = filter.ToLower();

            // Define the predicate expression based on the filter
            return u => string.IsNullOrWhiteSpace(filter) ||
            (u.Name ?? string.Empty).ToLower().Contains(filter) ||
            (u.Email ?? string.Empty).ToLower().Contains(filter);
        }
    }
}
