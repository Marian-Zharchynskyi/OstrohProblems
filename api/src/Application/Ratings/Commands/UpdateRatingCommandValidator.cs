using FluentValidation;

namespace Application.Ratings.Commands;

public class UpdateRatingCommandValidator : AbstractValidator<UpdateRatingCommand>
{
    public UpdateRatingCommandValidator()
    {
        RuleFor(x => x.RatingId)
            .NotEmpty()
            .WithMessage("Rating ID is required.");

        RuleFor(x => x.Points)
            .InclusiveBetween(1.0, 5.0)
            .WithMessage("Points must be between 1.0 and 5.0.");
    }
}