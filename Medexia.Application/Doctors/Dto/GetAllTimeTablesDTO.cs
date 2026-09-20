using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medexia.Application.Doctors.Dto
{
    public class GetAllTimeTablesDTO
    {
        public DayOfWeek DayOfWeek { get; set; }
        public TimeOnly startTime {  get; set; }
        public TimeOnly endTime { get; set; }
        public int TimeTableId {  get; set; }
        public bool IsActive { get; set; }
        public DateOnly Date {  get; set; }
    }
}
