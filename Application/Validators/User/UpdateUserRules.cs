namespace Application.Validators.User
{
    using Domain.Entities;
    using FluentValidation;

    /// <summary>
    /// The UpdateUserRules class defines the validation rules for updating a User.
    /// It extends AbstractValidator<User> from FluentValidation, providing a fluent API for specifying validation logic.
    /// </summary>
    public class UpdateUserRules : AbstractValidator<User>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref = "UpdateUserRules"/> class.
        /// Defines a set of rules used to validate the properties of the User entity.
        /// </summary>
        public UpdateUserRules()
        {
            // Validation rule for the 'Id' property:
            // This rule ensures that the 'Id' must not be null, empty, or be less than or equal to 0.
            // It helps guarantee that a valid identifier is provided for user modification.
            RuleFor(x => x.Id).NotEmpty().NotNull().NotEmpty();
            // Validation rule for the 'Name' property:
            // This rule ensures that the 'Name' must not be null or empty, 
            // and its length must be between 6 and 50 characters.
            // This check prevents issues with names that are too short or excessively long.
            RuleFor(x => x.Name).NotNull().NotEmpty().MinimumLength(6).MaximumLength(50);
            // Validation rule for the 'Password' property:
            // This rule mandates that the 'Password' must not be null or empty,
            // and its length must be between 6 and 100 characters.
            // This helps enforce basic password strength requirements.
            RuleFor(x => x.Password).NotNull().NotEmpty().MinimumLength(6).MaximumLength(100);
            // Validation rule for the 'Email' property:
            // Ensuring that the 'Email' must not be null or empty,
            // with a length requirement between 10 and 100 characters.
            // This ensures email addresses are provided in a reasonable format and length.
            RuleFor(x => x.Email).NotNull().NotEmpty().MinimumLength(10).MaximumLength(100);
        // Note: Additional considerations for more complex validation (e.g., email format validation)
        // could be added using FluentValidation's other capabilities.
        }
    }
}