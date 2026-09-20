using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medexia.Application.Doctors.Dto;
using Medexia.Application.user;
using MediatR;

namespace Medexia.Application.Doctors.Query.Follow_the_que
{
    public class DrFollow_The_queueCommand:IRequest<GenericResult<List<DrFollowTheQueueDTO>>>
    {
      public int TimeTableId { get; set; }
        public DateOnly Day {  get; set; }
       
    }
}
