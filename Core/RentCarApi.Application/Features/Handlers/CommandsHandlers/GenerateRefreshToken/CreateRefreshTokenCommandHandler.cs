using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RentCarApi.Application.Common.Interfaces.Authentication;
using RentCarApi.Application.Features.Commands.GenerateRefreshToken.Create;
using RentCarApi.Application.UnitOfWork;
using RentCarApi.Domain.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;

namespace RentCarApi.Application.Features.Handlers.CommandsHandlers.GenerateRefreshToken
{
    public class CreateRefreshTokenCommandHandler : IRequestHandler<RefreshTokenCreateCommandRequest, RefreshTokenCreateCommandResponse>
    {
        private readonly UserManager<RentCarApi.Domain.Models.AppUser> _userManager;
        private readonly IUnitOfWork _unit;
        private readonly ITokenGeneratorService _tokenGenerator;

        public CreateRefreshTokenCommandHandler(ITokenGeneratorService tokenGenerator ,IUnitOfWork unit, UserManager<Domain.Models.AppUser> userManager)
        {
            _tokenGenerator = tokenGenerator;
            _unit = unit;
            _userManager = userManager;
        }

        public async Task<RefreshTokenCreateCommandResponse> Handle(RefreshTokenCreateCommandRequest request, CancellationToken cancellationToken)
        {
            var refreshToken = await _unit.GetReadRepository<RefreshToken>().GetAsync(r=>r.JwtId==request.RefreshToken, true, query=>query.Include(a=>a.AppUser));

            if (refreshToken == null || refreshToken.ExpiryDate < DateTime.UtcNow)
            {
                throw new AuthenticationException("Refresh token has expired or is invalid.");
            }

            List<Claim> claims = await _tokenGenerator.CreateClaims(refreshToken.AppUser);
            string newJwtToken = await _tokenGenerator.GenerateJwtAccessTokenAsync(claims);
            string newRefreshToken = await _tokenGenerator.GenerateRefreshTokenAsync(claims, refreshToken.AppUser.Id);

            refreshToken.JwtId = newRefreshToken;
            refreshToken.ExpiryDate = refreshToken.ExpiryDate.AddDays(7); 

            await _unit.SaveChangesAsync();

            return new RefreshTokenCreateCommandResponse
            {
                AccessToken = newJwtToken,
                RefreshToken = newRefreshToken
            };
        }



    }
}
