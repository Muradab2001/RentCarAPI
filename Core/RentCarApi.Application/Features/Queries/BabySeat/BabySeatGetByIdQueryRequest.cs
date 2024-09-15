using MediatR;
using RentCarApi.Application.Features.Queries.Brend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarApi.Application.Features.Queries.BabySeat
{
    public class BabySeatGetByIdQueryRequest : IRequest<BabySeatGetByIdQueryResponse>
    {
        public int AppUserId { get; set; }
        public int Id { get; set; }
    }
}
