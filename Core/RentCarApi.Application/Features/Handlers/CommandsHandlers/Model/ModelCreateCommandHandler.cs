using AutoMapper;
using MediatR;
using RentCarApi.Application.Features.Commands.Model.Create;
using RentCarApi.Application.UnitOfWork;

namespace RentCarApi.Application.Features.Handlers.CommandsHandlers.Model;
public class ModelCreateCommandHandler : IRequestHandler<ModelCreateCommandRequest, ModelCreateCommandResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public ModelCreateCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<ModelCreateCommandResponse> Handle(ModelCreateCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new ModelCreateCommandResponse();
        var brand = await _unitOfWork.GetReadRepository<Domain.Models.Brand>().GetAsync(b => b.Id == request.BrandId);
        if (brand is null) { response.Message = "Brand Not Found!"; response.Succeeded = false; }
        else
        {
            var model = _mapper.Map<Domain.Models.Model>(request);
            await _unitOfWork.GetWriteRepository<Domain.Models.Model>().AddAsync(model);
            if (await _unitOfWork.SaveChangesAsync() < 1)
            {
                response.Succeeded = false;
                response.Message = "Something went wrong";
            }
        }
        return response;
    }
}
