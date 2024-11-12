
namespace Infrastructure.Repositories.Implementation.CRUD.User
{
    using Application.Result;
    using Application.UseCases.CRUD.User;
    using Application.UseCases.ExternalServices;
    using Application.Validators.User;
    using Domain.Entities;
    using Infrastructure.Repositories.Abstract.CRUD;
    using Persistence.BaseDbContext;
    using UtilitiesLayer;
    using FluentValidation.Results;
    using Application.UseCases.Repository.CRUD;
    using Application.UseCases.Repository;

    /// <summary>
    /// Implementation of the user creation repository.
    /// </summary>
    public class UserCreate : CreateRepository<User>, IUserCreate
    {
        private readonly IResourceProvider _resourceProvider;
        private IResourceHandler _resourceHandler;
        private readonly List<string> _resourceKeys;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserCreate"/> class.
        /// </summary>
        /// <param name="context">The database context for the application.</param>
        /// <param name="logService">The logging service for tracking operations.</param>
        public UserCreate(CommonDbContext context, ILogService logService, IUtilEntity<User> utilEntity, IResourceProvider resourceProvider, IResourceHandler resourceHandler) : base(context, logService, utilEntity, resourceProvider, resourceHandler)
        {
        }

        /// <summary>
        /// Method to create a new user entity and persist it in the database.
        /// </summary>
        /// <param name="entity">The user entity to be created and stored.</param>
        /// <returns>An operation result indicating the outcome of the user creation.</returns>
        protected override async Task<OperationResult<User>> CreateEntity(User entity)
        {
            // Validate the user entity using the defined rules
            CreateUserRules validatorAdd = new CreateUserRules();
            ValidationResult result = validatorAdd.Validate(entity);

            // Return an error result if validation fails
            if (!result.IsValid)
            {
                string errorMessage = GetErrorMessage(result);
                return OperationBuilder<User>.FailureBusinessValidation(string.Format(Resource.FailedDataSizeCharacter, errorMessage));
            }

            // Check for a valid email format
            var email = entity?.Email ?? string.Empty;
            if (!CredentialUtility.IsValidEmail(email))
            {
                return OperationBuilder<User>.FailureBusinessValidation(Resource.FailedEmailInvalidFormat);
            }

            // Ensure email uniqueness by checking if it's already used by another user
            IQueryable<User> userByEmail = await ReadFilter(p => p.Email == email);
            User? userExistByEmail = userByEmail?.FirstOrDefault();
            if (userExistByEmail is not null)
            {
                return OperationBuilder<User>.FailureBusinessValidation(Resource.FailedAlreadyRegisteredEmail);
            }

            // Create and return the user entity
            User entityAdd = GetUser(entity ?? new User());
            return OperationResult<User>.Success(entityAdd);
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
