namespace Domain.ViewModels.User;

public class UserProfileViewModel
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? PhotoUrl { get; set; }
}