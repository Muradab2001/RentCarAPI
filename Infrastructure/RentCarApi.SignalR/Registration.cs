using Microsoft.Extensions.DependencyInjection;
using RentCarApi.Application.Common.Interfaces.Hubs;
using RentCarApi.SignalR.HubServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarApi.SignalR
{
    public static class Registration
    {
        public static void AddSignalRServices(this IServiceCollection collection)
        {
            collection.AddTransient<IOrderHubService, OrderHubService>();
            collection.AddSignalR();
        }
    }
}
