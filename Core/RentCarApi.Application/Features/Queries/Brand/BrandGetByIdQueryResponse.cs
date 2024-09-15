using RentCarApi.Application.Features.Queries.Car;
using RentCarApi.Application.Features.Queries.Model;

namespace RentCarApi.Application.Features.Queries.Brend
{
    public class BrandGetByIdQueryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        //public List<CarGetAllQueryResponse> Cars { get; set; }
        public List<ModelGetByIdQueryResponse> Models { get; set; }
    }
}