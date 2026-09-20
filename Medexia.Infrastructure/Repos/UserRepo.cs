using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medexia.Domain.Entities;
using Medexia.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Medexia.Infrastructure.Repos
{
    internal class UserRepo : IUserRepo
    {
        private readonly UserManager<ApplicationUser> manger;

        public UserRepo(UserManager<ApplicationUser> manger)
        {
            this.manger = manger;
        }

        public Task<List<ApplicationUser>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<ApplicationUser> getuserById(string id)
        {
           return await manger.FindByIdAsync(id);
        }

        public async Task UpdateAsync(ApplicationUser user)
        {
           await manger.UpdateAsync(user);
        }
    }
}
