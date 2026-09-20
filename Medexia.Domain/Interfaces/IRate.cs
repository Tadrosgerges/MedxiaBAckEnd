using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medexia.Domain.Entities;

namespace Medexia.Domain.Interfaces
{
    public interface IRate:IRepo<_Rate>
    {


        public Task<double> GetAvergeRatesOfDoctor(int DoctorId);
        Task<List<_Rate>> GetCommentsAsync(int doctorId, int page, int pageSize);
        Task<int> GetRatingsCountAsync(int doctorId);

    }
}
