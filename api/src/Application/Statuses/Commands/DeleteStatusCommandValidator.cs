using FluentValidation;

namespace Application.Statuses.Commands;

public class DeleteStatusCommandValidator: AbstractValidator<DeleteStatusCommand>
{
    public DeleteStatusCommandValidator()
    {
        RuleFor(x => x.ProblemStatusId)
            .NotEmpty()
            .WithMessage("ProblemStatus ID is required");
    }
}