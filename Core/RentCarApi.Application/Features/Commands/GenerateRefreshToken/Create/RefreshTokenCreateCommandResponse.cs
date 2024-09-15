namespace RentCarApi.Application.Features.Commands.GenerateRefreshToken.Create
{
    public class RefreshTokenCreateCommandResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}