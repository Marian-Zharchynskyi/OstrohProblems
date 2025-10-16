using FluentValidation;

namespace Application.Problems.Commands;

public class DeleteProblemCommandValidator : AbstractValidator<DeleteProblemCommand>
{
    public DeleteProblemCommandValidator()
    {
        RuleFor(x => x.ProblemId)
            .NotEmpty()
            .WithMessage("Problem Id is required.");
    }
}