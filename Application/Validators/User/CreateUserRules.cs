using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.User
{
    using Domain.Entities;
    using FluentValidation;

    /// <summary>
    /// The CreateUserRules class defines the validation rules for creating a User entity.
    /// It extends the AbstractValidator<User> from FluentValidation, allowing us to specify validation logic
    /// using a fluent syntax which makes the code more readable and maintainable.
    /// </summary>
    public class CreateUserRules : AbstractValidator<User>
    {
        /// <summary>
        /// Initializes a new instance of the CreateUserRules class, where the validation rules for a User are defined.
        /// These rules enforce constraints on properties like Username, Password, and Email.
        /// </summary>
        public CreateUserRules()
        {
            // Defines a validation rule for the 'Username' property of the User entity.
            // This rule ensures that 'Name' is neither null nor empty, and its length is between 6 and 50 characters.
            RuleFor(x => x.Name).NotNull() // Username must not be null.
            .NotEmpty() // Username must not be empty.
            .MinimumLength(6) // Username must be at least 6 characters long.
            .MaximumLength(50); // Username must not exceed 50 characters.
            // Defines a validation rule for the 'Password' property of the User entity.
            // This rule ensures that 'Password' is neither null nor empty, and its length is between 6 and 100 characters.
            RuleFor(x => x.Password).NotNull() // Password must not be null.
            .NotEmpty() // Password must not be empty.
            .MinimumLength(6) // Password must be at least 6 characters long.
            .MaximumLength(100); // Password must not exceed 100 characters.
            // Defines a validation rule for the 'Email' property of the User entity.
            // This rule ensures that 'Email' is neither null nor empty, and its length is between 10 and 100 characters.
            RuleFor(x => x.Email).NotNull() // Email must not be null.
            .NotEmpty() // Email must not be empty.
            .MinimumLength(10) // Email must be at least 10 characters long.
            .MaximumLength(100); // Email must not exceed 100 characters.
        }
    }
}