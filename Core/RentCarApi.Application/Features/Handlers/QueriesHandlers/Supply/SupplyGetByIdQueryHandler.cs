using AutoMapper;
using MediatR;
using RentCarApi.Application.Features.Queries.Supply.GetById;
using RentCarApi.Application.UnitOfWork;

namespace RentCarApi.Application.Features.Handlers.QueriesHandlers.Supply;
public class SupplyGetByIdQueryHandler : IRequestHandler<SupplyGetByIdQueryRequest, SupplyGetByIdQueryResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public SupplyGetByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<SupplyGetByIdQueryResponse> Handle(SupplyGetByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var response = new SupplyGetByIdQueryResponse();
        var supply = await _unitOfWork.GetReadRepository<Domain.Models.Supply>().GetAsync(s => s.Id == request.Id);
        if (supply is not null)
        {
            response = _mapper.Map<SupplyGetByIdQueryResponse>(supply);
        }
        return response;
    }
}
