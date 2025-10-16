using FluentValidation;

namespace Application.Categories.Commands;

public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(x => x.ProblemCategoryId)
            .NotEmpty()
            .WithMessage("Problem category ID is required.");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required.")
            .MaximumLength(255)
            .MinimumLength(3)
            .WithMessage("Name must be between 3 and 255 characters.");
    }
}