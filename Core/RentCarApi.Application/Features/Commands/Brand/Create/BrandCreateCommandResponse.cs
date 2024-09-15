using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarApi.Application.Features.Commands.Brand.Create
{
    public class BrandCreateCommandResponse
    {
        public bool Succeeded { get; set; } = true;
        public string Message { get; set; } = "Saccesfully Created";
    }
}
