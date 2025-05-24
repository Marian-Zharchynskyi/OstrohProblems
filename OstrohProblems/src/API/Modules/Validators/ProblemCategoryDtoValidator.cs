using API.DTOs;
using API.DTOs.Statuses;
using FluentValidation;

namespace API.Modules.Validators;

public class ProblemCategoryDtoValidator : AbstractValidator<StatusDto>
{
    public ProblemCategoryDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(255).MinimumLength(3);
    }
}