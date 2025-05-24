using FluentValidation;

namespace Application.Categories.Commands;

public class DeleteCategoryCommandValidator: AbstractValidator<DeleteCategoryCommand>
{
    public DeleteCategoryCommandValidator()
    {
        RuleFor(x => x.ProblemCategoryId)
            .NotEmpty()
            .WithMessage("ProblemStatus ID is required");
    }
}