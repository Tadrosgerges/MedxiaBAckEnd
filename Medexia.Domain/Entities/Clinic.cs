using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medexia.Domain.Entities
{
    public class Clinic:BaseEntity
    {
        public string Address { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        [ForeignKey("Doctor")]
        public int DoctorID { get; set; }
        public Doctor Doctor { get; set; }
    }
}
