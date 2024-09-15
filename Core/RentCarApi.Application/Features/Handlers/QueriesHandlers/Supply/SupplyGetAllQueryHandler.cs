using AutoMapper;
using MediatR;
using RentCarApi.Application.Features.Queries.Supply.GetAll;
using RentCarApi.Application.UnitOfWork;

namespace RentCarApi.Application.Features.Handlers.QueriesHandlers.Supply;
public class SupplyGetAllQueryHandler : IRequestHandler<SupplyGetAllQueryRequest, IList<SupplyGetAllQueryResponse>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public SupplyGetAllQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<IList<SupplyGetAllQueryResponse>> Handle(SupplyGetAllQueryRequest request, CancellationToken cancellationToken)
    {
        var supplies = await _unitOfWork.GetReadRepository<Domain.Models.Supply>().GetAll(tracking: false);
        var response = _mapper.Map<IList<SupplyGetAllQueryResponse>>(supplies);
        return response;
    }
}
