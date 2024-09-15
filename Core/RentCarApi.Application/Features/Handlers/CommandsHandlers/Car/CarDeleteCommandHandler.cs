using AutoMapper;
using MediatR;
using RentCarApi.Application.Features.Commands.Car.Delete;
using RentCarApi.Application.Features.Response;
using RentCarApi.Application.UnitOfWork;

namespace RentCarApi.Application.Features.Handlers.CommandsHandlers.Car;
public class CarDeleteCommandHandler : IRequestHandler<CarDeleteCommandRequest, CarDeleteCommandResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public CarDeleteCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<CarDeleteCommandResponse> Handle(CarDeleteCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new CarDeleteCommandResponse();
        var car = await _unitOfWork.GetReadRepository<Domain.Models.Car>().GetAsync(c => c.Id == request.Id);
        if (car is null)
        {
            response.Succeeded = false;
            response.Message = ResponseMessages.NotFound;
        }
        else
        {
            car.IsDeleted = true;
            _unitOfWork.GetWriteRepository<Domain.Models.Car>().Update(car);
            if (await _unitOfWork.SaveChangesAsync() < 1)
            {
                response.Succeeded = false;
                response.Message = ResponseMessages.Failed;
            }
        }
        return response;
    }
}
