using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarApi.Application.Features.Commands.FAQ.Create
{
    public class FAQCreateCommandResponse
    {
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; }
    }
}
