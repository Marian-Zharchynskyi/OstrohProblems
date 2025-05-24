using FluentValidation;

namespace Application.Comments.Commands;

public class DeleteCommentCommandValidator : AbstractValidator<DeleteCommentCommand>
{
    public DeleteCommentCommandValidator()
    {
        RuleFor(x => x.CommentId)
            .NotEmpty()
            .WithMessage("Comment ID is required");
    }
}