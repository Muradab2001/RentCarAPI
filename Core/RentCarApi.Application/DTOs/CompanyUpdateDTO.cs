using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarApi.Application.DTOs
{
    public class CompanyUpdateDTO
    {
        public string Name { get; set; }
        public IFormFile? Image { get; set; }
    }
}
