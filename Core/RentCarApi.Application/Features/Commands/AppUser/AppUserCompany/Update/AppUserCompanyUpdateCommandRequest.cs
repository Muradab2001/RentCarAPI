using MediatR;
using RentCarApi.Application.DTOs;
using RentCarApi.Application.Features.Commands.Brand.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarApi.Application.Features.Commands.AppUser.AppUserCompany.Update
{
    public class AppUserCompanyUpdateCommandRequest : IRequest<AppUserCompanyUpdateCommandResponse>
    {
        public int AppUserId {  get; set; }
        public CompanyUpdateDTO CompanyData { get; set; }
        public int LocationId { get; set; }
    }
}
