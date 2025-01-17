﻿namespace Application.UseCases.CRUD.Validation
{
    using Domain.Entities;
    using Application.Result;

    /// <summary>
    /// Defines the contract for validating the existence of a User entity in the repository.
    /// </summary>
    public interface IUserChecker
    {
        /// <summary>
        /// Checks if a User entity with the given ID exists in the repository.
        /// </summary>
        /// <param name="id">The ID of the User entity to check.</param>
        /// <returns>An <see cref="Operation{T}"/> containing the User entity if it exists, or null if it does not.</returns>
        Task<Operation<User>> HasEntity(string id);
    }
}
