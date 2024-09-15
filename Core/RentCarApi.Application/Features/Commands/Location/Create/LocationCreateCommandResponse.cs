namespace RentCarApi.Application.Features.Commands.Location.Create
{
    public class LocationCreateCommandResponse
    {
        public bool Succeeded { get; set; } = true;
        public string Message { get; set; }
    }
}