using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medexia.Application.user;
using MediatR;

namespace Medexia.Application.Appointment.Delete_An_Appointment
{
    public class DeleteAnAppiontmentCommand :IRequest<Resultt>
    {
        public int AppointmentId { get; set; }
       
    }
}
