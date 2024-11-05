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
    /// </summary>
    public class UserQuery : IUserQuery
    {
        private readonly IUserReadFilter _userReadFilter;
        private readonly IUserReadFilterCount _userReadFilterCount;
        private readonly IUserReadFilterPage _userReadFilterPage;
        private readonly IUserReadId _userReadId;

        /// <summary>
        /// Constructor with dependency injection.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="logService">The log service.</param>
        /// <param name="userReadFilter">The user read filter service.</param>
        /// <param name="userReadFilterCount">The user read filter count service.</param>
        /// <param name="userReadFilterPage">The user read filter page service.</param>
        /// <param name="userReadId">The user read by ID service.</param>
        public UserQuery(
            IUserReadFilter userReadFilter,
            IUserReadFilterCount userReadFilterCount,
            IUserReadFilterPage userReadFilterPage,
            IUserReadId userReadId)
        {
            _userReadFilter = userReadFilter;
            _userReadFilterCount = userReadFilterCount;
            _userReadFilterPage = userReadFilterPage;
            _userReadId = userReadId;
        }

        /// <summary>
        /// Read user entities based on the provided filter predicate.
        /// </summary>
        /// <param name="predicate">The filter predicate.</param>
        /// <returns>An operation result with the queried user entities.</returns>
        public Task<OperationResult<IQueryable<User>>> ReadFilter(Expression<Func<User, bool>> predicate)
        {
            return _userReadFilter.ReadFilter(predicate);
        }

        /// <summary>
        /// Read the count of user entities based on the provided filter.
        /// </summary>
        /// <param name="filter">The filter expression.</param>
        /// <returns>An operation result with the count of user entities.</returns>
        public Task<OperationResult<int>> ReadFilterCount(string filter)
        {
            return _userReadFilterCount.ReadFilterCount(filter);
        }

        /// <summary>
        /// Read a page of user entities based on the provided filter and pagination parameters.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="filter">The filter expression.</param>
        /// <returns>An operation result with the queried page of user entities.</returns>
        public Task<OperationResult<IQueryable<User>>> ReadFilterPage(int pageNumber, int pageSize, string filter)
        {
            return _userReadFilterPage.ReadFilterPage(pageNumber, pageSize, filter);
        }

        /// <summary>
        /// Read a user entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <returns>An operation result with the queried user entity.</returns>
        public Task<OperationResult<User>> ReadId(string id)
        {
            return _userReadId.ReadId(id);
        }

        /// <summary>
        /// Read a user entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <returns>An operation result with the queried user entity.</returns>
        public Task<OperationResult<User>> ReadByBearer(string bearerToken)
        {
            return _userReadId.ReadByBearer(bearerToken);
        }
    }
}
