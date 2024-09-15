using Microsoft.AspNetCore.Builder;
using RentCarApi.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarApi.SignalR
{
    public static class HubRegistration
    {
        public static void MapHubs(this WebApplication webApplication)
        {
            webApplication.MapHub<OrderHub>("/orders-hub");
        }
    }
}
