using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarApi.Application.Features.Commands.Brand.Create
{
    public class BrandCreateCommandRequest :IRequest<BrandCreateCommandResponse>
    {
        public string Name { get; set; }
        public IFormFile Image { get; set; }
    }
}
