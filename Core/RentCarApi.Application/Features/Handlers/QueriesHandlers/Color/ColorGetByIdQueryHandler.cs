using AutoMapper;
using MediatR;
using RentCarApi.Application.Features.Queries.Color;
using RentCarApi.Application.UnitOfWork;

namespace RentCarApi.Application.Features.Handlers.QueriesHandlers.Color
{
    public class ColorGetByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        : IRequestHandler<ColorGetByIdQueryRequest, ColorGetByIdQueryResponse>
    {
        private readonly IMapper _mapper = mapper;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<ColorGetByIdQueryResponse> Handle(ColorGetByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var color = await _unitOfWork.GetReadRepository<Domain.Models.Color>().GetAsync(x=>x.Id==request.Id);
            var response = _mapper.Map<ColorGetByIdQueryResponse>(color);
            return response;
        }
    }
}