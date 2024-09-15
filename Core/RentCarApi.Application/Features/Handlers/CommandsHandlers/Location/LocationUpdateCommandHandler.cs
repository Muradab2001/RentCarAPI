using AutoMapper;
using MediatR;
using RentCarApi.Application.Features.Commands.Location.Update;
using RentCarApi.Application.Features.Response;
using RentCarApi.Application.UnitOfWork;

namespace RentCarApi.Application.Features.Handlers.CommandsHandlers.Location
{
    public class LocationUpdateCommandHandler(IMapper mapper, IUnitOfWork unitOfWork) : IRequestHandler<LocationUpdateCommandRequest, LocationUpdateCommandResponse>
    {
        private readonly IMapper _mapper = mapper;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<LocationUpdateCommandResponse> Handle(LocationUpdateCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new LocationUpdateCommandResponse();
            var location = await _unitOfWork.GetReadRepository<Domain.Models.Location>().GetAsync(x => x.Id == request.Id);

            if (location == null)
            {
                response.Message = ResponseMessages.NotFound;
            }
            else
            {
                _mapper.Map(request, location);
                _unitOfWork.GetWriteRepository<Domain.Models.Location>().Update(location);
            }

            if (await _unitOfWork.SaveChangesAsync() < 1)
            {
                response.Succeeded = false;
                response.Message = "Something went wrong!";
            }
            return response;
        }
    }
}