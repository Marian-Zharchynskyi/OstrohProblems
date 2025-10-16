using FluentValidation;

namespace Application.Comments.Commands;

public class UpdateCommentCommandValidator : AbstractValidator<UpdateCommentCommand>
{
    public UpdateCommentCommandValidator()
    {
        RuleFor(x => x.CommentId)
            .NotEmpty()
            .WithMessage("Comment ID is required.");

        RuleFor(x => x.Content)
            .NotEmpty()
            .WithMessage("Content is required.")
            .MinimumLength(3)
            .MaximumLength(1000)
            .WithMessage("Content must be between 3 and 1000 characters.");
    }
}