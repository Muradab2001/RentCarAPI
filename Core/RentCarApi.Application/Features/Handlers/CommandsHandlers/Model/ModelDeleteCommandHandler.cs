using AutoMapper;
using MediatR;
using RentCarApi.Application.Features.Commands.Model.Delete;
using RentCarApi.Application.Features.Response;
using RentCarApi.Application.UnitOfWork;

namespace RentCarApi.Application.Features.Handlers.CommandsHandlers.Model;
public class ModelDeleteCommandHandler : IRequestHandler<ModelDeleteCommandRequest, ModelDeleteCommandResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public ModelDeleteCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<ModelDeleteCommandResponse> Handle(ModelDeleteCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new ModelDeleteCommandResponse();
        var model = _mapper.Map<Domain.Models.Model>(request);
        await _unitOfWork.GetWriteRepository<Domain.Models.Model>().RemoveAsync(model);
        if (await _unitOfWork.SaveChangesAsync() < 1) 
        {
            response.Message = ResponseMessages.Failed;
        }
        return response;
    }
}
