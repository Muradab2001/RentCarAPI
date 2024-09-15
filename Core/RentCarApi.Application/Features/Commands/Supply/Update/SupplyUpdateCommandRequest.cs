using MediatR;
using RentCarApi.Application.Features.Commands.Color.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarApi.Application.Features.Commands.Supply.Update
{
    public class SupplyUpdateCommandRequest : IRequest<SupplyUpdateCommandResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
