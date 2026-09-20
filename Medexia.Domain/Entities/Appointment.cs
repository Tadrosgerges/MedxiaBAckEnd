using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medexia.Domain.Enums;

namespace Medexia.Domain.Entities
{
    public class Appointment: BaseEntity
    {
        [ForeignKey("TimeTable")]
        public int TimeTableId { get; set; }
        public TimeTable TimeTable { get; set; }
        public DateOnly AppointmentDate {  get; set; }   
        public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        public Patient Patient { get; set; } = null;
        public bool isItRated { get; set; } = false;
    }
}
