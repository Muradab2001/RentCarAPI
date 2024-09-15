using RentCarApi.Application.Features.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarApi.Application.Features.Commands.Supply.Delete
{
    public class SupplyDeleteCommandResponse
    {
        public int Id { get; set; }
        public string Message { get; set; } = ResponseMessages.Success;
    }
}
