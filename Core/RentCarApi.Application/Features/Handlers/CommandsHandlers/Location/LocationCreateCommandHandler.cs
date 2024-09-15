using AutoMapper;
using MediatR;
using RentCarApi.Application.Features.Commands.Location.Create;
using RentCarApi.Application.UnitOfWork;

namespace RentCarApi.Application.Features.Handlers.CommandsHandlers.Location
{
    public class LocationCreateCommandHandler(IMapper mapper, IUnitOfWork unitOfWork) : IRequestHandler<LocationCreateCommandRequest, LocationCreateCommandResponse>
    {
        private readonly IMapper _mapper = mapper;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<LocationCreateCommandResponse> Handle(LocationCreateCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new LocationCreateCommandResponse();
            var location = _mapper.Map<Domain.Models.Location>(request);
            await _unitOfWork.GetWriteRepository<Domain.Models.Location>().AddAsync(location);
            if (await _unitOfWork.SaveChangesAsync() < 1)
            {
                response.Succeeded = false;
                response.Message = "Something went wrong!";
            }
            return response;
        }
    }
}