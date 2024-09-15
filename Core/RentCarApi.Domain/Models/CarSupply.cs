using RentCarApi.Domain.Models.Base;

namespace RentCarApi.Domain.Models;
public class CarSupply : IBaseEntity
{
    public Car Car { get; set; }    
    public int CarId { get; set; }
    public Supply Supply { get; set; }  
    public int SupplyId { get; set; }

    public void SetSupply(int supplyId)
    {
        SupplyId = supplyId;
    }
}