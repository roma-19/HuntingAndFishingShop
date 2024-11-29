using System.Security.Claims;
using Domain.Response;
using Domain.Models;

namespace Services.Interfaces;

public interface IAccountService
{
    Task<BaseResponse<ClaimsIdentity>> Login(User model);
    Task<BaseResponse<string>> Register(User model);
    Task<BaseResponse<ClaimsIdentity>> ConfirmEmail(User model, string code, string confirmCode);
    Task<BaseResponse<ClaimsIdentity>> IsCreatedAccount(User model);
}

