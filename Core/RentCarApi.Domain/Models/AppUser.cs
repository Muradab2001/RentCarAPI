using Microsoft.AspNetCore.Identity;
using RentCarApi.Domain.Models.Base;
namespace RentCarApi.Domain.Models;

public class AppUser : IdentityUser<int>, IBaseEntity
{
    public PersonalData PersonalData { get; set; }
    public CompanyData CompanyData { get; set; }
    public Location Location { get; set; }
    public int LocationId { get; set; }
    public List<Review> Reviews { get; set; }
    public string? Otp {  get; set; }
    public DateTime? OtpExpiration { get; set; }

    public void SetOtp(string otp)
    {
        Otp = otp;
        OtpExpiration = DateTime.Now.AddMinutes(5);
    }
    public void SetDetails(int locationId, string name, string image)
    {
        LocationId = locationId;
        CompanyData.Name = name;
        CompanyData.Image = image;
    }
}