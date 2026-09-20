using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medexia.Application.user;
using MediatR;

namespace Medexia.Application.ClinicSetup.Command.AddClinic
{
    public class AddClinicCommand:IRequest<Resultt>
    {
        public string Address {  get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

    }
}
