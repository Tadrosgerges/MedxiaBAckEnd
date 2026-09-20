using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medexia.Domain.Entities;
using Medexia.Domain.Interfaces;

namespace Medexia.Infrastructure.Repos
{
    public class DoctorRepo : Reposatory<Doctor>, IDoctor
    {
        public DoctorRepo(MedexiaContext context) : base(context)
        {
        }
    }
}
