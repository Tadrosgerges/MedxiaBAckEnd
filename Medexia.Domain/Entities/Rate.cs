using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medexia.Domain.Entities
{
    public class _Rate : BaseEntity
    {
        [ForeignKey("Appointment")]
        public int AppointmentId { get; set; }

        public Appointment Appointment {  get; set; } = null!;
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; } = null!;

        public int Rating { get; set; }

        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
