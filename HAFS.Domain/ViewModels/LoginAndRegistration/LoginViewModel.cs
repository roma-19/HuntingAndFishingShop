using System.ComponentModel.DataAnnotations;
namespace Domain.ViewModels.LoginAndRegistration;
public class LoginViewModel
{
    [Required(ErrorMessage = "Введите почту")]
    [RegularExpression(
        @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$",
        ErrorMessage = "Некорректный адрес электронной почты")]
    public string? Email { get; set; }
    
    [Required(ErrorMessage = "Введите пароль")]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    public string? Password { get; set; }
}

