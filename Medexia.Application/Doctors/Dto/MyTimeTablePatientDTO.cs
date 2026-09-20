using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medexia.Domain.Enums;

namespace Medexia.Application.Doctors.Dto
{
    public class MyTimeTablePatientDTO
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public string PatientName { get; set; }
        public int PatientId { get; set; }
        public Gender Gender { get; set; }
        public string profilePicURL { get; set; }
    }
}
