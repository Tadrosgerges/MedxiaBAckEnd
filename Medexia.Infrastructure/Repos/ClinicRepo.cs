using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medexia.Domain.Entities;
using Medexia.Domain.Interfaces;

namespace Medexia.Infrastructure.Repos
{
    public class ClinicRepo:Reposatory<Clinic> , IClinic
    {
        private readonly MedexiaContext context;

        public ClinicRepo(MedexiaContext context) : base(context)
        {
            this.context = context;
        }

        public  async Task<List<Clinic>> GetAlldoctorsClinic(int DocId)
        {
            return  context.clinic.Where(x => x.DoctorID == DocId && x.IsDeleted == false).ToList();
        }
    }
}
