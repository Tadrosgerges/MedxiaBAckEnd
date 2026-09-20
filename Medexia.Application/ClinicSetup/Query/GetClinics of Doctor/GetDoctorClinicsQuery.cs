using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medexia.Application.ClinicSetup.DTOs;
using Medexia.Application.user;
using MediatR;

namespace Medexia.Application.ClinicSetup.Query.GetClinics_of_Doctor
{
    public class GetDoctorClinicsQuery:IRequest<GenericResult<List<GetDoctorClinicsDTO>>>
    {
        
    }
}
