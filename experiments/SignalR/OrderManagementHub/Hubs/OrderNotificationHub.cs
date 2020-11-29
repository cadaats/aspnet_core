using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using OrderManagementHub.Hubs;
using System;
using System.Threading.Tasks;

namespace OrderManagementHub.Hubs
{
    public class OrderNotificationHub : Hub<IOrderNotificationHub>
    {
        private readonly ILogger _logger;

        public OrderNotificationHub(ILogger<OrderNotificationHub> logger) => _logger = logger;

        //public Task SendMessage(string user, string message)
        //{
        //    return Clients.All.SendAsync("ReceiveOrderUpdates", user, message);
        //}
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.NotifyOrderUpdates(user, message);
        }

        public override async Task OnConnectedAsync()
        {
            _logger.LogInformation("Connected to SignalR");
            await Groups.AddToGroupAsync(Context.ConnectionId, "SignalR Users");
            _logger.LogInformation("Added to SignalR group");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            _logger.LogInformation("Disconnected from SignalR");
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "SignalR Users");
            _logger.LogInformation("Removed from SignalR group");
            await base.OnDisconnectedAsync(exception);
        }
    }
}