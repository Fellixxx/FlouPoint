namespace Infrastructure.Repositories.Implementation.CRUD.Query.User
{
    using Application.UseCases.CRUD.Query.User;
    using System.Linq.Expressions;
    using User = Domain.Entities.User;
    using Application.UseCases.ExternalServices;
    using Persistence.BaseDbContext;
    using Application.UseCases.ExternalServices.Resources;
    using Infrastructure.Repositories.Abstract.CRUD.Query.ReadFilterPage;

    /// <summary>
    /// Repository class for reading user entities with filtering and paging.
    /// This class provides functionality to retrieve user data from the database
    /// with specific filtering and pagination options.
    /// </summary>
    public class UserReadFilterPage : ReadFilterPageRepository<User>, IUserReadFilterPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref = "UserReadFilterPage"/> class.
        /// </summary>
        /// <param name = "context">An instance of the database context used to access the data store.</param>
        /// <param name = "logService">A logging service used to record actions or errors during data operations.</param>
        /// <param name = "provider">Resource provider for obtaining external resources if needed.</param>
        public UserReadFilterPage(DataContext context, ILogService logService, IResourcesProvider provider) : base(context, logService, provider)
        {
        }

        /// <summary>
        /// Builds the predicate used to filter user entities based on provided filter criteria.
        /// This predicate is used to filter users by their Name or Email against a search string.
        /// </summary>
        /// <param name = "filter">A string containing the search filter. Can be part of a name or email.</param>
        /// <returns>An expression that represents the predicate logic to be applied.</returns>
        public override Expression<Func<User, bool>> GetPredicate(string filter)
        {
            // Converts the filter to lowercase to ensure the search is case-insensitive
            filter = filter.ToLower();
            // Returns a predicate that checks if the filter string is null or whitespace.
            // If not, it checks if the user's Name or Email contains the filter string.
            return u => string.IsNullOrWhiteSpace(filter) || (u.Name ?? string.Empty).Contains(filter, StringComparison.CurrentCultureIgnoreCase) || (u.Email ?? string.Empty).Contains(filter, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}