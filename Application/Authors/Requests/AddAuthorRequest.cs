using Application.Books.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authors.Requests
{
    public class AddAuthorRequest
    {
        [MinLength(3, ErrorMessage = "The length of firstname must be at least 3 characters. ")]
        [Required(ErrorMessage = "The Firstname is required")]
        public string Firstname { get; set; }
        [MinLength(3, ErrorMessage = "The length of lastname must be at least 3 characters. ")]
        [Required(ErrorMessage = "The Lastname is required")]
        public string Lastname { get; set; }
        [Required(ErrorMessage = "The Birth Year is required")]
        public int BirthYear { get; set; }
    }

    public class AddAuthorRequestValidator : AbstractValidator<AddAuthorRequest>
    {
        public AddAuthorRequestValidator()
        {
            RuleFor(x => x.Firstname)
                .MinimumLength(3).WithErrorCode("The length of firstname must be at least 3 characters. ")
                .NotNull()
                .NotEmpty();
            RuleFor(x => x.Lastname)
                .MinimumLength(3).WithErrorCode("The length of lastname must be at least 3 characters. ")
                .NotNull()
                .NotEmpty();
            RuleFor(x => x.BirthYear)
                .GreaterThanOrEqualTo(0).WithErrorCode("Birth Year must be at least 0")
                .NotNull();
        }
    }
}
