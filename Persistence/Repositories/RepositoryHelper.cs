namespace Persistence.Repositories
{
    public abstract class RepositoryHelper
    {

        /// <summary>
        /// Validates if the provided argument is not null.
        /// </summary>
        /// <typeparam name="E">Type of the argument.</typeparam>
        /// <param name="entity">The argument to be validated.</param>
        /// <returns>The validated argument.</returns>
        public static E ValidateArgument<E>(E? entity)
        {
            return entity ?? throw new ArgumentNullException(nameof(entity));
        }
    }
}