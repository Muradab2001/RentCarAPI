using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarApi.Application.Features.Queries.BabySeat
{
    public class BabySeatGetByIdQueryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public List<string> Images { get; set; }
    }
}
