using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarApi.Application.Features.Commands.BabySeat.Delete
{
    public class BabySeatDeleteCommandRequest :IRequest<BabySeatDeleteCommandResponse>
    {
        public int Id { get; set; }
    }
}
