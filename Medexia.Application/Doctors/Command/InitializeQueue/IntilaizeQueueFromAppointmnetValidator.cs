using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Medexia.Application.Doctors.Command.InitializeQueue
{
    public class IntilaizeQueueFromAppointmnetValidator:AbstractValidator<InitializeQueueFromAppointmentsCommand>
    {
        public IntilaizeQueueFromAppointmnetValidator()
        {
            RuleFor(x=>x.TimeTableId).NotEmpty().WithMessage("This TimeTableId Is Not Valid Or NotFound").GreaterThan(0);
        }
    }
}
