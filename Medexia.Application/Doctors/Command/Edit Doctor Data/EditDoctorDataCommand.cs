using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medexia.Application.user;
using Medexia.Domain.Entities;
using MediatR;

namespace Medexia.Application.Doctors.Command.Edit_Doctor_Data
{
    public class EditDoctorDataCommand:IRequest<Resultt>
    { 
        public string? description {  get; set; }
       
        public string? ProfilePictureURL { get; set; }
        
    }
}
