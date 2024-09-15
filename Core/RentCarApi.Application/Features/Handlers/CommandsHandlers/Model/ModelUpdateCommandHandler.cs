using AutoMapper;
using MediatR;
using RentCarApi.Application.Features.Commands.Model.Update;
using RentCarApi.Application.Features.Response;
using RentCarApi.Application.UnitOfWork;

namespace RentCarApi.Application.Features.Handlers.CommandsHandlers.Model;
public class ModelUpdateCommandHandler : IRequestHandler<ModelUpdateCommandRequest, ModelUpdateCommandResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public ModelUpdateCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<ModelUpdateCommandResponse> Handle(ModelUpdateCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new ModelUpdateCommandResponse();
        var model = _mapper.Map<Domain.Models.Model>(request);
        var modelInDb = await _unitOfWork.GetReadRepository<Domain.Models.Model>().GetAsync(m => m.Id == request.Id, tracking: false);
        if (modelInDb is null) { response.Message = ResponseMessages.NotFound; response.Succeeded = false; }
        else
        {
            var brand = await _unitOfWork.GetReadRepository<Domain.Models.Brand>().GetAsync(b => b.Id == request.BrandId);
            if (brand is null) { response.Message = "Brand Not Found!"; response.Succeeded = false; }
            else
            {
                var isSuccess = _unitOfWork.GetWriteRepository<Domain.Models.Model>().Update(model);
                if (!isSuccess || await _unitOfWork.SaveChangesAsync() < 1)
                {
                    response.Message = ResponseMessages.Failed;
                    response.Succeeded = false;
                }
            }
        }
        return response;
    }
}
