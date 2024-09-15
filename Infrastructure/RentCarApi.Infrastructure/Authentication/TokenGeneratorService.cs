using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using RentCarApi.Application.Common.Interfaces.Authentication;
using RentCarApi.Application.UnitOfWork;
using RentCarApi.Domain.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RentCarApi.Infrastructure.Authentication
{
    public class TokenGeneratorService : ITokenGeneratorService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly JwtSettings _jwtSettings;
        private readonly IUnitOfWork _unitOfWork;

        public TokenGeneratorService(JwtSettings jwtSettings, UserManager<AppUser> userManager, IUnitOfWork unitOfWork)
        {
            _jwtSettings = jwtSettings;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<string> GenerateJwtAccessTokenAsync(List<Claim> claims)
        {
            SigningCredentials signingCredentials = new(new SymmetricSecurityKey(Encoding
                .UTF8.GetBytes(_jwtSettings.Secret)), SecurityAlgorithms.HmacSha256);

            JwtSecurityToken securityToken = new(
                claims: claims,
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                expires: DateTime.Now.AddMinutes(_jwtSettings.ExpiryMinutes),
                signingCredentials: signingCredentials
                );

            return await Task.FromResult(new JwtSecurityTokenHandler().WriteToken(securityToken));
        }
        public async Task<string> GenerateRefreshTokenAsync(List<Claim> claims, int userId)
        {
            var existingRefreshToken = await _unitOfWork.GetReadRepository<RefreshToken>().GetSingleAsync(t => t.AppUserId == userId && t.ExpiryDate > DateTime.UtcNow);

            string newJwtId = claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti)?.Value;

            if (existingRefreshToken != null)
            {
                existingRefreshToken.JwtId = newJwtId;
                existingRefreshToken.ExpiryDate = DateTime.UtcNow.AddDays(7);
            }
            else
            {

                existingRefreshToken = new RefreshToken
                {
                    JwtId = newJwtId,
                    AppUserId = userId,
                    CreatedDate = DateTime.UtcNow,
                    ExpiryDate = DateTime.UtcNow.AddDays(7)
                };
                await _unitOfWork.GetWriteRepository<RefreshToken>().AddAsync(existingRefreshToken);
            }
            await _unitOfWork.SaveChangesAsync();
            return existingRefreshToken.JwtId;
        }

        public async Task<List<Claim>> CreateClaims(AppUser user)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp,DateTime.Now
                .AddMinutes(_jwtSettings.ExpiryMinutes).ToString()),
            };

            IList<string> roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            return claims;
        }
    }
}