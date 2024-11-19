using Domain.Response;
using Domain.Models;

namespace Services.Interfaces;

public interface IAccountService
{
    Task<BaseResponce<User>> Login(User model);
    
    Task<BaseResponce<User>> Register(User model);
}