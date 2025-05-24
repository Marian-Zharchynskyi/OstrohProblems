using FluentValidation;

namespace Application.Ratings.Commands;

public class CreateRatingCommandValidator : AbstractValidator<CreateRatingCommand>
{
    public CreateRatingCommandValidator()
    {
        RuleFor(x => x.ProblemId)
            .NotEmpty()
            .WithMessage("ProblemId is required.");

        RuleFor(x => x.Points)
            .InclusiveBetween(1.0, 5.0)
            .WithMessage("Points must be between 1.0 and 5.0.");
    }
}