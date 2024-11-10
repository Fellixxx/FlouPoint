﻿namespace Infrastructure.Repositories.Implementation.CRUD.User
{
    using Application.Result;
    using FluentValidation.Results;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Infrastructure.Repositories.Abstract.CRUD;
    using Application.UseCases.ExternalServices;
    using Application.UseCases.CRUD.User;
    using UtilitiesLayer;
    using Persistence.BaseDbContext;
    using Domain.Entities;
    using Application.Validators.User;
    using Infrastructure.Repositories.Abstract.CRUD.Query;

    /// <summary>
    /// Provides methods to update user entities in the database.
    /// This class extends from the base UpdateRepository for generic update operations.
    /// </summary>
    public class UserUpdate : UpdateRepository<User>, IUserUpdate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserUpdate"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="logService">The service used for logging operations.</param>
        public UserUpdate(CommonDbContext context, ILogService logService) : base(context, logService)
        {
        }

        /// <summary>
        /// Updates a user entity in the database based on provided modifications.
        /// </summary>
        /// <param name="entityModified">The updated user entity with new values.</param>
        /// <param name="entityUnmodified">The original user entity before changes.</param>
        /// <returns>An operation result indicating the success or failure of the update operation.</returns>
        public override async Task<OperationResult<User>> UpdateEntity(User entityModified, User entityUnmodified)
        {
            // Validate the modified entity using the UpdateUserRules validator
            UpdateUserRules validatorModified = new UpdateUserRules();
            ValidationResult result = validatorModified.Validate(entityModified);

            // If validation fails, construct an error message and return a failed operation result
            if (!result.IsValid)
            {
                string errorMessage = GetErrorMessage(result);
                string message = string.Format(Resource.FailedDataSizeCharacter, errorMessage);
                return OperationBuilder<User>.FailureBusinessValidation(message);
            }

            // Check for email validity
            var email = entityModified?.Email ?? string.Empty;
            if (!CredentialUtility.IsValidEmail(email))
            {
                return OperationBuilder<User>.FailureBusinessValidation(Resource.FailedEmailInvalidFormat);
            }

            string id = entityModified?.Id ?? string.Empty;
            // Ensure that the modified email is unique and not associated with another user
            IQueryable<User> userByEmail = await ReadFilter(p => (p.Email ?? string.Empty).Equals(email) && !p.Id.Equals(id));
            User? userExistByEmail = userByEmail?.FirstOrDefault();
            if (userExistByEmail is not null)
            {
                return OperationBuilder<User>.FailureBusinessValidation(Resource.FailedAlreadyRegisteredEmail);
            }

            // Check for changes in the email and update relevant properties
            bool hasEmailChanged = !email.Equals(entityUnmodified.Email);
            if (hasEmailChanged)
            {
                var name = entityModified?.Name ?? string.Empty;
                entityUnmodified.Name = name;
                entityUnmodified.Email = email;
                entityUnmodified.Active = false;  // Deactivate the user if email changes
            }

            // Update the timestamp and hashed password
            entityUnmodified.UpdatedAt = DateTime.Now;
            var password = entityModified?.Password ?? string.Empty;
            entityUnmodified.Password = CredentialUtility.ComputeSha256Hash(password);

            // Return a success operation result
            var successfullySearchGeneric = ResourceQuery.SuccessfullySearchGeneric;
            var successMessage = string.Format(successfullySearchGeneric, typeof(User).Name);
            return OperationResult<User>.Success(entityUnmodified, successMessage);
        }

        /// <summary>
        /// Constructs an error message from a validation result.
        /// </summary>
        /// <param name="result">The validation result containing potential errors.</param>
        /// <returns>The constructed error message.</returns>
        private static string GetErrorMessage(ValidationResult result)
        {
            IEnumerable<string> errors = result.Errors.Select(x => x.ErrorMessage).Distinct();
            string errorMessage = string.Join(", ", errors);
            return errorMessage;
        }
    }
}
