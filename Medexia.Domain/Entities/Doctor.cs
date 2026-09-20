using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medexia.Domain.Entities
{
    public class Doctor:BaseEntity
    {
      
        public string Description { get; set; }
        [ForeignKey("Specialty")]
        public int SpecialtyId { get; set; }
        public Specialty Specialty { get; set; }
        [ForeignKey("User")]
        public string ApplicationUserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = null!;
        public ICollection<TimeTable> TimeTable { get; set; }
        public ICollection<_Rate> Rates { get; set; }

    }
}
