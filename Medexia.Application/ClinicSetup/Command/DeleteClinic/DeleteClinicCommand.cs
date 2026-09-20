using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medexia.Application.user;
using MediatR;

namespace Medexia.Application.ClinicSetup.Command.DeleteClinic
{
    public class DeleteClinicCommand:IRequest<Resultt>
    {
        public int ClinicId { get; set; }
    }
}
