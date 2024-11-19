using System.ComponentModel.DataAnnotations;

namespace Domain.Enum;

public enum Role
{
    [Display(Name = "User")]
    User = 0,
    
    [Display(Name = "Moderator")]
    Moderator = 1,
    
    [Display(Name = "Admin")]
    Admin = 2,
}