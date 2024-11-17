namespace Infrastructure.Repositories.Implementation.CRUD.User.Update
{
    using Application.Result;
    using FluentValidation.Results;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Application.UseCases.ExternalServices;
    using Application.UseCases.CRUD.User;
    using UtilitiesLayer;
    using Persistence.BaseDbContext;
    using Domain.Entities;
    using Application.Validators.User;
    using Application.UseCases.Repository.CRUD;
    using Infrastructure.Repositories.Abstract.CRUD.Update;
    using Application.UseCases.ExternalServices.Resources;
    using Application.UseCases.ExternalServices.Resources.Provider;

    /// <summary>
    /// Provides methods to update user entities in the database.
    /// This class extends from the base UpdateRepository for generic update operations.
    /// </summary>
    public class UserUpdate : UpdateRepository<User>, IUserUpdate
    {
        private readonly IResourcesProvider _provider;
        private IResourceHandler _handler;
        private readonly List<string> _resourceKeys;
        /// <summary>
        /// Initializes a new instance of the <see cref = "UserUpdate"/> class.
        /// </summary>
        /// <param name = "context">The database context.</param>
        /// <param name = "logService">The service used for logging operations.</param>
        /// <param name = "utilEntity">Utility functions for the User entity.</param>
        /// <param name = "provider">Service providing access to resources.</param>
        /// <param name = "handler">Handler for managing resource operations.</param>
        public UserUpdate(DataContext context, ILogService logService, IUtilEntity<User> utilEntity, IResourcesProvider provider, IResourceHandler handler) : base(context, logService, utilEntity, provider, handler)
        {
            _provider = provider;
            _handler = handler;
            _resourceKeys = new List<string>
            {
                "UpdateFailedDataSizeCharacter",
                "UpdateFailedEmailInvalidFormat",
                "UpdateFailedAlreadyRegisteredEmail",
                "UpdateSuccessfullySearchGeneric"
            };
        }

        /// <summary>
        /// Updates a user entity in the database based on provided modifications.
        /// </summary>
        /// <param name = "entityModified">The updated user entity with new values.</param>
        /// <param name = "entityUnmodified">The original user entity before changes.</param>
        /// <returns>An operation result indicating the success or failure of the update operation.</returns>
        public override async Task<Operation<User>> UpdateEntity(User entityModified, User entityUnmodified)
        {
            // Validate the modified entity using the UpdateUserRules validator
            UpdateUserRules validatorModified = new UpdateUserRules();
            ValidationResult result = validatorModified.Validate(entityModified);
            // Load resources required for generating messages
            await ResourceHandler.CreateAsync(_provider, _resourceKeys);
            // If validation fails, construct an error message and return a failed operation result
            if (!result.IsValid)
            {
                string errorMessage = GetErrorMessage(result);
                var updateFailedDataSizeCharacter = _handler.GetResource("UpdateFailedDataSizeCharacter");
                string message = string.Format(updateFailedDataSizeCharacter, errorMessage);
                var updateFailedAlreadyRegisteredEmail = _handler.GetResource("UpdateFailedAlreadyRegisteredEmail");
                return OperationStrategy<User>.Fail(updateFailedAlreadyRegisteredEmail, new BusinessStrategy<User>());
            }

            // Check for email validity
            var email = entityModified?.Email ?? string.Empty;
            if (!CredentialUtility.IsValidEmail(email))
            {
                var updateFailedEmailInvalidFormat = _handler.GetResource("UpdateFailedEmailInvalidFormat");
                return OperationStrategy<User>.Fail(updateFailedEmailInvalidFormat, new BusinessStrategy<User>());
            }

            // Ensure that the modified email is unique and not associated with another user
            string id = entityModified?.Id ?? string.Empty;
            IQueryable<User> userByEmail = await ReadFilter(p => (p.Email ?? string.Empty).Equals(email) && !p.Id.Equals(id));
            User? userExistByEmail = userByEmail?.FirstOrDefault();
            if (userExistByEmail != null)
            {
                var updateFailedAlreadyRegisteredEmail = _handler.GetResource("UpdateFailedAlreadyRegisteredEmail");
                return OperationStrategy<User>.Fail(updateFailedAlreadyRegisteredEmail, new BusinessStrategy<User>());
            }

            // Check for changes in the email and update relevant properties
            bool hasEmailChanged = !email.Equals(entityUnmodified.Email);
            if (hasEmailChanged)
            {
                var name = entityModified?.Name ?? string.Empty;
                entityUnmodified.Name = name;
                entityUnmodified.Email = email;
                entityUnmodified.Active = false; // Deactivate the user if email changes
            }

            // Update the timestamp and hashed password
            entityUnmodified.UpdatedAt = DateTime.Now;
            var password = entityModified?.Password ?? string.Empty;
            entityUnmodified.Password = CredentialUtility.ComputeSha256Hash(password);
            // Return a success operation result
            var updateSuccessfullySearchGeneric = _handler.GetResource("UpdateSuccessfullySearchGeneric");
            var successMessage = string.Format(updateSuccessfullySearchGeneric, typeof(User).Name);
            return Operation<User>.Success(entityUnmodified, successMessage);
        }

        /// <summary>
        /// Constructs an error message from a validation result.
        /// </summary>
        /// <param name = "result">The validation result containing potential errors.</param>
        /// <returns>The constructed error message.</returns>
        private static string GetErrorMessage(ValidationResult result)
        {
            // Extract distinct error messages and concatenate them into a single string
            IEnumerable<string> errors = result.Errors.Select(x => x.ErrorMessage).Distinct();
            string errorMessage = string.Join(", ", errors);
            return errorMessage;
        }
    }
}