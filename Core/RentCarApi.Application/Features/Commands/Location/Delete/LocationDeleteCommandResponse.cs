namespace RentCarApi.Application.Features.Commands.Location.Delete
{
    public class LocationDeleteCommandResponse
    {
        public bool Succeeded { get; set; } = true;
        public string Message { get; set; }
    }
}