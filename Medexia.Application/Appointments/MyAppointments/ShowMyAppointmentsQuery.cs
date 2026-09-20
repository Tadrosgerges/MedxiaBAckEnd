using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medexia.Application.Appointments.DTOs;
using Medexia.Application.user;
using MediatR;

namespace Medexia.Application.Appointments.MyAppointments
{
    public class ShowMyAppointmentsQuery:IRequest<GenericResult<List<ShowMyAppointmentsDTO>>>
    {
    }
}
