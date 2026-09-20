using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medexia.Domain.Entities;

namespace Medexia.Domain.Interfaces
{
    public interface IAppointment :IRepo<Appointment>
    {
        public  Task SoftDelete(Appointment appointment);
        public  Task<List<Appointment>> getappointment(int Id);
        public Task<List<Appointment>> PatientAppointment(int PatientId);
        public Task<List<Appointment>> GetTimeTableAppointments(int TimeTableId);
    }
}
