using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarApi.Application.Features.Commands.BabySeat.Delete
{
    public class BabySeatDeleteCommandResponse
    {
        public int Id { get; set; }
        public string Message { get; set; } = "Saccesfully Deleted";
    }
}
