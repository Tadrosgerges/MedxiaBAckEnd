using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medexia.Domain.Entities;
using Medexia.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Medexia.Infrastructure.Repos
{
    public class RateRepo : Reposatory<_Rate>, IRate
    {
        private readonly MedexiaContext context;

        public RateRepo(MedexiaContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<double> GetAvergeRatesOfDoctor(int DoctorId)
        {
            var rating = context.Rates.Where(x => x.IsDeleted == false && x.DoctorId == DoctorId);
            if (!await rating.AnyAsync()) 
            {
                return 0;
            }
            return await rating.AverageAsync(x => x.Rating);
        }
        public async Task<List<_Rate>> GetCommentsAsync(int doctorId, int page = 1 , int pageSize = 10)
        {
            return await context.Rates.Where(x=>x.IsDeleted ==false &&  x.DoctorId == doctorId 
            && x.Comment != null && !string.IsNullOrEmpty(x.Comment))
            .OrderByDescending(x => x.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();  
        }
        public async Task<int> GetRatingsCountAsync(int doctorId)
        {
            var rates =  context.Rates.Where(x => x.IsDeleted == false && x.DoctorId == doctorId);
           return  rates.Count();
        }
    }
}
