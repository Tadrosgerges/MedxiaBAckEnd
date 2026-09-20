using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medexia.Domain.Entities;

namespace Medexia.Domain.Interfaces
{
    public interface IQueueItem:IRepo<QueueItem>
    {
        Task<bool> AnyForTimeTable(int id);
        public Task<QueueItem> LastOne(int docId, int timetableId);
        public Task<List<QueueItem>> GetQueueList(int Timetable);
        public Task<QueueItem> Currentmember(int TimeTableId);
       public Task<QueueItem> NextMemberQueue(int TimeTable);
        public Task <int> ReminingUntilMyOrder(int TimeTableId,int patientId);
    }
}
