using System.Security.Claims;
using Schedule.Core.Models;

namespace Schedule.Application.Common.Interfaces;

public interface ITokenService
{
    ClaimsPrincipal GetPrincipalFromExpiredToken(string accessToken);
    string GenerateAccessToken(User user, Guid sessionId);
    string GenerateRefreshToken();
}