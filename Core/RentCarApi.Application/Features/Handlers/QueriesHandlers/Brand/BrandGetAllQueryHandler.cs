using AutoMapper;
using MediatR;
using RentCarApi.Application.Features.Queries.Brend;
using RentCarApi.Application.Features.Queries.Color;
using RentCarApi.Application.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarApi.Application.Features.Handlers.QueriesHandlers.Brand
{
    public class BrandGetAllQueryHandler : IRequestHandler<BrandGetAllQueryRequest, IList<BrandGetAllQueryResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public BrandGetAllQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<IList<BrandGetAllQueryResponse>> Handle(BrandGetAllQueryRequest request, CancellationToken cancellationToken)
        {
            var brends = await _unitOfWork.GetReadRepository<Domain.Models.Brand>().GetAll();
            var response = _mapper.Map<IList<BrandGetAllQueryResponse>>(brends);
            return response;
        }
    }
}
