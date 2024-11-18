namespace Infrastructure.Repositories.Implementation.CRUD.User.Create
{
    using Application.Result;
    using Application.UseCases.CRUD.User;
    using Application.UseCases.ExternalServices;
    using Application.Validators.User;
    using Domain.Entities;
    using Persistence.BaseDbContext;
    using UtilitiesLayer;
    using FluentValidation.Results;
    using Application.UseCases.Repository.CRUD;
    using Infrastructure.Repositories.Abstract.CRUD.Create;
    using Infrastructure.Repositories;
    using Application.UseCases.ExternalServices.Resources;
    using Application.UseCases.ExternalServices.Resources.Provider;

    /// <summary>
    /// Implementation of the user creation repository.
    /// </summary>
    public class UserCreate : CreateRepository<User>, IUserCreate
    {
        private readonly IResourcesProvider _provider;
        private IResourceHandler _handler;
        protected List<string> _resourceKeys;
        /// <summary>
        /// Initializes a new instance of the <see cref = "UserCreate"/> class.
        /// </summary>
        /// <param name = "context">The database context for the application.</param>
        /// <param name = "logService">The logging service for tracking operations.</param>
        /// <param name = "utilEntity">Utility entity used for management.</param>
        /// <param name = "provider">Resource provider for localization or resource access.</param>
        /// <param name = "handler">Resource handler for managing resources and messaging.</param>
        public UserCreate(DataContext context, ILogService logService, IUtilEntity<User> utilEntity, IResourcesProvider provider, IResourceHandler handler) : base(context, logService, utilEntity, provider, handler)
        {
            _provider = provider;
            _handler = handler;
            SetResourceKeys();
        }

        protected virtual void SetResourceKeys()
        {
            _resourceKeys =
            [
                "CreateFailedDataSizeCharacter",
                "CreateFailedEmailInvalidFormat",
                "CreateFailedAlreadyRegisteredEmail"
            ];
        }

        /// <summary>
        /// Method to create a new user entity and persist it in the database.
        /// </summary>
        /// <param name = "entity">The user entity to be created and stored.</param>
        /// <returns>An operation result indicating the outcome of the user creation.</returns>
        protected override async Task<Operation<User>> CreateEntity(User entity)
        {
            // Validate the user entity using the defined rules
            CreateUserRules validatorAdd = new CreateUserRules();
            ValidationResult result = validatorAdd.Validate(entity);
            await ResourceHandler.CreateAsync(_provider, _resourceKeys);
            // Check if validation fails and return an error if so
            if (!result.IsValid)
            {
                string errorMessage = GetErrorMessage(result);
                var createFailedDataSizeCharacter = _handler.GetResource("CreateFailedDataSizeCharacter");
                var message = string.Format(createFailedDataSizeCharacter, errorMessage);
                var business = new BusinessStrategy<User>();
                return OperationStrategy<User>.Fail(message, business);
            }

            // Check for a valid email format
            var email = entity?.Email ?? string.Empty;
            if (!CredentialUtility.IsValidEmail(email))
            {
                var createFailedEmailInvalidFormat = _handler.GetResource("CreateFailedEmailInvalidFormat");
                return OperationStrategy<User>.Fail(createFailedEmailInvalidFormat, new BusinessStrategy<User>());
            }

            // Ensure email uniqueness by checking if it's already used by another user
            IQueryable<User> userByEmail = await ReadFilter(p => p.Email == email);
            User? userExistByEmail = userByEmail?.FirstOrDefault();
            if (userExistByEmail is not null)
            {
                var createFailedAlreadyRegisteredEmail = _handler.GetResource("CreateFailedAlreadyRegisteredEmail");
                return OperationStrategy<User>.Fail(createFailedAlreadyRegisteredEmail, new BusinessStrategy<User>());
            }

            // Create and return the user entity
            User entityAdd = GetUser(entity ?? new User());
            return Operation<User>.Success(entityAdd);
        }

        /// <summary>
        /// Extracts and consolidates validation error messages.
        /// </summary>
        /// <param name = "result">The validation result that may contain multiple error messages.</param>
        /// <returns>A single string consolidating all error messages.</returns>
        private static string GetErrorMessage(ValidationResult result)
        {
            IEnumerable<string> errors = result.Errors.Select(x => x.ErrorMessage).Distinct();
            string errorMessage = string.Join(", ", errors);
            return errorMessage;
        }

        /// <summary>
        /// Prepares the user entity for creation by setting certain properties.
        /// </summary>
        /// <param name = "entity">The original user entity.</param>
        /// <returns>A new user entity with additional properties set.</returns>
        private static User GetUser(User entity)
        {
            return new User()
            {
                Id = entity.Id,
                Name = entity.Name,
                Password = CredentialUtility.ComputeSha256Hash(entity?.Password ?? string.Empty),
                Email = entity?.Email ?? string.Empty,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Active = false,
            };
        }
    }
}