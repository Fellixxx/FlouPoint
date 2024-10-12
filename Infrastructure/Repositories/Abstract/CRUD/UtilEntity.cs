namespace Infrastructure.Repositories.Abstract.CRUD
{
    using global::Application.Result;

    /// <summary>
    /// Utility class for entity validation operations.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    public static class UtilEntity<T>
    {
        /// <summary>
        /// Check if an entity exists.
        /// </summary>
        /// <param name="entity">The entity to be checked.</param>
        /// <returns>An operation result indicating whether the entity exists or not.</returns>
        public static OperationResult<T> HasEntity(T entity)
        {
            if (entity is null)
            {
                // Return a failure result if the entity is null
                return OperationBuilder<T>.FailureBusinessValidation(Resource.FailedNecesaryData);
            }

            // Return a success result if the entity is not null
            return OperationResult<T>.Success(entity, Resource.GlobalOkMessage);
        }
    }
}
