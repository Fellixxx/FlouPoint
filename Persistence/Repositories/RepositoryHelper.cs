namespace Persistence.Repositories
{
    /// <summary>
    /// Provides helper methods for repository operations.
    /// </summary>
    public abstract class RepositoryHelper
    {
        /// <summary>
        /// Validates that the provided argument is not null and throws an <see cref = "ArgumentNullException"/> if it is.
        /// </summary>
        /// <typeparam name = "E">The type of the argument to be validated.</typeparam>
        /// <param name = "entity">The argument to validate.</param>
        /// <returns>The validated argument.</returns>
        /// <exception cref = "ArgumentNullException">Thrown when the provided argument is null.</exception>
        public static E ValidateArgument<E>(E? entity)
        {
            return entity ?? throw new ArgumentNullException(nameof(entity));
        }
    }
}