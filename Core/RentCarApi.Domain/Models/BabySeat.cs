using RentCarApi.Domain.Models.Base;

namespace RentCarApi.Domain.Models;
public class BabySeat : BaseEntity
{
    public string Name { get; set; }
    public double Price { get; set; }
    public CompanyData CompanyData { get; set; }
    public int CompanyDataId { get; set; }
    private readonly List<Image<BabySeat>> _images;
    public IReadOnlyCollection<Image<BabySeat>> Images => _images;

    public BabySeat()
    {
        _images = [];
    }
}