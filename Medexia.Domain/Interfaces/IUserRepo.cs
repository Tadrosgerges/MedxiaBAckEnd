using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medexia.Domain.Entities;

namespace Medexia.Domain.Interfaces
{
    public interface IUserRepo
    {
        public Task<ApplicationUser> getuserById(string id);
        public Task<List<ApplicationUser>> GetAll();
        public Task UpdateAsync(ApplicationUser user);
    }
}
