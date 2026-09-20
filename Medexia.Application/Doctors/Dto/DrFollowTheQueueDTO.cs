using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medexia.Domain.Enums;

namespace Medexia.Application.Doctors.Dto
{
    public class DrFollowTheQueueDTO
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public string PatientName { get; set; }
        public int PatientId { get; set; }
        public Gender Gender { get; set; }
        public BloodType BloodType { get; set; }
        public bool IsDiabatic { get; set; }
        public bool HasHIghBloodPressure { get; set; }
        public int QueueNumber { get; set; }
        public string profilePicURL { get; set; }

    }
}
