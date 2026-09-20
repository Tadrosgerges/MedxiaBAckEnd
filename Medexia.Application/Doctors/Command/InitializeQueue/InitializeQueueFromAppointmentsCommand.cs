using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medexia.Application.user;
using Medexia.Domain.Entities;
using MediatR;

namespace Medexia.Application.Doctors.Command.InitializeQueue
{
    public class InitializeQueueFromAppointmentsCommand:IRequest<Resultt>
    {
       
        public int TimeTableId     { get; set; }
        
    }
}
