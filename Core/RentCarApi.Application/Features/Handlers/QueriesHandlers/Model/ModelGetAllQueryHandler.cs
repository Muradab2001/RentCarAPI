using AutoMapper;
using MediatR;
using RentCarApi.Application.Features.Queries.Model;
using RentCarApi.Application.UnitOfWork;

namespace RentCarApi.Application.Features.Handlers.QueriesHandlers.Model
{
    public class ModelGetAllQueryHandler(IMapper mapper, IUnitOfWork unitOfWork) 
        : IRequestHandler<ModelGetAllQueryRequest, IList<ModelGetAllQueryResponse>>
    {
        private readonly IMapper _mapper = mapper;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<IList<ModelGetAllQueryResponse>> Handle(ModelGetAllQueryRequest request, CancellationToken cancellationToken)
        {
            var models = await _unitOfWork.GetReadRepository<Domain.Models.Model>().GetAll();
            var response = _mapper.Map<IList<ModelGetAllQueryResponse>>(models);
            return response;
        }
    }
}