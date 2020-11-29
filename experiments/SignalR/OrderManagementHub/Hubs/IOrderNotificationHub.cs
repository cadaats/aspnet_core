using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagementHub.Hubs
{
    public interface IOrderNotificationHub
    {
        Task NotifyOrderUpdates(string user, string message);
    }
}
