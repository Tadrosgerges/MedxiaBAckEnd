using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Medexia.Application.user;
using Medexia.Domain.Entities;
using Medexia.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Medexia.Application.ClinicSetup.Command.AddClinic
{
    public class AddClinicHandeller : IRequestHandler<AddClinicCommand, Resultt>
    {
        private readonly IHttpContextAccessor acc;
        private readonly IDoctor doc;
        private readonly IClinic clinicRepo;

        public AddClinicHandeller(IHttpContextAccessor acc , IDoctor doc, IClinic clinicRepo)
        {
            this.acc = acc;
            this.doc = doc;
            this.clinicRepo = clinicRepo;
        }
        public async Task<Resultt> Handle(AddClinicCommand request, CancellationToken cancellationToken)
        {
           var existinguserId = acc.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var doctor = await doc.GetAsync(x=>x.ApplicationUserId == existinguserId);
            if (doctor == null)
                return new Resultt() { IsSuccess = false, Message = "Login As doc , And TryAgain" };
            Clinic Hisclinic = new Clinic() 
            { Address = request.Address 
            , DoctorID = doctor.Id ,
              Latitude = request.Latitude
            , Longitude = request.Longitude,
                IsDeleted = false 
            };
           await clinicRepo.AddAsync(Hisclinic);
           await clinicRepo.saveAsync();
            return new Resultt() { IsSuccess = true, Message = $"Clinic Has been saved Successfully with Id {Hisclinic.Id}" };

        }
    }
}
