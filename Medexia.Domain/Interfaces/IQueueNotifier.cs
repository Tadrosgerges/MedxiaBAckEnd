using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medexia.Domain.Interfaces
{
   public interface IQueueNotifier
    {
        public Task NotifyNextPatient(int timeTableId, object patientData);
    }
}
