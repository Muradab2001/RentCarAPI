using AutoMapper;
using MediatR;
using RentCarApi.Application.Features.Commands.FAQ.Create;
using RentCarApi.Application.UnitOfWork;

namespace RentCarApi.Application.Features.Handlers.CommandsHandlers.FAQ
{
    public class FAQCreateCommandHandler(IMapper mapper, IUnitOfWork unitOfWork) : IRequestHandler<FAQCreateCommandRequest, FAQCreateCommandResponse>
    {
        private readonly IMapper _mapper = mapper;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<FAQCreateCommandResponse> Handle(FAQCreateCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new FAQCreateCommandResponse();
            var model = _mapper.Map<Domain.Models.FAQ>(request);
            await _unitOfWork.GetWriteRepository<Domain.Models.FAQ>().AddAsync(model);
            if (await _unitOfWork.SaveChangesAsync() < 1)
            {
                response.IsSuccess = false;
                response.Message = "Something went wrong";
            }

            return response;
        }
    }
}
