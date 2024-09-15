using Microsoft.AspNetCore.SignalR;
using RentCarApi.Application.Common.Interfaces.Hubs;
using RentCarApi.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarApi.SignalR.HubServices
{
    public class OrderHubService(IHubContext<OrderHub> hubContext) : IOrderHubService
    {
        readonly IHubContext<OrderHub> _hubContext = hubContext;

      

        public async Task OrderAddedMessageAsync(int orderId, int userId)
        {
            await _hubContext.Clients.User(userId.ToString()).SendAsync("AddOrder", orderId);
        }
    }
}
