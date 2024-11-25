using Domain.Models;
using FluentValidation;

namespace Domain.Validators;

public class CategoryValidator : AbstractValidator<Category>
{
    public CategoryValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Название категории обязательно")
            .Length(3, 50).WithMessage("Название категории должно иметь от 3 до 50 символов")
            .Matches(@"^[A-Za-zА-Яа-я]+$").WithMessage("Неверный формат названия категории");
    }
}

