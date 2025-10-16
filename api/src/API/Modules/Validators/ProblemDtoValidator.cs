using API.DTOs.Problems;
using FluentValidation;

namespace API.Modules.Validators;

public class ProblemDtoValidator : AbstractValidator<CreateProblemDto>
{
    public ProblemDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(255)
            .MinimumLength(3)
            .WithMessage("Title must be between 3 and 255 characters.");

        RuleFor(x => x.Latitude)
            .NotEmpty()
            .InclusiveBetween(-90, 90)
            .WithMessage("Latitude must be between -90 and 90 degrees.");

        RuleFor(x => x.Longitude)
            .NotEmpty()
            .InclusiveBetween(-180, 180)
            .WithMessage("Longitude must be between -180 and 180 degrees.");

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(2000)
            .MinimumLength(10)
            .WithMessage("Description must be between 10 and 2000 characters.");

        RuleFor(x => x.ProblemStatusId)
            .NotEmpty()
            .WithMessage("Problem status must be specified.");
    }
}