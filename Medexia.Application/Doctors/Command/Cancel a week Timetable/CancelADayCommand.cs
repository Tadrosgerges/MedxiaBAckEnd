using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medexia.Application.user;
using MediatR;

namespace Medexia.Application.Doctors.Command.Cancel_a_week_Timetable
{
    public class CancelADayCommand: IRequest<Resultt>
    {
        public int timetableId { get; set; }
        public DateOnly day {  get; set; }
    }
}
