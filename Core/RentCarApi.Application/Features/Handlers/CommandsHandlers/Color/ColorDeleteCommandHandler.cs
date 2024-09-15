using AutoMapper;
using MediatR;
using RentCarApi.Application.Features.Commands.Color.Delete;
using RentCarApi.Application.UnitOfWork;

namespace RentCarApi.Application.Features.Handlers.CommandsHandlers.Color
{
    public class ColorDeleteCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        : IRequestHandler<ColorDeleteCommandRequest, ColorDeleteCommandResponse>
    {
        private readonly IMapper _mapper = mapper;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<ColorDeleteCommandResponse> Handle(ColorDeleteCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new ColorDeleteCommandResponse();
            
            var color = await _unitOfWork.GetReadRepository<Domain.Models.Color>().GetAsync(x=>x.Id==request.Id);
            await _unitOfWork.GetWriteRepository<Domain.Models.Color>().RemoveAsync(color);
            if (await _unitOfWork.SaveChangesAsync() < 1)
            {
                response.Succeeded = false;
                response.Message = "Something went wrong!";
            }
            return response;
        }
    }
}