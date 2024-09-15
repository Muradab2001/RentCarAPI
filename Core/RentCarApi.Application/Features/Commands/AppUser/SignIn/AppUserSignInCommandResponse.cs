namespace RentCarApi.Application.Features.Commands.AppUser.SignIn
{
    public class AppUserSignInCommandResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string RefreshToken { get; set; }
        public string AccessToken { get; set; }
    }
}