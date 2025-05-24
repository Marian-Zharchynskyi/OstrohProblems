using API.DTOs;
using API.DTOs.Comments;
using FluentValidation;

namespace API.Modules.Validators;

public class CommentDtoValidator : AbstractValidator<CommentDto>
{
    public CommentDtoValidator()
    {
        RuleFor(x => x.Content)
            .NotEmpty()
            .WithMessage("Content is required.")
            .MaximumLength(1000)
            .WithMessage("Content must not exceed 1000 characters.")
            .MinimumLength(3)
            .WithMessage("Content must be at least 3 characters long.");

        RuleFor(x => x.ProblemId)
            .NotEmpty()
            .WithMessage("ProblemId is required.");
    }
}