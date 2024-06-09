using Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Books.Requests
{
    public class AddBookRequest
    {
        [MinLength(3, ErrorMessage = "he length of Title must be at least 3 characters. ")]
        [Required(ErrorMessage = "The Title is required")]
        public string Title { get; set; }
        [MinLength(6, ErrorMessage = "The length of Description must be at least 6 characters. ")]
        [Required(ErrorMessage = "The Description is required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "The Rating is required")]
        public double Rating { get; set; }
        [Required(ErrorMessage = "The Publish Year is required")]
        public int PublishYear { get; set; }
        [Required(ErrorMessage = "The Taken status is required")]
        public bool IsTaken { get; set; }
        [Required(ErrorMessage = "The Image is required")]
        public IFormFile Image { get; set; }
        [Required(ErrorMessage = "The Author/s field is required.")]
        public IEnumerable<string> AuthorIds { get; set; }
    }

    public class AddBookRequestValidator : AbstractValidator<AddBookRequest>
    {
        public AddBookRequestValidator()
        {
            RuleFor(x => x.Title)
                .MinimumLength(3).WithErrorCode("The length of Title must be at least 3 characters. ")
                .NotNull()
                .NotEmpty();
            RuleFor(x => x.Description)
                .MinimumLength(6).WithErrorCode("The length of Description must be at least 6 characters. ")
                .NotNull()
                .NotEmpty();
            RuleFor(x => x.Rating)
                .GreaterThanOrEqualTo(0).WithErrorCode("The rating must be at least 0")
                .NotNull();
            RuleFor(x => x.PublishYear)
                .GreaterThanOrEqualTo(0).WithErrorCode("Publish Year must be at least 0")
                .NotNull();
            RuleFor(x => x.Image)
                .NotNull()
                .NotEmpty();
            RuleFor(x => x.AuthorIds)
                .NotNull()
                .NotEmpty();
        }
    }
}
