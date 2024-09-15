using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCarApi.Application.Features.Queries.Model;
using RentCarApi.Application.UnitOfWork;

namespace RentCarApi.Application.Features.Handlers.QueriesHandlers.Model
{
    public class ModelGetByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        : IRequestHandler<ModelGetByIdQueryRequest, ModelGetByIdQueryResponse>
    {
        private readonly IMapper _mapper = mapper;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<ModelGetByIdQueryResponse> Handle(ModelGetByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var model = await _unitOfWork.GetReadRepository<Domain.Models.Model>().GetAsync(x => x.Id == request.Id, true, query => query.Include(x => x.Brand));
            var response = _mapper.Map<ModelGetByIdQueryResponse>(model);
            return response;
        }
    }
}