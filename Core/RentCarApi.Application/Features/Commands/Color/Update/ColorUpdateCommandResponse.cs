namespace RentCarApi.Application.Features.Commands.Color.Update
{
    public class ColorUpdateCommandResponse
    {
        public bool Succeeded { get; set; } = true;
        public string Message { get; set; } = "Succesfully updated";
    }
}