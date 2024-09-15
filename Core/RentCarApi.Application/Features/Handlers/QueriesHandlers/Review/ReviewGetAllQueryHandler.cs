using AutoMapper;
using MediatR;
using RentCarApi.Application.Features.Queries.Review.GetAll;
using RentCarApi.Application.UnitOfWork;

namespace RentCarApi.Application.Features.Handlers.QueriesHandlers.Review;
public class ReviewGetAllQueryHandler : IRequestHandler<ReviewGetAllQueryRequest, IList<ReviewGetAllQueryResponse>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public ReviewGetAllQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<IList<ReviewGetAllQueryResponse>> Handle(ReviewGetAllQueryRequest request, CancellationToken cancellationToken)
    {
        var reviews = await _unitOfWork.GetReadRepository<Domain.Models.Review>().GetAll(tracking: false);
        return _mapper.Map<IList<ReviewGetAllQueryResponse>>(reviews);
    }
}
