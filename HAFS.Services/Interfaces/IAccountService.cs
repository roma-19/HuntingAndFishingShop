using System.Security.Claims;
using Domain.Response;
using Domain.Models;

namespace Services.Interfaces;

public interface IAccountService
{
    Task<BaseResponce<ClaimsIdentity>> Login(User model);
    Task<BaseResponce<ClaimsIdentity>> Register(User model);
}

