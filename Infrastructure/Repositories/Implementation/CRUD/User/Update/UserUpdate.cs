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
    using Infrastructure.Repositories.Abstract.CRUD.Query;
    using Application.UseCases.Repository.CRUD;
    using Application.UseCases.Repository;
    using Infrastructure;
    using Infrastructure.Repositories.Abstract.CRUD.Update;
    using Application.UseCases.ExternalServices.Resorces;

    /// <summary>
    /// Provides methods to update user entities in the database.
    /// This class extends from the base UpdateRepository for generic update operations.
    /// </summary>
    public class UserUpdate : UpdateRepository<User>, IUserUpdate
    {
        private readonly IResorcesProvider _provider;
        private IResourceHandler _handler;
        private readonly List<string> _resourceKeys;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserUpdate"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="logService">The service used for logging operations.</param>
        public UserUpdate(
            DataContext context, 
            ILogService logService, 
            IUtilEntity<User> utilEntity, 
            IResorcesProvider provider, 
            IResourceHandler handler) : base(context,
                logService,
                utilEntity,
                provider,
                handler)
        {
            _provider = provider;
            _handler = handler;
            _resourceKeys =
            [
                "UpdateFailedDataSizeCharacter",
                "UpdateFailedEmailInvalidFormat",
                "UpdateFailedAlreadyRegisteredEmail"
            ];
        }

        /// <summary>
        /// Updates a user entity in the database based on provided modifications.
        /// </summary>
        /// <param name="entityModified">The updated user entity with new values.</param>
        /// <param name="entityUnmodified">The original user entity before changes.</param>
        /// <returns>An operation result indicating the success or failure of the update operation.</returns>
        public override async Task<Operation<User>> UpdateEntity(User entityModified, User entityUnmodified)
        {
            // Validate the modified entity using the UpdateUserRules validator
            UpdateUserRules validatorModified = new UpdateUserRules();
            ValidationResult result = validatorModified.Validate(entityModified);
            await ResourceHandler.CreateAsync(_provider, _resourceKeys);

            // If validation fails, construct an error message and return a failed operation result
            if (!result.IsValid)
            {
                string errorMessage = GetErrorMessage(result);
                var failedDataSizeCharacter = _handler.GetResource("UpdateFailedDataSizeCharacter");
                string message = string.Format(failedDataSizeCharacter, errorMessage);
                return OperationBuilder<User>.FailBusiness(message);
            }

            // Check for email validity
            var email = entityModified?.Email ?? string.Empty;
            if (!CredentialUtility.IsValidEmail(email))
            {
                var failedDataSizeCharacter = _handler.GetResource("UpdateFailedEmailInvalidFormat");
                return OperationBuilder<User>.FailBusiness(failedDataSizeCharacter);
            }

            string id = entityModified?.Id ?? string.Empty;
            // Ensure that the modified email is unique and not associated with another user
            IQueryable<User> userByEmail = await ReadFilter(p => (p.Email ?? string.Empty).Equals(email) && !p.Id.Equals(id));
            User? userExistByEmail = userByEmail?.FirstOrDefault();
            if (userExistByEmail is not null)
            {
                var failedDataSizeCharacter = _handler.GetResource("UpdateFailedAlreadyRegisteredEmail");
                return OperationBuilder<User>.FailBusiness(failedDataSizeCharacter);
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
            var successfullySearchGeneric = _handler.GetResource("UpdateSuccessfullySearchGeneric");
            var successMessage = string.Format(successfullySearchGeneric, typeof(User).Name);
            return Operation<User>.Success(entityUnmodified, successMessage);
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
