namespace Infrastructure.Repositories.Implementation.CRUD.Resource.Update
{
    using Application.Result;
    using Application.UseCases.CRUD.User;
    using Application.UseCases.ExternalServices.Resources;
    using Application.UseCases.ExternalServices;
    using Application.UseCases.Repository.CRUD;
    using Application.Validators.User;
    using Infrastructure.Repositories.Abstract.CRUD.Update;
    using FluentValidation.Results;
    using Resource = Domain.Entities.Resource;
    using Persistence.BaseDbContext;

    public class ResourceUpdate : UpdateRepository<Resource>, IResourceUpdate
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
        public ResourceUpdate(DataContext context, ILogService logService, IUtilEntity<Resource> utilEntity, IResourcesProvider provider, IResourceHandler handler) : base(context, logService, utilEntity, provider, handler)
        {
            _provider = provider;
            _handler = handler;
            _resourceKeys = new List<string>
            {
                "UpdateFailedDataLength",
                "UpdateFailedDuplicateName",
                "UpdateSearchSuccess"
            };
        }

        /// <summary>
        /// Updates a user entity in the database based on provided modifications.
        /// </summary>
        /// <param name = "entityModified">The updated user entity with new values.</param>
        /// <param name = "entityUnmodified">The original user entity before changes.</param>
        /// <returns>An operation result indicating the success or failure of the update operation.</returns>
        public override async Task<Operation<Resource>> UpdateEntity(Resource entityModified, Resource entityUnmodified)
        {
            // Validate the modified entity using the UpdateUserRules validator
            UpdateResourceRules validatorModified = new UpdateResourceRules();
            ValidationResult result = validatorModified.Validate(entityModified);
            // Load resources required for generating messages
            await ResourceHandler.CreateAsync(_provider, _resourceKeys);
            // If validation fails, construct an error message and return a failed operation result
            if (!result.IsValid)
            {
                string errorMessage = GetErrorMessage(result);
                var updateFailedDataSizeCharacter = _handler.GetResource("UpdateFailedDataLength");
                string message = string.Format(updateFailedDataSizeCharacter, errorMessage);
                var updateFailedAlreadyRegisteredEmail = _handler.GetResource("UpdateFailedDuplicateName");
                return OperationStrategy<Resource>.Fail(updateFailedAlreadyRegisteredEmail, new BusinessStrategy<Resource>());
            }

            // Ensure that the modified email is unique and not associated with another user
            var id = entityModified?.Id ?? string.Empty;
            var name = entityModified?.Name ?? string.Empty;
            IQueryable<Resource> userByEmail = await ReadFilter(p => (p.Name ?? string.Empty).Equals(name) && !p.Id.Equals(id));
            Resource? resourceExistByName = userByEmail?.FirstOrDefault();
            if (resourceExistByName != null)
            {
                var updateFailedAlreadyRegisteredEmail = _handler.GetResource("UpdateFailedDuplicateName");
                return OperationStrategy<Resource>.Fail(updateFailedAlreadyRegisteredEmail, new BusinessStrategy<Resource>());
            }

            
            // Check for changes in the email and update relevant properties
            bool hasEmailChanged = !name.Equals(entityUnmodified.Name);
            entityUnmodified.UpdatedAt = DateTime.Now;
            // Return a success operation result
            var updateSuccessfullySearchGeneric = _handler.GetResource("UpdateSearchSuccess");
            var successMessage = string.Format(updateSuccessfullySearchGeneric, typeof(Resource).Name);
            return Operation<Resource>.Success(entityUnmodified, successMessage);
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
