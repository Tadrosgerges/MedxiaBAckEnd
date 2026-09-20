using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medexia.Application.user;
using MediatR;

namespace Medexia.Application.Doctors.Command.Start_a_week_TimeTable
{
    public class StartAweekTimeTableCommand:IRequest<Resultt>
    {
        public int TimetableId { get; set; }
        public DateOnly day {  get; set; }
    }
}
