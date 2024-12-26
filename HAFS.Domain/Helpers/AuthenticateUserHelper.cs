using System.Security.Claims;
using Domain.ModelsDb;

namespace Domain.Helpers;

public class AuthenticateUserHelper
{
    public static ClaimsIdentity Authenticate(UserDb userDb)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, userDb.Email),
            new Claim(ClaimTypes.Name, userDb.Login),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, userDb.Role.ToString()),
            new Claim("AvatarPath", userDb.PathImage),
        };
        return new ClaimsIdentity(claims, "ApplicationCookie",ClaimTypes.Email, ClaimsIdentity.DefaultRoleClaimType);
    }
}

