using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medexia.Application.user;
using MediatR;

namespace Medexia.Application.Doctors.Command.Next_Patient
{
   public class NextPatientCommand:IRequest<Resultt>
    {
        public int TimeTableId { get; set; }
        //public int PatientId { get; set; }
    }
}
