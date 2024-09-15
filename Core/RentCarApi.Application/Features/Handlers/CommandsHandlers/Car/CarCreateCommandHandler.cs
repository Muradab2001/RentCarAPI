using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCarApi.Application.Features.Commands.Car.Create;
using RentCarApi.Application.Services;
using RentCarApi.Application.UnitOfWork;
using RentCarApi.Domain.Enum;
using RentCarApi.Domain.Models;

namespace RentCarApi.Application.Features.Handlers.CommandsHandlers.Car
{
    public class CarCreateCommandHandler(IStorageService storageService, IMapper mapper, IUnitOfWork unitOfWork) : IRequestHandler<CarCreateCommandRequest, CarCreateCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IStorageService _storageService = storageService;

        public async Task<CarCreateCommandResponse> Handle(CarCreateCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new CarCreateCommandResponse();
            var car = _mapper.Map<Domain.Models.Car>(request);

            var color = await _unitOfWork.GetReadRepository<Domain.Models.Color>().GetSingleAsync(c => c.Id == request.ColorId);
            var brand = await _unitOfWork.GetReadRepository<Domain.Models.Brand>().GetSingleAsync(c => c.Id == request.BrandId);
            var location = await _unitOfWork.GetReadRepository<Domain.Models.Location>().GetSingleAsync(c => c.Id == request.LocationId);
            var model = await _unitOfWork.GetReadRepository<Domain.Models.Model>().GetSingleAsync(c => c.Id == request.ModelId);

            ValidateEntity(color, "Invalid ColorId");
            ValidateEntity(brand, "Invalid BrandId");
            ValidateEntity(location, "Invalid LocationId");
            ValidateEntity(model, "Invalid ModelId");

            if (request.SuppliesId != null && request.SuppliesId.Count != 0)
            {
                IQueryable<Domain.Models.Supply> supplies = _unitOfWork.GetReadRepository<Domain.Models.Supply>().Table.AsQueryable();
                var supplyList = await supplies.Where(x => request.SuppliesId.Contains(x.Id)).ToListAsync(cancellationToken: cancellationToken);
                car.AddSupply(supplyList);
            }

            foreach (var image in request.Images)
            {
                var img = new Image<Domain.Models.Car>()
                {
                    ImageUrl = await _storageService.UploadPhotoAsync(image, FolderNames.Car),
                    Item = car,
                    ItemId = car.Id
                };
                await _unitOfWork.GetWriteRepository<Image<Domain.Models.Car>>().AddAsync(img);
            }
            await _unitOfWork.GetWriteRepository<Domain.Models.Car>().AddAsync(car);
            if (await _unitOfWork.SaveChangesAsync() < 1)
            {
                response.Succeeded = false;
                response.Message = "Something went wrong!";
            }
            return response;
        }

        private void ValidateEntity<T>(T entity, string errorMessage)
        {
            if (entity == null)
            {
                throw new ArgumentException(errorMessage);
            }
        }
    }
}