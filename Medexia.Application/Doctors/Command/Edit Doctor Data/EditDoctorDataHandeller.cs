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
using Microsoft.AspNetCore.Identity;

namespace Medexia.Application.Doctors.Command.Edit_Doctor_Data
{
    public class EditDoctorDataHandeller : IRequestHandler<EditDoctorDataCommand, Resultt>
    {
        private readonly IDoctor docrepo;
        private readonly IHttpContextAccessor acc;
        private readonly UserManager<ApplicationUser> manger;

        public EditDoctorDataHandeller(IDoctor docrepo , IHttpContextAccessor acc  , UserManager<ApplicationUser> manger )
        {
            this.docrepo = docrepo;
            this.acc = acc;
            this.manger = manger;
        }
        public async Task<Resultt> Handle(EditDoctorDataCommand request, CancellationToken cancellationToken)
        {
           var existingUserid = acc.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (existingUserid == null)
            { return new Resultt { IsSuccess = false, Message = "You are not Authorized , Login And TryAgain" }; }
            var user = await manger.FindByIdAsync(existingUserid);
            var doc = await docrepo.GetAsync(x=>x.ApplicationUserId == existingUserid);
            if (doc == null)
            { return new Resultt { IsSuccess = false, Message = "You are Not Registred as a Doctor " }; }
            if (request.description != null)
            {
                doc.Description = request.description;
            }

            if (request.ProfilePictureURL != null)
            {
                user.ProfilePictureURL = request.ProfilePictureURL;
            }
            await docrepo.UpdateAsync(doc);
            await manger.UpdateAsync(user);
            await docrepo.saveAsync();
            return new Resultt { IsSuccess = true, Message = $"The Data Of Dr {user.FirstName} Has Been Updated" };


        }
    }
}
