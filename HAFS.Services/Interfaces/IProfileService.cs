using Domain.Models;

namespace Services.Interfaces;

public interface IProfileService
{
    Task<User> GetUserByEmailAsync(string email);
}