using FluentValidation;

namespace Application.Identity.Commands;

public class SignUpCommandValidator : AbstractValidator<SignUpCommand>
{
    public SignUpCommandValidator()
    {
        RuleFor(u => u.Email)
            .NotEmpty().WithMessage("Enter your email address")
            .EmailAddress().WithMessage("Invalid mail format");

        RuleFor(u => u.Password)
            .NotEmpty().WithMessage("Enter your password")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long");

        RuleFor(u => u.Name)
            .NotEmpty().WithMessage("Enter your name")
            .WithMessage("Name cannot be empty or whitespace");
            
        RuleFor(u => u.Surname)
            .NotEmpty().WithMessage("Enter your name")
            .WithMessage("Name cannot be empty or whitespace");
    }
}