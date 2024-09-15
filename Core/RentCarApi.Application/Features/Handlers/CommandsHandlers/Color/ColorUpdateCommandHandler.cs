using AutoMapper;
using MediatR;
using RentCarApi.Application.Features.Commands.Color.Update;
using RentCarApi.Application.UnitOfWork;

namespace RentCarApi.Application.Features.Handlers.CommandsHandlers.Color
{
    public class ColorUpdateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<ColorUpdateCommandRequest, ColorUpdateCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        public async Task<ColorUpdateCommandResponse> Handle(ColorUpdateCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new ColorUpdateCommandResponse();
            var color = await _unitOfWork.GetReadRepository<Domain.Models.Color>().GetAsync(x => x.Id == request.Id);
            _mapper.Map(request, color);
            _unitOfWork.GetWriteRepository<Domain.Models.Color>().Update(color);
            if (await _unitOfWork.SaveChangesAsync() < 1)
            {
                response.Succeeded = false;
                response.Message = "Something went wrong!";
            }
            return response;
        }
    }
}