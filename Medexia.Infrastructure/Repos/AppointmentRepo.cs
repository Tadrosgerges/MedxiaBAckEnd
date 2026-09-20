using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medexia.Domain.Entities;
using Medexia.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Medexia.Infrastructure.Repos
{
    public class AppointmentRepo : Reposatory<Appointment>, IAppointment
    {
        private readonly MedexiaContext context;

        public AppointmentRepo(MedexiaContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<List<Appointment>> getappointment(int Id)
        {
          return  context.Appointments.Where(x => x.TimeTableId == Id && x.IsDeleted == false).OrderBy(x=>x.CreatedAt)
                .Include(x => x.Patient).Include(x => x.Patient.User).Include(x => x.TimeTable.Doctor).ToList();
        }
        public async Task<List<Appointment>> PatientAppointment(int PatientId)
        {
            return context.Appointments.Where(x => x.PatientId == PatientId && x.IsDeleted == false).OrderBy(x => x.CreatedAt)
                  .Include(x => x.Patient).Include(x => x.Patient.User).Include(x => x.TimeTable)
                  .Include(x=>x.TimeTable.Doctor).Include(x => x.TimeTable.Doctor.User).ToList();
        }
        public async Task<List<Appointment>> GetTimeTableAppointments(int TimeTableId)
        { return await  context.Appointments.Where(x => x.TimeTableId == TimeTableId && x.IsDeleted == false).Include(x=>x.Patient).Include(x=>x.Patient.User)
                .ToListAsync(); }
        public async Task SoftDelete(Appointment appointment) 
        {
        appointment.IsDeleted = true;
        }

    }
}
