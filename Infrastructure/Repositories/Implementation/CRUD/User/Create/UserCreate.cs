
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
    using Application.UseCases.Repository;
    using Infrastructure.Repositories.Abstract.CRUD.Create;
    using Infrastructure.Repositories;
    using Application.UseCases.ExternalServices.Resorces;

    /// <summary>
    /// Implementation of the user creation repository.
    /// </summary>
    public class UserCreate : CreateRepository<User>, IUserCreate
    {
        private readonly IResorcesProvider _resourceProvider;
        private IResourceHandler _resourceHandler;
        private readonly List<string> _resourceKeys;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserCreate"/> class.
        /// </summary>
        /// <param name="context">The database context for the application.</param>
        /// <param name="logService">The logging service for tracking operations.</param>
        public UserCreate(DataContext context, ILogService logService, IUtilEntity<User> utilEntity, IResorcesProvider resourceProvider, IResourceHandler resourceHandler) : base(context, logService, utilEntity, resourceProvider, resourceHandler)
        {
            _resourceProvider = resourceProvider;
            _resourceHandler = resourceHandler;
            _resourceKeys =
            [
                "FailedDataSizeCharacter",
                "FailedEmailInvalidFormat",
                "FailedAlreadyRegisteredEmail"
            ];
        }

        /// <summary>
        /// Method to create a new user entity and persist it in the database.
        /// </summary>
        /// <param name="entity">The user entity to be created and stored.</param>
        /// <returns>An operation result indicating the outcome of the user creation.</returns>
        protected override async Task<Operation<User>> CreateEntity(User entity)
        {
            // Validate the user entity using the defined rules
            CreateUserRules validatorAdd = new CreateUserRules();
            ValidationResult result = validatorAdd.Validate(entity);
            await ResourceHandler.CreateAsync(_resourceProvider, _resourceKeys);

            if (!result.IsValid)
            {
                string errorMessage = GetErrorMessage(result);
                var failedDataSizeCharacter = _resourceHandler.GetResource("FailedDataSizeCharacter");
                return OperationBuilder<User>.FailBusiness(string.Format(failedDataSizeCharacter, errorMessage));
            }

            // Check for a valid email format
            var email = entity?.Email ?? string.Empty;
            if (!CredentialUtility.IsValidEmail(email))
            {
                var failedEmailInvalidFormat = _resourceHandler.GetResource("FailedEmailInvalidFormat");
                return OperationBuilder<User>.FailBusiness(failedEmailInvalidFormat);
            }

            // Ensure email uniqueness by checking if it's already used by another user
            IQueryable<User> userByEmail = await ReadFilter(p => p.Email == email);
            User? userExistByEmail = userByEmail?.FirstOrDefault();
            if (userExistByEmail is not null)
            {
                var failedEmailInvalidFormat = _resourceHandler.GetResource("FailedAlreadyRegisteredEmail");
                return OperationBuilder<User>.FailBusiness(failedEmailInvalidFormat);
            }

            // Create and return the user entity
            User entityAdd = GetUser(entity ?? new User());
            return Operation<User>.Success(entityAdd);
        }

        /// <summary>
        /// Extracts and consolidates validation error messages.
        /// </summary>
        /// <param name="result">The validation result that may contain multiple error messages.</param>
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
        /// <param name="entity">The original user entity.</param>
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
