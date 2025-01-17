﻿namespace Application.UseCases.Repository.Status.StatusChange
{
    using Application.Result;

    /// <summary>
    /// Defines the contract for deactivating an active entity in the repository.
    /// </summary>
    public interface IDeactivate
    {
        /// <summary>
        /// Deactivates an active entity in the repository.
        /// </summary>
        /// <param name="id">The ID of the entity to deactivate.</param>
        /// <returns>An <see cref="Operation{T}"/> containing a boolean that indicates whether the deactivation was successful.</returns>
        Task<Operation<bool>> Deactivate(string id);
    }
}
