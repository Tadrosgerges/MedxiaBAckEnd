using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medexia.Domain.Entities;

namespace Medexia.Domain.Interfaces
{
    public interface ITimeTable:IRepo<TimeTable>
    {
        public  Task<List<TimeTable>> GetAllDocAsync();
        public Task<List<TimeTable>> GetAllDocAsyncwithclinic();

        public Task<TimeTable> GetDocTimeTableByDate(int DocId, int TimeTableId, DateOnly date);
        public Task<TimeTable> GetDocTimeTable(int DocId, int TimeTableId);
        public Task<List<TimeTable>> GetTimeTableOfdoc(int DocId);
        //public Task<TimeTable> GetDocTimeTableAsync(
    }
}
