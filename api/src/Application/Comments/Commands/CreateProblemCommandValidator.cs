using FluentValidation;

namespace Application.Comments.Commands;

public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
{
    public CreateCommentCommandValidator()
    {
        RuleFor(x => x.Content)
            .NotEmpty()
            .WithMessage("Content is required.")
            .MaximumLength(300)
            .WithMessage("Content must not exceed 300 characters.")
            .MinimumLength(3)
            .WithMessage("Content must be at least 3 characters long.");

        RuleFor(x => x.ProblemId)
            .NotEmpty()
            .WithMessage("ProblemId is required.");
    }
}