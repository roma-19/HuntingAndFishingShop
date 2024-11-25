using Domain.Models;
using FluentValidation;

namespace Domain.Validators;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(u => u.Email)
            .NotEmpty().WithMessage("Электронная почта обязательна")
            .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$").WithMessage("Неверный формат почты");
        
        RuleFor(u => u.Password)
            .NotEmpty().WithMessage("Пароль обязателен")
            .MinimumLength(6).WithMessage("Пароль должен модержать не менее 6 символов");
    }
}

