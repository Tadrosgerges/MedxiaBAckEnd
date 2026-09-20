using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medexia.Domain.Entities;
using Medexia.Domain.Interfaces;

namespace Medexia.Infrastructure.Repos
{
    public class SpecialityRepo : Reposatory<Specialty>, ISpecialty
    {
        public SpecialityRepo(MedexiaContext context) : base(context)
        {
        }
    }
}
