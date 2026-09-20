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

namespace Medexia.Application.Doctors.Command.Start_a_week_TimeTable
{
    public class StartAweekTimeTableHandeller : IRequestHandler<StartAweekTimeTableCommand, Resultt>
    {
        private readonly IHttpContextAccessor acc;
        private readonly ITimeTable timeRepo;
        private readonly IDoctor docRepo;

        public StartAweekTimeTableHandeller(IHttpContextAccessor acc, ITimeTable TimeRepo, IDoctor docRepo)
        {
            this.acc = acc;
            timeRepo = TimeRepo;
            this.docRepo = docRepo;
        }
        public async Task<Resultt> Handle(StartAweekTimeTableCommand request, CancellationToken cancellationToken)
        {

            var existingUserId = acc.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (existingUserId == null)
                return new Resultt { IsSuccess = false, Message = "UnAuthorized" };
            var doc = await docRepo.GetAsync(x => x.ApplicationUserId == existingUserId);
            if (doc == null)
                return new Resultt { IsSuccess = false, Message = "You Are not Registerd As A Doctor" };
            var TimeTables = await timeRepo.GetDocTimeTableByDate(doc.Id, request.TimetableId , request.day);
            if (TimeTables == null)
                return new Resultt { IsSuccess = false, Message = "TimeTable NotFound" };
            if (TimeTables.IsActive == false)
            {
                TimeTables.IsActive = true;
                await timeRepo.UpdateAsync(TimeTables);
                await timeRepo.saveAsync();
                return new Resultt { IsSuccess = true, Message = $"Timetable {TimeTables.Id} has been started again" };
            }
            return new Resultt { IsSuccess = false, Message = $"TimeTable {TimeTables.Id} Is already inProgress"
            };
        }
    }
}
