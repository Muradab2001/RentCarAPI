using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCarApi.Application.Features.Queries.Brend;
using RentCarApi.Application.Features.Queries.Model;
using RentCarApi.Application.UnitOfWork;

namespace RentCarApi.Application.Features.Handlers.QueriesHandlers.Brand
{
    public class BrandGetByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork) : IRequestHandler<BrandGetByIdQueryRequest, BrandGetByIdQueryResponse>
    {
        private readonly IMapper _mapper = mapper;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<BrandGetByIdQueryResponse> Handle(BrandGetByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var brand = await _unitOfWork.GetReadRepository<Domain.Models.Brand>().GetAsync(x => x.Id == request.Id, true, query => query.Include(x => x.Models));
            var response = _mapper.Map<BrandGetByIdQueryResponse>(brand);
            return response;
        }
    }
}