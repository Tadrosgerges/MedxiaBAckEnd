using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Configuration;
using Medexia.Application.user;
using Medexia.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Medexia.Application.Doctors.Command.Next_Patient
{
    public class NextPatientHandeller : IRequestHandler<NextPatientCommand, Resultt>
    {
        private readonly IQueueItem queueRepo;
        private readonly ITimeTable timeRepo;
        private readonly IHttpContextAccessor acc;
        private readonly IDoctor docRepo;
        private readonly IQueueNotifier notifier;
        private readonly ILogger<NextPatientHandeller> logger;
        private readonly IAppointment appoRepo;

        public NextPatientHandeller(IQueueItem QueueRepo , ITimeTable TimeRepo , IHttpContextAccessor acc , IDoctor DocRepo , IQueueNotifier notifier , ILogger<NextPatientHandeller> logger , IAppointment AppoRepo)
        {
            queueRepo = QueueRepo;
            timeRepo = TimeRepo;
            this.acc = acc;
            docRepo = DocRepo;
            this.notifier = notifier;
            this.logger = logger;
            appoRepo = AppoRepo;
        }

        public async Task<Resultt> Handle(NextPatientCommand request, CancellationToken cancellationToken)
        {
            var exisitingUserId = acc.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (exisitingUserId == null)
                return new Resultt() { IsSuccess = false, Message = "You Are not Authorized" };
            var doc = await docRepo.GetAsync(x => x.ApplicationUserId == exisitingUserId);
            if (doc == null)
                return new Resultt() { IsSuccess = false, Message = "Login As A Doctor And TryAgain" };
            var timeTable = await timeRepo.GetAsync(x => x.DoctorId == doc.Id && x.Id == request.TimeTableId);
            if (timeTable == null)
                return new Resultt() { IsSuccess = false, Message = "This TimeTable does not belong to you" };
            var QueueAppointments = await appoRepo.GetTimeTableAppointments(request.TimeTableId);
            if (QueueAppointments == null || QueueAppointments.Count == 0)
                return new Resultt() { IsSuccess = false , Message = "There Is No One In This Queue"};

            var currentqueueMember = await queueRepo.Currentmember(request.TimeTableId);
            if (currentqueueMember != null)
            {
               
                currentqueueMember.Status = Domain.Enums.AppointmentStatus.Completed;
                await queueRepo.UpdateAsync(currentqueueMember);
                var nextMember = await queueRepo.NextMemberQueue(request.TimeTableId);
                if (nextMember == null)
                {
                     QueueAppointments.ForEach(x=>x.Status=Domain.Enums.AppointmentStatus.Completed);
                    await appoRepo.saveAsync();
                    await queueRepo.saveAsync();
                    return new Resultt { IsSuccess = true, Message = "The queue Has been Ended" };
                }
                nextMember.Status = Domain.Enums.AppointmentStatus.InProgress;
                await queueRepo.UpdateAsync(nextMember);
                await queueRepo.saveAsync();
                try
                {
                    await notifier.NotifyNextPatient(request.TimeTableId, new
                    {
                        PatientId = nextMember.PatientId,
                        PatientName = nextMember.PatientName,
                        QueueNumber = nextMember.QueueNumber
                    });
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"Failed to notify next patient via SignalR for TimeTableId {timeTable.Id}", request.TimeTableId);
                }
                return new Resultt() { IsSuccess = true, Message = $"The Patient Number {nextMember.QueueNumber} Is Coming" };

            }
            QueueAppointments.ForEach(x => x.Status = Domain.Enums.AppointmentStatus.Completed);
            await appoRepo.saveAsync();
            return new Resultt() { IsSuccess = false, Message = "Queue has been Ended" };
        }
            //if (nextMember == null)
            //{
            //   
            //    await queueRepo.UpdateAsync(currentqueueMember);
            //   

            //   
            //}


           
    }
}
