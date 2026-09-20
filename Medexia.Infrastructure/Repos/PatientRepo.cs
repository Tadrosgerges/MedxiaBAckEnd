using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medexia.Domain.Entities;
using Medexia.Domain.Interfaces;

namespace Medexia.Infrastructure.Repos
{
    public class PatientRepo : Reposatory<Patient>, IPatient
    {
        private readonly MedexiaContext context;

        public PatientRepo(MedexiaContext context) : base(context)
        {
            this.context = context;
        }

    }
}
