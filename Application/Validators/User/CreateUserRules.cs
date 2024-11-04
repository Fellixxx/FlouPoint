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
    /// The AddUserRequestRules class defines the validation rules for creating a User.
    /// It extends the AbstractValidator<User> from FluentValidation, which provides a fluent interface for defining validation rules.
    /// </summary>
    public class CreateUserRules : AbstractValidator<User>
    {
        /// <summary>
        /// Create the validation for the class addUserRequest.
        /// </summary>
        public CreateUserRules()
        {
            // Defines a validation rule for the 'Username' property of the User entity.
            // The rule states that the 'Username' must not be null, must not be empty, and its length should be between 6 and 50 characters.
            RuleFor(x => x.Name).NotNull().NotEmpty().MinimumLength(6).MaximumLength(50);

            // Defines a validation rule for the 'Password' property of the User entity.
            // The rule states that the 'Password' must not be null, must not be empty, and its length should be between 6 and 100 characters.
            RuleFor(x => x.Password).NotNull().NotEmpty().MinimumLength(6).MaximumLength(100);

            // Defines a validation rule for the 'Email' property of the User entity.
            // The rule states that the 'Email' must not be null, must not be empty, and its length should be between 10 and 100 characters.
            RuleFor(x => x.Email).NotNull().NotEmpty().MinimumLength(10).MaximumLength(100);
        }
    }
}
