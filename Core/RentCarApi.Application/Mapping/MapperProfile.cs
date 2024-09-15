using AutoMapper;
using RentCarApi.Application.DTOs;
using RentCarApi.Application.Features.Commands.AppUser.AppUserCompany.Delete;
using RentCarApi.Application.Features.Commands.AppUser.AppUserCompany.SignUp;
using RentCarApi.Application.Features.Commands.AppUser.AppUserCompany.Update;
using RentCarApi.Application.Features.Commands.AppUser.AppUserPersonal.SignUp;
using RentCarApi.Application.Features.Commands.AppUser.AppUserPersonal.Update;
using RentCarApi.Application.Features.Commands.BabySeat.Create;
using RentCarApi.Application.Features.Commands.BabySeat.Update;
using RentCarApi.Application.Features.Commands.Brand.Create;
using RentCarApi.Application.Features.Commands.Brand.Update;
using RentCarApi.Application.Features.Commands.Car.Create;
using RentCarApi.Application.Features.Commands.Car.Update;
using RentCarApi.Application.Features.Commands.Color.Create;
using RentCarApi.Application.Features.Commands.Color.Delete;
using RentCarApi.Application.Features.Commands.Color.Update;
using RentCarApi.Application.Features.Commands.Discount.Create;
using RentCarApi.Application.Features.Commands.FAQ.Create;
using RentCarApi.Application.Features.Commands.Location.Create;
using RentCarApi.Application.Features.Commands.Location.Delete;
using RentCarApi.Application.Features.Commands.Location.Update;
using RentCarApi.Application.Features.Commands.Model.Create;
using RentCarApi.Application.Features.Commands.Model.Delete;
using RentCarApi.Application.Features.Commands.Model.Update;
using RentCarApi.Application.Features.Commands.Order.Create;
using RentCarApi.Application.Features.Commands.PromoCode.Create;
using RentCarApi.Application.Features.Commands.Review.Create;
using RentCarApi.Application.Features.Commands.Review.Delete;
using RentCarApi.Application.Features.Commands.Setting.Create;
using RentCarApi.Application.Features.Commands.Supply.Create;
using RentCarApi.Application.Features.Commands.Supply.Update;
using RentCarApi.Application.Features.Commands.VehicleType.Create;
using RentCarApi.Application.Features.Commands.VehicleType.Update;
using RentCarApi.Application.Features.Queries.Brand;
using RentCarApi.Application.Features.Queries.Brend;
using RentCarApi.Application.Features.Queries.Car.GetAll;
using RentCarApi.Application.Features.Queries.Car.GetById;
using RentCarApi.Application.Features.Queries.Color;
using RentCarApi.Application.Features.Queries.Favorite;
using RentCarApi.Application.Features.Queries.Location.GetAll;
using RentCarApi.Application.Features.Queries.Location.GetById;
using RentCarApi.Application.Features.Queries.Model;
using RentCarApi.Application.Features.Queries.Review.GetAll;
using RentCarApi.Application.Features.Queries.Supply.GetAll;
using RentCarApi.Application.Features.Queries.Supply.GetById;
using RentCarApi.Application.Features.Queries.VehicleType;
using RentCarApi.Domain.Common;
using RentCarApi.Domain.Models;

namespace RentCarApi.Application.Mapping;
public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<VehicleTypeCreateCommandRequest, VehicleType>();
        CreateMap<VehicleTypeGetAllQueryResponse, VehicleType>();
        CreateMap<VehicleTypeUpdateCommandRequest, VehicleType>();
        CreateMap<VehicleType, VehicleTypeGetByIdQueryResponse>();
        CreateMap<VehicleType, VehicleTypeGetAllQueryResponse>();

        CreateMap<ModelUpdateCommandRequest, Model>();
        CreateMap<ModelCreateCommandRequest, Model>();
        CreateMap<ModelDeleteCommandRequest, Model>();
        CreateMap<Model, ModelGetByIdQueryResponse>();
        CreateMap<Model, ModelGetAllQueryResponse>();
        CreateMap<Model, ModelDto>();

        CreateMap<BrandCreateCommandRequest, Brand>();
        CreateMap<BrandCreateCommandRequest, Brand>();
        CreateMap<BrandUpdateCommandRequest, Brand>();
        CreateMap<Brand, BrandGetAllQueryResponse>();
        CreateMap<Brand, BrandGetByIdQueryResponse>();
        CreateMap<Brand, ModelBrandGetByIdQueryResponse>();

        CreateMap<ColorCreateCommandRequest, Color>();
        CreateMap<ColorDeleteCommandRequest, Color>();
        CreateMap<ColorUpdateCommandRequest, Color>();
        CreateMap<Color, ColorGetByIdQueryResponse>();
        CreateMap<ColorGetAllQueryResponse, Color>();

        CreateMap<SupplyCreateCommandRequest, Supply>();
        CreateMap<SupplyUpdateCommandRequest, Supply>();
        CreateMap<Supply, SupplyGetByIdQueryResponse>();
        CreateMap<Supply, SupplyGetAllQueryResponse>();

        CreateMap<LocationCreateCommandRequest, Location>();
        CreateMap<LocationDeleteCommandRequest, Location>();
        CreateMap<LocationUpdateCommandRequest, Location>();
        CreateMap<Location, LocationGetByIdQueryResponse>();
        CreateMap<Location, LocationGetAllQueryResponse>();

        CreateMap<AppUserPersonalSignUpCommandRequest, AppUser>();

        CreateMap<PersonalDataDTO, PersonalData>();
        CreateMap<LocationDTO, Location>();

        CreateMap<GetFavoritesByUserIdRequest, Favorite>();
        CreateMap<Image<Car>, ImageResponse>();

        CreateMap<ReviewCreateCommandRequest, Review>();
        CreateMap<Review, ReviewGetAllQueryResponse>();
        CreateMap<ReviewDeleteCommandRequest, Review>();

        CreateMap<AppUserCompanySignUpCommandRequest, AppUser>();
        CreateMap<AppUserCompanyDeleteCommandRequest, AppUser>();
        CreateMap<AppUserPersonalUpdateCommandRequest, AppUser>();
        CreateMap<AppUserCompanyUpdateCommandRequest, AppUser>()
       .ForMember(dest => dest.SecurityStamp, opt => opt.Ignore());
        CreateMap<AppUser, AppUserCompanyDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CompanyData.Name));

        CreateMap<CompanyDataDto, AppUser>();
        CreateMap<CompanyDataDto, CompanyData>();
        CreateMap<CompanyUpdateDTO, CompanyData>();
        CreateMap<PersonalDataDTO, PersonalData>();
        CreateMap<BabySeatCreateCommandRequest, BabySeat>()
               .ForMember(dest => dest.Images, opt => opt.Ignore());
        CreateMap<BabySeatUpdateCommandRequest, BabySeat>();

        CreateMap<CarCreateCommandRequest, Car>()
              .ForMember(dest => dest.Images, opt => opt.Ignore());
        CreateMap<CarUpdateCommandRequest, Car>();
        CreateMap<Car, CarGetAllQueryResponse>();
        CreateMap<Car, CarGetByIdQueryResponse>();

        CreateMap<CarSupply, CarSupplyResponse>();

        CreateMap<OrderCreateCommandRequest, Order>();

        CreateMap<CreateDiscountCommandRequest, Discount>();

        CreateMap<SettingCreateCommandRequest, Setting>();

        CreateMap<FAQCreateCommandRequest, FAQ>();

        CreateMap<PromoCodeCreateCommandRequest, PromoCode>()
              .ForMember(dest => dest.CompanyDataId, opt => opt.Ignore());
    }
}