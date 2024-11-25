using Domain.Models;
using FluentValidation;

namespace Domain.Validators;

public class OrderValidator : AbstractValidator<Order>
{
    public OrderValidator()
    {
        // RuleFor(o => o.Id)
        //     .NotEmpty().WithMessage("Идентификатор обязателен");
        //
        // RuleFor(o => o.UserId)
        //     .NotEmpty().WithMessage("Идентификатор обязателен");
        //
        // RuleFor(o => o.ProductId)
        //     .NotEmpty().WithMessage("Идентификатор обязателен");
        
        RuleFor(o => o.Name)
            .NotEmpty().WithMessage("Название заказа обязательно")
            .Length(3, 50).WithMessage("Название заказа должно иметь от 3 до 50 символов");
        
        RuleFor(o => o.Quantity)
            .NotEmpty().WithMessage("Кол-во товара обязательно")
            .GreaterThan(0).WithMessage("Кол-во товара должно быть больше нуля");

        RuleFor(o => o.Price)
            .NotEmpty().WithMessage("Цена обязательна")
            .GreaterThan(0).WithMessage("Цена должна быть больше нуля")
            .PrecisionScale(6, 2, true).WithMessage("У цены не должно быть больше 2 знаков после нуля");
    }
}