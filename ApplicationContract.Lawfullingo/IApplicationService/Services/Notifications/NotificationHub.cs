using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
namespace Dynamic.Domain.Services.Notifications
{
    public class NotificationHub : Hub
    {
        public string GetConnectionId()
        {
            return Context.ConnectionId;
        }

    
}
}
