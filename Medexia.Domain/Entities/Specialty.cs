using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medexia.Domain.Entities
{
    public class Specialty : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Doctor> Doctors { get; set; }
            = new List<Doctor>();
    }
}
