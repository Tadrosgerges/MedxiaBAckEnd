using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medexia.Domain.Enums;

namespace Medexia.Domain.Entities
{
    public class Patient:BaseEntity
    {
        public Gender gender { get; set; }

        public  bool HasHighBloodPressure { get; set; }
        public bool IsDiabetic { get; set; }
        public BloodType BloodType { get; set; }
        public string ? Address { get; set; }
        public ICollection<Appointment>? Appointments {  get; set; }
        [ForeignKey("User")]
        public string ApplicationUserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = null!;

    }
}
