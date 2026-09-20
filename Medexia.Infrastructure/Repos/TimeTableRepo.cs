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
    public class TimeTableRepo : Reposatory<TimeTable>, ITimeTable
    {
        private readonly MedexiaContext context;

        public TimeTableRepo(MedexiaContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<List<TimeTable>> GetAllDocAsync()
        {
           return await context.TimeTables.Include(dox=> dox.Doctor).Include(us=>us.Doctor.User).Include(sp=>sp.Doctor.Specialty).Include(x=>x.clinic)
                .Where(x=>x.IsDeleted == false).ToListAsync();
        }

        public async Task<TimeTable> GetDocTimeTableByDate( int DocId , int TimeTableId , DateOnly date)
        {
           return context.TimeTables.Include(x => x.Doctor).FirstOrDefault(x => x.DoctorId == DocId &&x.Id ==TimeTableId && x.Date == date && x.IsDeleted == false);
        }

        public async Task<TimeTable> GetDocTimeTable(int DocId, int TimeTableId)
        {
          return  context.TimeTables.FirstOrDefault(x=>x.DoctorId == DocId &&x.Id == TimeTableId && x.IsDeleted == false);
        }

       

        public Task<List<TimeTable>> GetTimeTableOfdoc(int DocId)
        {
          return context.TimeTables.Where(x=>x.DoctorId==DocId && x.IsDeleted == false).ToListAsync();
        }

        public async Task<List<TimeTable>> GetAllDocAsyncwithclinic()
        {
            return await context.TimeTables.Include(dox => dox.Doctor).Include(us => us.Doctor.User).Include(sp => sp.Doctor.Specialty).Include(x=>x.clinic)
               .Where(x => x.IsDeleted == false).ToListAsync();
        }
    }
}
