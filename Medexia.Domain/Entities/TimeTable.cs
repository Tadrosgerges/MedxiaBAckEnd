using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medexia.Domain.Entities
{
    public class TimeTable : BaseEntity
    {
        public DateOnly Date { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public bool IsActive { get; set; } = true;
        public int NumberOfPatients { get; set; }
        public int MaximumNumberOfPatients { get; set; }
        [ForeignKey("clinic")]
        public int ClinicId { get; set; }
       public Clinic? clinic { get; set; }




    }
}
