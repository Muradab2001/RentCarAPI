using Microsoft.AspNetCore.Http;

namespace RentCarApi.Application.DTOs;
public class PersonalDataDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public IFormFile IdCardImage { get; set; }
    public IFormFile? DrivingLicenseImage { get; set; }
}