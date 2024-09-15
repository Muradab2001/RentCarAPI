using MediatR;
using RentCarApi.Application.Features.Commands.Model.Delete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarApi.Application.Features.Commands.Supply.Delete
{
    public class SupplyDeleteCommandRequest : IRequest<SupplyDeleteCommandResponse>
    {
    }
}
