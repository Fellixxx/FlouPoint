namespace Infrastructure.Repositories.Implementation.CRUD.Query.User
{
    using Application.Result;
    using Application.UseCases.CRUD.Query.User;
    using Domain.Entities;
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    /// <summary>
    /// Repository class for querying user entities.
    /// Provides methods to interact with user data using various query criteria.
    /// </summary>
    public class UserQuery : IUserQuery
    {
        private readonly IUserReadFilter _userReadFilter;
        private readonly IUserReadFilterCount _userReadFilterCount;
        private readonly IUserReadFilterPage _userReadFilterPage;
        private readonly IUserReadId _userReadId;
        /// <summary>
        /// Initializes a new instance of the <see cref = "UserQuery"/> class with injected dependencies.
        /// </summary>
        /// <param name = "userReadFilter">The service responsible for filtering user queries.</param>
        /// <param name = "userReadFilterCount">The service for getting the count of users based on filters.</param>
        /// <param name = "userReadFilterPage">The service for paginating filtered user results.</param>
        /// <param name = "userReadId">The service to read a user by their ID.</param>
        public UserQuery(
            IUserReadFilter userReadFilter, 
            IUserReadFilterCount userReadFilterCount, 
            IUserReadFilterPage userReadFilterPage, 
            IUserReadId userReadId
            )
        {
            _userReadFilter = userReadFilter;
            _userReadFilterCount = userReadFilterCount;
            _userReadFilterPage = userReadFilterPage;
            _userReadId = userReadId;
        }

        /// <summary>
        /// Queries user entities based on a specified filter predicate.
        /// Allows for flexible query composition using expressions.
        /// </summary>
        /// <param name = "predicate">The predicate expression to filter users.</param>
        /// <returns>An operation result containing the filtered list of user entities.</returns>
        public Task<Operation<IQueryable<User>>> ReadFilter(Expression<Func<User, bool>> predicate)
        {
            return _userReadFilter.ReadFilter(predicate);
        }

        /// <summary>
        /// Retrieves the count of user entities that match a given filter expression.
        /// Useful for understanding the scale of data matching certain criteria.
        /// </summary>
        /// <param name = "filter">A filter expression in string format.</param>
        /// <returns>An operation result containing the count of matching user entities.</returns>
        public Task<Operation<int>> ReadFilterCount(string filter)
        {
            return _userReadFilterCount.ReadFilterCount(filter);
        }

        /// <summary>
        /// Retrieves a page of user entities based on filtering and pagination parameters.
        /// Facilitates efficient data display by using paginated results.
        /// </summary>
        /// <param name = "pageNumber">The desired page number to retrieve.</param>
        /// <param name = "pageSize">The number of users per page.</param>
        /// <param name = "filter">A string representing the filter criteria.</param>
        /// <returns>An operation result containing the queried page of user entities.</returns>
        public Task<Operation<IQueryable<User>>> ReadFilterPage(int pageNumber, int pageSize, string filter)
        {
            return _userReadFilterPage.ReadFilterPage(pageNumber, pageSize, filter);
        }

        /// <summary>
        /// Queries a specific user entity by their unique ID.
        /// </summary>
        /// <param name = "id">The unique identifier for the user.</param>
        /// <returns>An operation result containing the requested user entity.</returns>
        public Task<Operation<User>> ReadId(string id)
        {
            return _userReadId.ReadId(id);
        }

        /// <summary>
        /// Queries a user entity based on a bearer token.
        /// Typically used for authentication-related queries.
        /// </summary>
        /// <param name = "bearerToken">The bearer token associated with the user.</param>
        /// <returns>An operation result containing the queried user entity.</returns>
        public Task<Operation<User>> ReadByBearer(string bearerToken)
        {
            return _userReadId.ReadByBearer(bearerToken);
        }
    }
}