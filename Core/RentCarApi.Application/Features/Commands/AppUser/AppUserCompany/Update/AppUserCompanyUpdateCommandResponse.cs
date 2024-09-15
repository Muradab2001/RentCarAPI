using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarApi.Application.Features.Commands.AppUser.AppUserCompany.Update
{
    public class AppUserCompanyUpdateCommandResponse
    {
        public bool Succeeded { get; set; } = true;
        public string Message { get; set; } = "Saccesfully Created";
    }
}
