using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medexia.Domain.Entities;

namespace Medexia.Domain.Interfaces
{
    public interface IClinic:IRepo<Clinic>
    {
        Task<List<Clinic>> GetAlldoctorsClinic(int Id);
    }
}
