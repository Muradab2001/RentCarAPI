using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCarApi.Application.Features.Queries.BabySeat;
using RentCarApi.Application.UnitOfWork;

namespace RentCarApi.Application.Features.Handlers.QueriesHandlers.BabySeat
{
    public class BabySeatGetByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork) : IRequestHandler<BabySeatGetByIdQueryRequest, BabySeatGetByIdQueryResponse>
    {
        private readonly IMapper _mapper = mapper;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<BabySeatGetByIdQueryResponse> Handle(BabySeatGetByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var babySeat = await _unitOfWork.GetReadRepository<Domain.Models.BabySeat>().GetAsync(x => x.Id == request.Id,true,b=>b.Include(b=>b.Images));
            if (babySeat == null) throw new Exception("BabySeat not found");
            var response = _mapper.Map<BabySeatGetByIdQueryResponse>(babySeat);
            return response;
        }
    }
}
