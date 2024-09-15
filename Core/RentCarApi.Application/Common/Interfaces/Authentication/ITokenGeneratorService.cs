using RentCarApi.Domain.Models;
using System.Security.Claims;

namespace RentCarApi.Application.Common.Interfaces.Authentication
{
    public interface ITokenGeneratorService 
    {
        Task<List<Claim>> CreateClaims(AppUser user);
        Task<string> GenerateJwtAccessTokenAsync(List<Claim> claims);
        Task<string> GenerateRefreshTokenAsync(List<Claim> claims, int userId);
    }
}