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

namespace Medexia.Application.Doctors.Command.InitializeQueue
{
    public class InitializeQueueFromAppointmentsHandeller : IRequestHandler<InitializeQueueFromAppointmentsCommand, Resultt>
    {
        private readonly IHttpContextAccessor acc;
        private readonly IQueueItem queuerepo;
        private readonly IDoctor docrepo;
        private readonly IPatient patientRepo;
        private readonly IAppointment appoRepo;
        private readonly ITimeTable timeRepo;

        public InitializeQueueFromAppointmentsHandeller(IHttpContextAccessor acc, IQueueItem queuerepo, IDoctor docrepo, IPatient patientRepo
             , IAppointment appoRepo, ITimeTable timeRepo)
        {
            this.acc = acc;
            this.queuerepo = queuerepo;
            this.docrepo = docrepo;
            this.patientRepo = patientRepo;
            this.appoRepo = appoRepo;
            this.timeRepo = timeRepo;
        }



        public async Task<Resultt> Handle(InitializeQueueFromAppointmentsCommand request, CancellationToken cancellationToken)
        {
            var existinguserId = acc.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var existingDocId = await docrepo.GetAsync(x => x.ApplicationUserId == existinguserId);
            var MyTimeTable = await timeRepo.GetDocTimeTable( existingDocId.Id ,request.TimeTableId );
            if (MyTimeTable == null)
            {
                Resultt result0 = new Resultt() { IsSuccess = false, Message = "There is no TimeTable For This Doc With This ID" };
                return result0;
            }
            var queue = await queuerepo.LastOne(existingDocId.Id, MyTimeTable.Id);
            int lastnum = queue?.QueueNumber ?? 0;
            var appointments = await appoRepo.getappointment(MyTimeTable.Id);
            if (appointments == null || !appointments.Any())
                return new Resultt { IsSuccess = false, Message = "There no Appointemnet With This specification" };

            var alreadyExists = await queuerepo.AnyForTimeTable(MyTimeTable.Id);
            if (alreadyExists)
                return new Resultt { IsSuccess = false, Message = "Queue already initialized for this TimeTable" };

            var queueItems = appointments.Select((item, index) => new QueueItem
            {
                docId = existingDocId.Id,
                TimeTableId = item.TimeTableId,
                PatientId = item.PatientId,
                AppointmentId = item.Id,
                PatientName = item.Patient.User.FirstName,
                QueueNumber = lastnum + index + 1,
                Status = Domain.Enums.AppointmentStatus.Waiting,
                dateOftheQueue = MyTimeTable.Date
            }).ToList();
            foreach (var item in queueItems)
            {
                queuerepo.AddAsync(item);
               
            }
            var firstone = queueItems.First();
            firstone.Status = Domain.Enums.AppointmentStatus.InProgress;
            queuerepo.UpdateAsync(firstone);
            queuerepo.saveAsync();
           
            return new Resultt { IsSuccess = true, Message = "Queue Has been Started" };









        }
    }
}
