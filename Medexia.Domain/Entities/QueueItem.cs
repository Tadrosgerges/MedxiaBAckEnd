using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medexia.Domain.Enums;

namespace Medexia.Domain.Entities
{
    public class QueueItem:BaseEntity
    {
        [ForeignKey("TimeTable")]
        public int TimeTableId { get; set; }
        public TimeTable TimeTable { get; set; }
        public int docId { get; set; }
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public int QueueNumber { get; set; }       
        public AppointmentStatus Status { get; set; }
        public DateOnly dateOftheQueue { get; set; }
    }
}
