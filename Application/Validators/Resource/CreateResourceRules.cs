namespace Application.Validators.User
{
    using Domain.Entities;
    using FluentValidation;

    /// <summary>
    /// The CreateUserRules class defines the validation rules for creating a User entity.
    /// It extends the AbstractValidator<User> from FluentValidation, allowing us to specify validation logic
    /// using a fluent syntax which makes the code more readable and maintainable.
    /// </summary>
    public class CreateResourceRules : AbstractValidator<Resource>
    {
        public CreateResourceRules()
        {
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