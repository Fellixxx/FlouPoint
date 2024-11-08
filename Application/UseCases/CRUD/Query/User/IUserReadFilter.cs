namespace Application.UseCases.CRUD.Query.User
{
    using Application.Result;
    using System.Linq.Expressions;
    using User = Domain.Entities.User;
    public interface IUserReadFilter
    {
        /// <summary>
        /// The ReadAllByFilter method returns all entities of type T that satisfy the provided predicate.
        /// It takes a lambda expression that represents a condition the entities should meet.
        /// </summary>
        /// <param name="predicate">The predicate</param>
        /// <returns>The result of the operation</returns>
        Task<OperationResult<IQueryable<User>>> ReadFilter(Expression<Func<User, bool>> predicate);
    }
}
