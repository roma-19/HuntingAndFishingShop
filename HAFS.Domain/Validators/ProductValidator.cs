using Domain.Models;
using FluentValidation;

namespace Domain.Validators;

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        // RuleFor(p => p.Id)
        //     .NotEmpty().WithMessage("Идентификатор обязателен");
        //
        // RuleFor(p => p.CategoryId)
        //     .NotEmpty().WithMessage("Идентификатор обязателен");
        
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("Название товара обязательно")
            .Length(3, 50).WithMessage("Название товара должно иметь от 3 до 50 символов");
        
        RuleFor(p => p.Description)
            .NotEmpty().WithMessage("Описание обязательно");
        
        RuleFor(p => p.Price)
            .NotEmpty().WithMessage("Цена обязательна")
            .GreaterThan(0).WithMessage("Цена должна быть больше нуля")
            .PrecisionScale(6, 2, true).WithMessage("У цены не должно быть больше 2 знаков после нуля");
        
        RuleFor(p => p.Quantity)
            .NotEmpty().WithMessage("Кол-во товара обязательно")
            .GreaterThan(0).WithMessage("Кол-во товара должно быть больше нуля");
    }
}