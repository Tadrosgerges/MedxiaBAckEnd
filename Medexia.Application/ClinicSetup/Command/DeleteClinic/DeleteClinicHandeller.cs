using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Medexia.Application.user;
using Medexia.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Medexia.Application.ClinicSetup.Command.DeleteClinic
{
    public class DeleteClinicHandeller : IRequestHandler<DeleteClinicCommand, Resultt>
    {
        private readonly IHttpContextAccessor acc;
        private readonly IDoctor docrepo;
        private readonly IClinic clinicrepo;

        public DeleteClinicHandeller(IHttpContextAccessor acc, IDoctor docrepo, IClinic clinicrepo)
        {
            this.acc = acc;
            this.docrepo = docrepo;
            this.clinicrepo = clinicrepo;
        }
        public async Task<Resultt> Handle(DeleteClinicCommand request, CancellationToken cancellationToken)
        {
            var existinguserId = acc.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var Doc = await docrepo.GetAsync(x => x.ApplicationUserId == existinguserId);
            if (Doc == null)
                return new Resultt() { IsSuccess = false, Message = "Error Occured , Login as Doc And TryAgain" };
             var clinic = await clinicrepo.GetAsync(x => x.DoctorID == Doc.Id && x.Id == request.ClinicId);
            if (clinic == null)
                return new Resultt { IsSuccess = false, Message = "Invalid Clinic Id "};
            clinicrepo.SoftDelete(clinic);
            clinicrepo.saveAsync();
            return new Resultt() { IsSuccess = true, Message = $"Clinic With Id {clinic.Id} Has Been Deleted " };
     
        }
    }
}
