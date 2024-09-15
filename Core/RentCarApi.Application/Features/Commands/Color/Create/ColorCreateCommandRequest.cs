using MediatR;
using RentCarApi.Application.Features.Commands.VehicleType.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarApi.Application.Features.Commands.Color.Create
{
    public class ColorCreateCommandRequest : IRequest<ColorCreateCommandResponse>
    {
        public string Name { get; set; }
    }
}
