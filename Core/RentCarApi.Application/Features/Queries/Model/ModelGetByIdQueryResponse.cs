using RentCarApi.Application.Features.Queries.Brand;

namespace RentCarApi.Application.Features.Queries.Model
{
    public class ModelGetByIdQueryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ModelBrandGetByIdQueryResponse Brand { get; set; }
    }
}