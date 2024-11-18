namespace Infrastructure.Repositories.Implementation.CRUD.Resource.Create
{
    using Application.Result;
    using Application.UseCases.CRUD.Resource;
    using Application.UseCases.ExternalServices;
    using Application.UseCases.ExternalServices.Resources;
    using Application.UseCases.Repository.CRUD;
    using Application.Validators.User;
    using Infrastructure.Repositories.Abstract.CRUD.Create;
    using Microsoft.EntityFrameworkCore;
    using FluentValidation.Results;
    using System.Threading.Tasks;
    using Resource = Domain.Entities.Resource;
    using Application.UseCases.ExternalServices.Resources.Provider;

    /// <summary>
    /// Represents the logic for creating a Resource entity in the database.
    /// Inherits from CreateRepository and implements the IResourceCreate interface.
    /// </summary>
    public class ResourceCreate : CreateRepository<Resource>, IResourceCreate
    {
        // Provider for resources, used to create and manage resource-specific operations.
        private readonly IResourcesProvider _provider;
        // Handler to manage resource-related actions and retrieve resource messages.
        private IResourceHandler _handler;
        // List of keys used for managing resource-related messages.
        protected List<string> _resourceKeys;
        /// <summary>
        /// Initializes a new instance of the <see cref = "ResourceCreate"/> class.
        /// </summary>
        /// <param name = "context">Database context for entity operations.</param>
        /// <param name = "logService">Service used for logging operations.</param>
        /// <param name = "utilEntity">Utility entity operations handler.</param>
        /// <param name = "provider">Resources provider for resource management.</param>
        /// <param name = "handler">Handler for resources, used for message retrieval and actions.</param>
        public ResourceCreate(DbContext context, ILogService logService, IUtilEntity<Resource> utilEntity, IResourcesProvider provider, IResourceHandler handler) : base(context, logService, utilEntity, provider, handler)
        {
            _provider = provider;
            _handler = handler;
            SetResourceKeys();
        }

        protected virtual void SetResourceKeys()
        {
            _resourceKeys =
            [
                "ResourceFailedDataLength",
                "ResourceFailedDuplicateName"
            ];
        }
        /// <summary>
        /// Asynchronously creates a resource entity after validation.
        /// </summary>
        /// <param name = "entity">The resource entity to be created.</param>
        /// <returns>An operation result containing the created resource or an error message.</returns>
        protected override async Task<Operation<Resource>> CreateEntity(Resource entity)
        {
            // Validate the resource entity using the defined rules.
            CreateResourceRules validatorAdd = new CreateResourceRules();
            ValidationResult result = validatorAdd.Validate(entity);
            // Execute additional provider operations if necessary.
            await ResourceHandler.CreateAsync(_provider, _resourceKeys);
            // Check if validation fails and return an error if so.
            if (!result.IsValid)
            {
                string errorMessage = GetErrorMessage(result);
                var createFailedDataSizeCharacter = _handler.GetResource("ResourceFailedDataLength");
                var message = string.Format(createFailedDataSizeCharacter, errorMessage);
                var business = new BusinessStrategy<Resource>();
                return OperationStrategy<Resource>.Fail(message, business);
            }

            var name = entity?.Name ?? string.Empty;
            var id = entity?.Id ?? string.Empty;
            // Check for name uniqueness by seeing if it's already used by another resource.
            IQueryable<Resource> resourceByName = await ReadFilter(p => p.Name == name || p.Id == id);
            Resource? resourceExistByName = resourceByName?.FirstOrDefault();
            if (resourceExistByName is not null)
            {
                var createFailedAlreadyRegisteredName = _handler.GetResource("ResourceFailedDuplicateName");
                return OperationStrategy<Resource>.Fail(createFailedAlreadyRegisteredName, new BusinessStrategy<Resource>());
            }

            // Create and return a new resource entity based on validated data.
            Resource entityAdd = GetResource(entity ?? new Resource());
            return Operation<Resource>.Success(entityAdd);
        }

        /// <summary>
        /// Constructs an error message by concatenating unique error messages from the validation result.
        /// </summary>
        /// <param name = "result">The validation result containing error messages.</param>
        /// <returns>A concatenated string of error messages.</returns>
        private static string GetErrorMessage(ValidationResult result)
        {
            IEnumerable<string> errors = result.Errors.Select(x => x.ErrorMessage).Distinct();
            string errorMessage = string.Join(", ", errors);
            return errorMessage;
        }

        /// <summary>
        /// Creates a new resource entity with default and provided values.
        /// </summary>
        /// <param name = "entity">The original entity containing the data.</param>
        /// <returns>A new instance of a Resource entity with set fields.</returns>
        private static Resource GetResource(Resource entity)
        {
            return new Resource()
            {
                Id = entity.Id,
                Name = entity.Name,
                Value = entity.Value,
                Comment = entity?.Comment ?? string.Empty,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Active = false, // Default value for new resources
            };
        }
    }
}