using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Medexia.Application.ClinicSetup.DTOs;
using Medexia.Application.Patients.DTOs;
using Medexia.Application.user;
using Medexia.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Medexia.Application.ClinicSetup.Query.GetClinics_of_Doctor
{
    public class GetDoctorClinicsHandeller : IRequestHandler<GetDoctorClinicsQuery, GenericResult<List<GetDoctorClinicsDTO>>>
    {
        private readonly IHttpContextAccessor acc;
        private readonly IDoctor docRepo;
        private readonly IClinic clinicRepo;

        public GetDoctorClinicsHandeller(IHttpContextAccessor acc , IDoctor docRepo , IClinic clinicRepo)
        {
            this.acc = acc;
            this.docRepo = docRepo;
            this.clinicRepo = clinicRepo;
        }
        public async Task<GenericResult<List<GetDoctorClinicsDTO>>> Handle(GetDoctorClinicsQuery request, CancellationToken cancellationToken)
        {
            var existingUserId = acc.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var doc =await docRepo.GetAsync(x=>x.ApplicationUserId ==  existingUserId);
            if (doc == null)
            { return GenericResult<List<GetDoctorClinicsDTO>>.Failure("User Not Found , Login as doctor and Tryagain"); }
            var Clinics = await clinicRepo.GetAlldoctorsClinic(doc.Id);
            if (Clinics.Count == 0)
            { return GenericResult<List<GetDoctorClinicsDTO>>.Failure("There is No clinics for This Doc"); }
            if (Clinics ==  null)
            { return GenericResult<List<GetDoctorClinicsDTO>>.Failure("Invalid ID , There is no Clinics for This Doctor"); }
           List< GetDoctorClinicsDTO> dto = new List < GetDoctorClinicsDTO>();
            foreach (var item in Clinics)
            {
                GetDoctorClinicsDTO Dto0 = new GetDoctorClinicsDTO() { Id = item.Id, Address = item.Address, Latitude = item.Latitude, Longitude = item.Longitude };
                dto.Add(Dto0);
                
            }
            return GenericResult<List<GetDoctorClinicsDTO>>.Success(dto);

        }
    }
}
