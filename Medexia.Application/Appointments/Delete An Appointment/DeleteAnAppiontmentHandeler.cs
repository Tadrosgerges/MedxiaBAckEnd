using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Configuration.Annotations;
using Medexia.Application.user;
using Medexia.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Medexia.Application.Appointment.Delete_An_Appointment
{
    public class DeleteAnAppiontmentHandeler : IRequestHandler<DeleteAnAppiontmentCommand, Resultt>
    {
        private readonly IHttpContextAccessor acc;

        public IAppointment ApoRepo { get; }
        public IPatient PatientRepo { get; }

        public DeleteAnAppiontmentHandeler(IAppointment apoRepo , IHttpContextAccessor acc , IPatient patientRepo)
        {
            ApoRepo = apoRepo;
            this.acc = acc;
            PatientRepo = patientRepo;
        }

        

        public async Task<Resultt> Handle(DeleteAnAppiontmentCommand request, CancellationToken cancellationToken)
        {
            var existinguser =  acc.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            if (existinguser != null) 
            {
                var patient = await PatientRepo.GetAsync(x => x.ApplicationUserId == existinguser);
                if (patient != null)
                {
                    var patientId = patient.Id;
                    var appointmnet = await ApoRepo.GetAsync(x => x.Id == request.AppointmentId && x.PatientId == patientId);
                    if (appointmnet != null)
                    {
                        if (appointmnet.AppointmentDate > today.AddDays(1))
                        {
                            await ApoRepo.SoftDelete(appointmnet);
                            await ApoRepo.saveAsync();
                            Resultt result = new Resultt() { IsSuccess = true, Message = $"Appointement With Id {appointmnet.Id} Has been Deleted Sucessfully" };
                            return result;
                        }
                        Resultt result0 = new Resultt() { IsSuccess = false , Message = $"You cannot Delete An Appointment Before Its date by 1 Day" }; 
                        return result0;

                    }

                    Resultt result1 = new Resultt() { IsSuccess = false, Message = "There is no Appointment with This Id" };
                    return result1;

                }
                Resultt result2 = new Resultt() { IsSuccess = false, Message = "Error has been Occured , Try again" };
                return result2;

            }
            Resultt result3 = new Resultt() { IsSuccess = false, Message = "Login with your Account and Try again" };
            return result3;
            
        }
    }
}
