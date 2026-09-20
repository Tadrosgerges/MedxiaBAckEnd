using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medexia.Domain.Interfaces;
using Medexia.WebApi.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Medexia.Infrastructure.Repos
{
    [Authorize]
    public class QueueNotifierRepo : IQueueNotifier
    {
        private readonly IHubContext<QueueHub> _hubContext;

        public QueueNotifierRepo(IHubContext<QueueHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task NotifyNextPatient(int timeTableId, object patientData)
        {
            var groupName = QueueHub.GetGroupName(timeTableId);
            await _hubContext.Clients.Group(groupName).SendAsync("NextPatientCalled", patientData);
        }

        public async Task IfLast(int timeTableId, object patientData)
        {
            var groupName = QueueHub.GetGroupName(timeTableId);
            await _hubContext.Clients.Group(groupName).SendAsync("NextPatientCalled", patientData);
        }
    }
}
