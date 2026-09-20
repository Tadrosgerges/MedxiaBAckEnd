using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Medexia.Application.Appointments.DTOs;
using Medexia.Application.user;
using Medexia.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Medexia.Application.Appointments.MyAppointments
{
    public class ShowMyAppointmentsHandeller : IRequestHandler<ShowMyAppointmentsQuery, GenericResult<List<ShowMyAppointmentsDTO>>>
    {
        private readonly IHttpContextAccessor acc;
        private readonly IAppointment appoRepo;
        private readonly IPatient patientRepo;
        private readonly ITimeTable timeTableRepo;
        private readonly IQueueItem queueRepo;

        public ShowMyAppointmentsHandeller(IHttpContextAccessor acc , IAppointment appoRepo , IPatient patientRepo , ITimeTable TimeTableRepo , IQueueItem QueueRepo )
        {
            this.acc = acc;
            this.appoRepo = appoRepo;
            this.patientRepo = patientRepo;
            timeTableRepo = TimeTableRepo;
            queueRepo = QueueRepo;
        }
        public async Task<GenericResult<List<ShowMyAppointmentsDTO>>> Handle(ShowMyAppointmentsQuery request, CancellationToken cancellationToken)
        {
            var existingUserid =acc.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (existingUserid == null)
                return GenericResult<List<ShowMyAppointmentsDTO>>.Failure("UnAuthorized User") ;
            var patient = await patientRepo.GetAsync(x => x.ApplicationUserId == existingUserid);
            if (patient == null)
                return GenericResult<List<ShowMyAppointmentsDTO>>.Failure("UnRegistered As Patient");
            var appointments = await appoRepo.PatientAppointment(patient.Id);
            if (appointments == null)
                return GenericResult<List<ShowMyAppointmentsDTO>>.Failure("There is No Booked Appointments");
       
            List<ShowMyAppointmentsDTO> OutDto = new List<ShowMyAppointmentsDTO>();
            foreach (var item in appointments)
            {
                var queueorder = await queueRepo.GetAsync(x=>x.AppointmentId == item.Id &&x.IsDeleted ==false);

                ShowMyAppointmentsDTO dto = new ShowMyAppointmentsDTO() { DoctorName = item.TimeTable.Doctor.User.FirstName ,
                    AppointmentDate = item.AppointmentDate , StartTime = item.TimeTable.StartTime , EndTime = item.TimeTable.EndTime , TimeTableID = item.TimeTableId , YourOrderInTheQueue = queueorder?.QueueNumber ??0 , AppointmentId = item.Id , status = item.Status, IsItRated = item.isItRated};
                OutDto.Add(dto);
            }
            return GenericResult<List<ShowMyAppointmentsDTO>>.Success(OutDto);


        }
    }
}
