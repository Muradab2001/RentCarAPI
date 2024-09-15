using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using RentCarApi.Application.Features.Commands.Review.Create;
using RentCarApi.Application.UnitOfWork;
using RentCarApi.Domain.Models;

namespace RentCarApi.Application.Features.Handlers.CommandsHandlers.Review;
public class ReviewCreateCommandHandler : IRequestHandler<ReviewCreateCommandRequest, ReviewCreateCommandResponse>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<Domain.Models.AppUser> _userManager;
    public ReviewCreateCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, UserManager<Domain.Models.AppUser> userManager)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _userManager = userManager;
    }

    public async Task<ReviewCreateCommandResponse> Handle(ReviewCreateCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new ReviewCreateCommandResponse();
        var appUser = await _userManager.FindByIdAsync(request.AppUserId.ToString());
        var car = await _unitOfWork.GetReadRepository<Domain.Models.Car>().GetAsync(c => c.Id == request.CarId);
        if (appUser is not null && car is not null)
        {
            var review = _mapper.Map<Domain.Models.Review>(request);
            await _unitOfWork.GetWriteRepository<Domain.Models.Review>().AddAsync(review);

            if (await _unitOfWork.SaveChangesAsync() < 1)
            {
                response.Succeeded = false;
                response.Message = "Something went wrong";
            }
            return response;
        }
        if (appUser is null)
        {
            response.Message += "User Not Found!";
            response.Succeeded = false;
        }
        if (car is null)
        {
            response.Message += "Car Not Found!";
            response.Succeeded = false;
        }
        return response;
    }
}
