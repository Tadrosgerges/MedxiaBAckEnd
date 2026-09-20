using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Medexia.WebApi.Hubs
{
    [Authorize]
    public class QueueHub:Hub
    {
        public async Task JoinQueueGroup(int timeTableId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, GetGroupName(timeTableId));
        }

        public async Task LeaveQueueGroup(int timeTableId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, GetGroupName(timeTableId));
        }

        public static string GetGroupName(int timeTableId) => $"Queue_{timeTableId}";
    }
}

