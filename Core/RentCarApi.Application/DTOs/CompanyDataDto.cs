using Microsoft.AspNetCore.Http;

namespace RentCarApi.Application.DTOs;

public class CompanyDataDto
{
    public string Name { get; set; }
    public IFormFile Image { get; set; }
}