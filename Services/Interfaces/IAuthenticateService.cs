using BusinessObjects.Requests.Authentication;
using BusinessObjects.Responses;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Services.Interfaces
{
    public interface IAuthenticateService
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        Task<TokenModel?> Login(LoginRequest loginModel);
        Task<IdentityResult> RegisterUser(RegisterRequest registerModel);
        Task<TokenModel?> RefreshToken(TokenModel tokenModel);
    }
}
