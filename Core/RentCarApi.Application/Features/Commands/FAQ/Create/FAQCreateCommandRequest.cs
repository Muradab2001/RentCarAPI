using MediatR;
using RentCarApi.Application.Features.Commands.Location.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarApi.Application.Features.Commands.FAQ.Create
{
    public class FAQCreateCommandRequest : IRequest<FAQCreateCommandResponse>
    {
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
