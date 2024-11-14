using System.ComponentModel.DataAnnotations;
namespace Domain.ViewModels.LoginAndRegistration;
public class RegisterViewModel
{
    [Required(ErrorMessage = "Укажите имя 3-20 символов")]
    [MaxLength(20, ErrorMessage = "Имя должно иметь длину меньше 20 символов")]
    [MinLength(3, ErrorMessage = "Имя должно иметь длину более 3 символов")]
    public string? Login { get; set; }
    
    [Required(ErrorMessage = "Укажите почту")]
    [RegularExpression(
        @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$",
        ErrorMessage = "Некорректный адрес электронной почты")]
    public string? Email { get; set; }
    
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Укажите пароль")]
    [MinLength(6, ErrorMessage = "Пароль должен иметь длину больше 6 символов")]
    public string? Password { get; set; }
    
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Подтвердите пароль")]
    [Compare("Password", ErrorMessage = "Пароли не совпадают")]
    public string? PasswordConfirm { get; set; }
}