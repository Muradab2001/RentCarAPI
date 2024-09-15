using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarApi.Application.Features.Commands.Brand.Delete
{
    public class BrandDeleteCommandResponse
    {
        public int Id { get; set; }
        public string Message { get; set; } = "Saccesfully Deleted";
    }
}
