using RentCarApi.Domain.Models.Base;

namespace RentCarApi.Domain.Models;
public class PromoCode : BaseEntity
{
    public string Code {  get; set; }
    public int CompanyDataId { get; set; }
    public CompanyData CompanyData { get; set; }
}
