using AutoMapper;
using MediatR;
using RentCarApi.Application.Features.Commands.Model.Delete;
using RentCarApi.Application.Features.Commands.Review.Delete;
using RentCarApi.Application.Features.Response;
using RentCarApi.Application.UnitOfWork;

namespace RentCarApi.Application.Features.Handlers.CommandsHandlers.Review;
public class ReviewDeleteCommandHandler : IRequestHandler<ReviewDeleteCommandRequest, ReviewDeleteCommandResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public ReviewDeleteCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<ReviewDeleteCommandResponse> Handle(ReviewDeleteCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new ReviewDeleteCommandResponse();
        var review = _mapper.Map<Domain.Models.Review>(request);
        await _unitOfWork.GetWriteRepository<Domain.Models.Review>().RemoveAsync(review);
        if (await _unitOfWork.SaveChangesAsync() < 1)
        {
            response.Message = ResponseMessages.Failed;
        }
        return response;
    }
}
