using AutoMapper;
using MediatR;
using RentCarApi.Application.Features.Commands.Location.Delete;
using RentCarApi.Application.Features.Response;
using RentCarApi.Application.UnitOfWork;

namespace RentCarApi.Application.Features.Handlers.CommandsHandlers.Location
{
    public class LocationDeleteCommandHandler(IMapper mapper, IUnitOfWork unitOfWork) : IRequestHandler<LocationDeleteCommandRequest, LocationDeleteCommandResponse>
    {
        private readonly IMapper _mapper = mapper;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<LocationDeleteCommandResponse> Handle(LocationDeleteCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new LocationDeleteCommandResponse();
            var location = _mapper.Map<Domain.Models.Location>(request);
            await _unitOfWork.GetWriteRepository<Domain.Models.Location>().RemoveAsync(location);
            if (await _unitOfWork.SaveChangesAsync() < 1)
            {
                response.Message = ResponseMessages.Failed;
            }
            return response;
        }
    }
}