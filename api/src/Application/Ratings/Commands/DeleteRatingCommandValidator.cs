using FluentValidation;

namespace Application.Ratings.Commands;

public class DeleteRatingCommandValidator : AbstractValidator<DeleteRatingCommand>
{
    public DeleteRatingCommandValidator()
    {
        RuleFor(x => x.RatingId)
            .NotEmpty()
            .WithMessage("Rating ID is required");
    }
}