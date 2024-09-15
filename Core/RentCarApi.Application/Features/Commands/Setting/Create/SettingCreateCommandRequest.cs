using MediatR;
using RentCarApi.Application.Features.Commands.Location.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarApi.Application.Features.Commands.Setting.Create
{
    public class SettingCreateCommandRequest : IRequest<SettingCreateCommandResponse>
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
