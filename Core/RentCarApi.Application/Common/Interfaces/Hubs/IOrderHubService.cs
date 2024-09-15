using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarApi.Application.Common.Interfaces.Hubs
{
    public interface IOrderHubService
    {
        Task OrderAddedMessageAsync(int orderId, int userId);
    }
}
