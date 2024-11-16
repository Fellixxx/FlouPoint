namespace Application.Validators.User
{
    using Domain.Entities;
    using FluentValidation;

    /// <summary>
    /// The UpdateUserRules class defines the validation rules for updating a User.
    /// It extends AbstractValidator<User> from FluentValidation, providing a fluent API for specifying validation logic.
    /// </summary>
    public class UpdateResourceRules : AbstractValidator<Resource>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref = "UpdateUserRules"/> class.
        /// Defines a set of rules used to validate the properties of the User entity.
        /// </summary>
        public UpdateResourceRules()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull().NotEmpty();
            RuleFor(x => x.Name).NotNull()
            .NotEmpty()
            .MinimumLength(6)
            .MaximumLength(50);
            RuleFor(x => x.Value).NotNull()
            .NotEmpty()
            .MinimumLength(6)
            .MaximumLength(100);
            RuleFor(x => x.Comment).NotNull()
            .NotEmpty()
            .MinimumLength(10)
            .MaximumLength(100);
        }
    }
}