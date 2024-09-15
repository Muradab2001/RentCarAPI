using AutoMapper;
using MediatR;
using RentCarApi.Application.Features.Commands.Car.Update;
using RentCarApi.Application.Features.Response;
using RentCarApi.Application.Services;
using RentCarApi.Application.UnitOfWork;
using RentCarApi.Domain.Enum;
using RentCarApi.Domain.Exceptions;
using RentCarApi.Domain.Models;

namespace RentCarApi.Application.Features.Handlers.CommandsHandlers.Car;
public class CarUpdateCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IStorageService storageService) : IRequestHandler<CarUpdateCommandRequest, CarUpdateCommandResponse>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IStorageService _storageService = storageService;

    public async Task<CarUpdateCommandResponse> Handle(CarUpdateCommandRequest request, CancellationToken cancellationToken)
    {
        var response = new CarUpdateCommandResponse();
        #region ValidationsEntities
        if (request.ModelId * request.LocationId * request.BrandId * request.ColorId * request.VehicleTypeId == 0)
            throw new ArgumentNullException($"{nameof(request.ModelId)}, {nameof(request.LocationId)}, {nameof(request.BrandId)}, {nameof(request.ColorId)}, and {nameof(request.VehicleTypeId)} are required fields.");

        var color = await _unitOfWork.GetReadRepository<Domain.Models.Color>().GetAsync(c => c.Id == request.ColorId, false);
        var brand = await _unitOfWork.GetReadRepository<Domain.Models.Brand>().GetAsync(b => b.Id == request.BrandId, false);
        var location = await _unitOfWork.GetReadRepository<Domain.Models.Location>().GetAsync(l => l.Id == request.LocationId, false);
        var model = await _unitOfWork.GetReadRepository<Domain.Models.Model>().GetAsync(m => m.Id == request.ModelId, false);
        var vehicleType = await _unitOfWork.GetReadRepository<Domain.Models.VehicleType>().GetAsync(v => v.Id == request.VehicleTypeId, false);

        request.SuppliesIdsToAdd.Select(async supplyId =>
        {
            if (supplyId != 0)
            {
                var supply = await _unitOfWork.GetReadRepository<Domain.Models.Supply>()
                                              .GetAsync(s => s.Id == supplyId, false);
                ValidateEntity(supply, "Invalid SupplyId");
            }
        });

        request.SuppliesIdsToDelete.Select(async supplyId =>
        {
            if (supplyId != 0)
            {
                var supply = await _unitOfWork.GetReadRepository<Domain.Models.Supply>()
                                          .GetAsync(s => s.Id == supplyId, false);
                ValidateEntity(supply, "Invalid SupplyId");
            }

        });

        ValidateEntity(color, "Invalid ColorId");
        ValidateEntity(brand, "Invalid BrandId");
        ValidateEntity(location, "Invalid LocationId");
        ValidateEntity(model, "Invalid ModelId");
        ValidateEntity(vehicleType, "Invalid VehicleTypeId");
        #endregion

        var carInDb = await _unitOfWork.GetReadRepository<Domain.Models.Car>()
                                       .GetAsync(c => c.Id == request.Id, false);

        var car = _mapper.Map<Domain.Models.Car>(request);
        car.AppUserId = carInDb.AppUserId;

        foreach (var supplyId in request.SuppliesIdsToAdd)
        {
            if (supplyId != 0)
            {
                var carSupply = await _unitOfWork.GetWriteRepository<CarSupply>()
                                             .AddAsync(new() { CarId = request.Id, SupplyId = supplyId, });
            }
        }
        foreach (var supplyId in request.SuppliesIdsToDelete)
        {
            if (supplyId != 0)
            {
                var carSupply = await _unitOfWork.GetReadRepository<CarSupply>()
                                                 .GetAsync(cs => cs.CarId == request.Id && cs.SupplyId == supplyId);
                await _unitOfWork.GetWriteRepository<CarSupply>()
                                 .RemoveAsync(carSupply);
            }
        }
        foreach (var imageId in request.ImagesToDelete)
        {
            if (imageId != 0)
            {
                Image<Domain.Models.Car>? img = await _unitOfWork.GetReadRepository<Image<Domain.Models.Car>>().GetAsync(x=>x.Id== imageId);
                if (img is null)
                {
                    throw new InvalidFileFormatException();
                }
                else
                {
                    await _storageService.DeletePhotoAsync(img.ImageUrl);
                    await _unitOfWork.GetWriteRepository<Image<Domain.Models.Car>>().RemoveAsync(img);
                }
            }
        }
        foreach (var image in request.ImagesToAdd)
        {
            var img = new Image<Domain.Models.Car>()
            {
                ImageUrl = await _storageService.UploadPhotoAsync(image, FolderNames.Car),
                Item = car,
                ItemId = car.Id
            };
            await _unitOfWork.GetWriteRepository<Image<Domain.Models.Car>>().AddAsync(img);
        }

        _unitOfWork.GetWriteRepository<Domain.Models.Car>().Update(car);

        if (await _unitOfWork.SaveChangesAsync() < 1)
        {
            response.Message = ResponseMessages.Failed;
            response.Succeeded = false;
        }
        return response;
    }

    private void ValidateEntity<T>(T entity, string errorMessage) =>
        _ = entity ?? throw new ArgumentNullException(errorMessage);
}