using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Auth.Requests
{
    public class RegisterRequest
    {
        [MinLength(6, ErrorMessage = "The length of Email must be at least 6 characters. ")]
        [EmailAddress]
        [Required(ErrorMessage = "The Email is required")]
        public string Email { get; set; }
        [MinLength(6, ErrorMessage = "The length of Username must be at least 6 characters. ")]
        [Required(ErrorMessage = "The Username is required")]
        public string UserName { get; set; }
        [MinLength(6, ErrorMessage = "The length of Password must be at least 6 characters. ")]
        [Required(ErrorMessage = "The Password is required")]
        public string Password { get; set; }
    }

    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .MinimumLength(6).WithErrorCode("The length of Email must be at least 6 characters. ")
                .NotNull()
                .NotEmpty();
            RuleFor(x => x.UserName)
                .MinimumLength(6).WithErrorCode("The UserName of UserName must be at least 6 characters. ")
                .NotNull()
                .NotEmpty();
            RuleFor(x => x.Password)
                .MinimumLength(6).WithErrorCode("The length of Password must be at least 6 characters. ")
                .NotNull()
                .NotEmpty();
        }
    }
}
