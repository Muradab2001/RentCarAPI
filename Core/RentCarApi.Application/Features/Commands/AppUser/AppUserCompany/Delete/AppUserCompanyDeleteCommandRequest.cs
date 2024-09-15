using MediatR;

namespace RentCarApi.Application.Features.Commands.AppUser.AppUserCompany.Delete
{
    public class AppUserCompanyDeleteCommandRequest : IRequest<AppUserCompanyDeleteCommandResponse>
    {
        public int Id { get; set; }
    }
}