using MediatR;
using RentCarApi.Application.Features.Commands.Model.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarApi.Application.Features.Commands.Supply.Create
{
    public class SupplyCreateCommandRequest : IRequest<SupplyCreateCommandResponse>
    {
        public string Name { get; set; }
    }
}
