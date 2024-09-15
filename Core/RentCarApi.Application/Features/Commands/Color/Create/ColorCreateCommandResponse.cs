using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarApi.Application.Features.Commands.Color.Create
{
    public class ColorCreateCommandResponse
    {
        public bool Succeeded { get; set; } = true;
        public string Message { get; set; } = "Saccesfully Created";
    }
}
