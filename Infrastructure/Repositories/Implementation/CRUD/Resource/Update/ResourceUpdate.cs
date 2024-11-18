namespace Infrastructure.Repositories.Implementation.CRUD.Resource.Update
{
    using Application.Result;
    using Application.UseCases.CRUD.Resource;
    using Application.UseCases.ExternalServices.Resources;
    using Application.UseCases.ExternalServices.Resources.Provider;
    using Application.UseCases.ExternalServices;
    using Application.UseCases.Repository.CRUD;
    using Application.Validators.User;
    using Persistence.BaseDbContext;
    using Infrastructure.Repositories.Abstract.CRUD.Update;
    using FluentValidation.Results;
    using Resource = Domain.Entities.Resource;
    
    /// <summary>
    /// Provides functionality for updating a Resource entity in the database.
    /// </summary>
    public class ResourceUpdate : UpdateRepository<Resource>, IResourceUpdate
    {
        private readonly IResourcesProvider _provider;
        private IResourceHandler _handler;
        protected List<string> _resourceKeys;
        /// <summary>
        /// Initializes a new instance of the <see cref = "ResourceUpdate"/> class.
        /// </summary>
        /// <param name = "context">The database context.</param>
        /// <param name = "logService">The service used for logging operations.</param>
        /// <param name = "utilEntity">Utility functions for the Resource entity.</param>
        /// <param name = "provider">Service providing access to resources.</param>
        /// <param name = "handler">Handler for managing resource operations.</param>
        public ResourceUpdate(DataContext context, ILogService logService, IUtilEntity<Resource> utilEntity, IResourcesProvider provider, IResourceHandler handler) : base(context, logService, utilEntity, provider, handler)
        {
            _provider = provider;
            _handler = handler;
            SetResourceKeys();
        }

        protected virtual void SetResourceKeys()
        {
            _resourceKeys =
            [
                "UpdateFailedDataLength",
                "UpdateFailedDuplicateName",
                "UpdateSearchSuccess"
            ];
        }
        /// <summary>
        /// Updates a resource entity in the database based on the provided modifications.
        /// </summary>
        /// <param name = "entityModified">The updated resource entity with new values.</param>
        /// <param name = "entityUnmodified">The original resource entity before changes.</param>
        /// <returns>An operation result indicating the success or failure of the update operation.</returns>
        public override async Task<Operation<Resource>> UpdateEntity(Resource entityModified, Resource entityUnmodified)
        {
            // Validate the modified entity using the UpdateResourceRules validator
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

            // Ensure that the modified resource name is unique and not associated with another resource
            var id = entityModified?.Id ?? string.Empty;
            var name = entityModified?.Name ?? string.Empty;
            IQueryable<Resource> resourceByName = await ReadFilter(p => (p.Name ?? string.Empty).Equals(name) && !p.Id.Equals(id));
            Resource? resourceExistByName = resourceByName?.FirstOrDefault();
            if (resourceExistByName != null)
            {
                var updateFailedAlreadyRegisteredEmail = _handler.GetResource("UpdateFailedDuplicateName");
                return OperationStrategy<Resource>.Fail(updateFailedAlreadyRegisteredEmail, new BusinessStrategy<Resource>());
            }

            // Update the timestamp of the resource entity
            bool hasNameChanged = !name.Equals(entityUnmodified.Name);
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