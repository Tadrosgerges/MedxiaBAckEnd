using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medexia.Domain.Enums
{
    public enum AppointmentStatus
    {
        Pending = 0,
        Waiting = 1,
        InProgress = 2,
        Completed = 3,
        Cancelled = 4,
        
    }
}
