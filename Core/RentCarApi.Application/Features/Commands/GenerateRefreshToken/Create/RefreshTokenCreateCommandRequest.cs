using MediatR;

namespace RentCarApi.Application.Features.Commands.GenerateRefreshToken.Create
{
    public class RefreshTokenCreateCommandRequest : IRequest<RefreshTokenCreateCommandResponse>
    {
        public string RefreshToken { get; set; }
    }
}