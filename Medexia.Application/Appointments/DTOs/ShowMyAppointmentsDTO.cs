using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medexia.Domain.Enums;

namespace Medexia.Application.Appointments.DTOs
{
    public class ShowMyAppointmentsDTO
    {
        public string DoctorName { get; set; }

        public DateOnly AppointmentDate { get; set; }
        public int AppointmentId { get; set; }
       public int TimeTableID { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public int YourOrderInTheQueue {  get; set; }
        public AppointmentStatus status { get; set; }
        public bool IsItRated {  get; set; }


    }
}
