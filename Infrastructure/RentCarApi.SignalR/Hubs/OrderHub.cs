using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using RentCarApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RentCarApi.SignalR.Hubs
{
    public class OrderHub :Hub
    {
        private readonly UserManager<AppUser> _userManager;

        public OrderHub(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public override async Task OnConnectedAsync()
        {
            var userId = Context.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (!string.IsNullOrEmpty(userId))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, userId);
                await Clients.All.SendAsync("userConnected", userId, "bağlandı");
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userId = Context.ConnectionId;
            if (!string.IsNullOrEmpty(userId))
            {
                await Clients.All.SendAsync("userDisconnected", userId, "ayrıldı");
            }

            await base.OnDisconnectedAsync(exception);
        }

    }
}
