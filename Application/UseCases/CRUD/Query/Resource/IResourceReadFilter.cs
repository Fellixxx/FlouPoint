namespace Application.UseCases.CRUD.Query.Resource
{
    using Application.Result;
    using System.Linq.Expressions;
    using Resource = Domain.Entities.Resource;
    public interface IResourceReadFilter
    {
        /// <summary>
        /// The ReadAllByFilter method returns all entities of type T that satisfy the provided predicate.
        /// It takes a lambda expression that represents a condition the entities should meet.
        /// </summary>
        /// <param name="predicate">The predicate</param>
        /// <returns>The result of the operation</returns>
        Task<Operation<IQueryable<Resource>>> ReadFilter(Expression<Func<Resource, bool>> predicate);
    }
}
