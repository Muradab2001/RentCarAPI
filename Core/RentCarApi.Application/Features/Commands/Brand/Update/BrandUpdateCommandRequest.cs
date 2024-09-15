using MediatR;
using Microsoft.AspNetCore.Http;
using RentCarApi.Application.Features.Commands.VehicleType.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarApi.Application.Features.Commands.Brand.Update
{
    public class BrandUpdateCommandRequest : IRequest<BrandUpdateCommandResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IFormFile? Image { get; set; }
    }
}
