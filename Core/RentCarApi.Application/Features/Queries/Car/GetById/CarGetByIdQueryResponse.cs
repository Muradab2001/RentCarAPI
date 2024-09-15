using RentCarApi.Application.DTOs;
using RentCarApi.Application.Features.Queries.Color;
using RentCarApi.Application.Features.Queries.Model;
using RentCarApi.Application.Features.Queries.VehicleType;
using RentCarApi.Domain.Enum;
using System.Text.Json.Serialization;

namespace RentCarApi.Application.Features.Queries.Car.GetById
{
    public class CarGetByIdQueryResponse
    {
        public string Description { get; set; }
        public int SeatCount { get; set; }
        public int ReservedCount { get; set; }
        public bool isReserved { get; set; }
        public int Year { get; set; }
        public int Power { get; set; }
        public string City { get; set; }
        public double Weight { get; set; }
        public double Price { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TransmissionType TransmissionType { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public FuelType FuelType { get; set; }
        public AppUserCompanyDto AppUser { get; set; }
        public ColorGetByIdQueryResponse Color { get; set; }
        public ModelGetByIdQueryResponse Model { get; set; }
        public VehicleTypeGetByIdQueryResponse VehicleType { get; set; }
        public List<CarSupplyResponse> Supplies { get; set; }
    }
}