using FluentValidation;

namespace Application.Statuses.Commands;

public class UpdateStatusCommandValidator : AbstractValidator<UpdateStatusCommand>
{
    public UpdateStatusCommandValidator()
    {
        RuleFor(x => x.ProblemStatusId)
            .NotEmpty()
            .WithMessage("Problem Status ID is required.");
        
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required.")
            .MaximumLength(255)
            .MinimumLength(3)
            .WithMessage("Name must be between 3 and 255 characters.");
    }
}