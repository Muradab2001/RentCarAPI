using AutoMapper;
using MediatR;
using RentCarApi.Application.Features.Commands.PromoCode.Create;
using RentCarApi.Application.Features.Response;
using RentCarApi.Application.UnitOfWork;
using RentCarApi.Domain.Exceptions;

namespace RentCarApi.Application.Features.Handlers.CommandsHandlers.PromoCode;
public class PromoCodeCreateCommandHandler(IMapper mapper, IUnitOfWork unitOfWork) : IRequestHandler<PromoCodeCreateCommandRequest, PromoCodeCreateCommandResponse>
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<PromoCodeCreateCommandResponse> Handle(PromoCodeCreateCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new PromoCodeCreateCommandResponse();
        var companyData = await _unitOfWork.GetReadRepository<Domain.Models.CompanyData>()
                                           .GetAsync(cd => cd.AppUserId == request.AppUserId);
        if (companyData is null) { throw new EntityNotFoundException("Company Not Found!"); }
        var promoCode = _mapper.Map<Domain.Models.PromoCode>(request);
        promoCode.CompanyDataId = companyData.Id;
        await _unitOfWork.GetWriteRepository<Domain.Models.PromoCode>().AddAsync(promoCode);
        if (await _unitOfWork.SaveChangesAsync() < 1)
        {
            response.Succeeded = false;
            response.Message = ResponseMessages.Failed;
        }
        return response;
    }
}
