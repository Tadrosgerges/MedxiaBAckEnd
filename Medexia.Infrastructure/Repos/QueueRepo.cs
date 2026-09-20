using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medexia.Domain.Entities;
using Medexia.Domain.Enums;
using Medexia.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Medexia.Infrastructure.Repos
{
    public class QueueRepo : Reposatory<QueueItem>, IQueueItem
    {
        private readonly MedexiaContext context;

        public QueueRepo(MedexiaContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<bool> AnyForTimeTable(int id)
        {
            return context.QueueItems.Where(x => x.TimeTableId == id).Any();
        }

        public async Task<QueueItem> LastOne(int docId, int timetableId)
        {
            return context.QueueItems.Include(x => x.TimeTable.Doctor).OrderBy(x => x.QueueNumber)
                  .FirstOrDefault(x => x.TimeTable.DoctorId == docId && x.TimeTableId == timetableId);
        }
        public async Task<List<QueueItem>> GetQueueList(int Timetable)
        {
            return context.QueueItems.Where(x => x.TimeTableId == Timetable && x.IsDeleted ==false &&x.Status!= AppointmentStatus.Completed).ToList();

        }
        public async Task<QueueItem> Currentmember(int TimeTableId) 
        {
            return context.QueueItems.FirstOrDefault(x => x.Status == Domain.Enums.AppointmentStatus.InProgress && x.IsDeleted ==false && x.TimeTableId ==TimeTableId);
        }
        public async Task<QueueItem> NextMemberQueue(int TimeTable)
        {
            return await  context.QueueItems.Where(x => x.IsDeleted == false && x.Status == Domain.Enums.AppointmentStatus.Waiting 
            && x.TimeTableId ==TimeTable).OrderBy(x=>x.QueueNumber).FirstOrDefaultAsync();
        }

        public async Task<int> ReminingUntilMyOrder(int TimeTableId , int patientId)
        {
            var patientQueueNumber = await context.QueueItems
      .Where(x => x.PatientId == patientId
               && x.TimeTableId == TimeTableId
               && !x.IsDeleted)
      .Select(x => x.QueueNumber)
      .FirstOrDefaultAsync();

            if (patientQueueNumber == 0)
                return 0;

            return await context.QueueItems
                .CountAsync(x => x.TimeTableId == TimeTableId
                              && !x.IsDeleted
                              && x.Status == AppointmentStatus.Waiting
                              && x.QueueNumber < patientQueueNumber);
        }
    }
}
