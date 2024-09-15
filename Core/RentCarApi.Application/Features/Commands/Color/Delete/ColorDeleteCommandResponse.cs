namespace RentCarApi.Application.Features.Commands.Color.Delete
{
    public class ColorDeleteCommandResponse
    {
        public bool Succeeded { get; set; } = true;
        public string Message { get; set; } = "Successfully deleted";
    }
}