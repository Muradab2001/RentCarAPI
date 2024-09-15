using AutoMapper;
using MediatR;
using RentCarApi.Application.Features.Queries.Color;
using RentCarApi.Application.UnitOfWork;

namespace RentCarApi.Application.Features.Handlers.QueriesHandlers.Color
{
    public class ColorGetAllQueryHandler : IRequestHandler<ColorGetAllQueryRequest, IList<ColorGetAllQueryResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public ColorGetAllQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<IList<ColorGetAllQueryResponse>> Handle(ColorGetAllQueryRequest request, CancellationToken cancellationToken)
        {
            var colors = await _unitOfWork.GetReadRepository<Domain.Models.Color>().GetAll();
            var response = _mapper.Map<IList<ColorGetAllQueryResponse>>(colors);
            return response;
        }
    }
}
