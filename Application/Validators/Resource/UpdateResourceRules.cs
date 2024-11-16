namespace Application.Validators.User
{
    using Domain.Entities;
    using FluentValidation;

    /// <summary>
    /// The UpdateResourceRules class defines the validation rules for updating a Resource.
    /// It extends AbstractValidator<Resource> from FluentValidation, providing a fluent API for specifying validation logic.
    /// </summary>
    public class UpdateResourceRules : AbstractValidator<Resource>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref = "UpdateResourceRules"/> class.
        /// Defines a set of rules used to validate the properties of the Resource entity.
        /// </summary>
        public UpdateResourceRules()
        {
            // Validate that the 'Id' property is not null or empty
            RuleFor(x => x.Id).NotEmpty().NotNull();
            // Validate that the 'Name' property is not null, not empty, and its length is between 6 and 50 characters
            RuleFor(x => x.Name).NotNull().NotEmpty().MinimumLength(6).MaximumLength(50);
            // Validate that the 'Value' property is not null, not empty, and its length is between 6 and 100 characters
            RuleFor(x => x.Value).NotNull().NotEmpty().MinimumLength(6).MaximumLength(100);
            // Validate that the 'Comment' property is not null, not empty, and its length is between 10 and 100 characters
            RuleFor(x => x.Comment).NotNull().NotEmpty().MinimumLength(10).MaximumLength(100);
        }
    }
}