using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarApi.Application.Features.Commands.VehicleType.Update
{
    public class VehicleTypeUpdateCommandRequest : IRequest<VehicleTypeUpdateCommandResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
