using FluentValidation;

namespace Application.Problems.Commands;

public class CreateProblemCommandValidator: AbstractValidator<CreateProblemCommand>
{
    public CreateProblemCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Title is required.")
            .MaximumLength(255)
            .MinimumLength(3)
            .WithMessage("Title must be between 3 and 255 characters.");

        RuleFor(x => x.Latitude)
            .InclusiveBetween(-90, 90)
            .WithMessage("Latitude must be between -90 and 90 degrees.");

        RuleFor(x => x.Longitude)
            .InclusiveBetween(-180, 180)
            .WithMessage("Longitude must be between -180 and 180 degrees.");
        
        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description is required.")
            .MinimumLength(3)
            .MaximumLength(2000)
            .WithMessage("Description must be between 3 and 2000 characters.");

        RuleFor(x => x.StatusId)
            .NotNull()
            .WithMessage("Problem status is required.");
        
        RuleFor(x => x.ProblemCategoryIds)
            .NotNull()
            .WithMessage("Problem category list should not be null.");
    }
}