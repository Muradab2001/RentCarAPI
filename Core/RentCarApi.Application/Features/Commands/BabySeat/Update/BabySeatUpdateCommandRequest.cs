using MediatR;
using Microsoft.AspNetCore.Http;

namespace RentCarApi.Application.Features.Commands.BabySeat.Update
{
    public class BabySeatUpdateCommandRequest : IRequest<BabySeatUpdateCommandResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public List<IFormFile>? ImagesToAdd { get; set; }
        public List<int>? ImagesToDelete { get; set; }
    }
}