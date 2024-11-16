namespace Application.Validators.User
{
    using Domain.Entities;
    using FluentValidation;

    /// <summary>
    /// The CreateResourceRules class defines the validation rules for creating a Resource entity.
    /// It extends the AbstractValidator<Resource> from FluentValidation, allowing us to specify validation logic
    /// using a fluent syntax which makes the code more readable and maintainable.
    /// </summary>
    public class CreateResourceRules : AbstractValidator<Resource>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref = "CreateResourceRules"/> class, 
        /// setting up validation rules for the Resource entity.
        /// </summary>
        public CreateResourceRules()
        {
            // Ensure the Resource Name is not null or empty, and its length is between 6 and 50 characters.
            RuleFor(x => x.Name).NotNull().NotEmpty().MinimumLength(6).MaximumLength(50);
            // Ensure the Resource Value is not null or empty, and its length is between 6 and 100 characters.
            RuleFor(x => x.Value).NotNull().NotEmpty().MinimumLength(6).MaximumLength(100);
            // Ensure the Resource Comment is not null or empty, and its length is between 10 and 100 characters.
            RuleFor(x => x.Comment).NotNull().NotEmpty().MinimumLength(10).MaximumLength(100);
        }
    }
}