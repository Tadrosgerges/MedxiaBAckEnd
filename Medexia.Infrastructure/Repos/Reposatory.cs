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
    public class Reposatory<T> : IRepo<T> where T : BaseEntity
    {
        private readonly MedexiaContext context;

        public Reposatory(MedexiaContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(T obj)
        {
          await context.Set<T>().AddAsync(obj);
        }

        public async Task<List<T>> GetAllAsync(string include = "")
        {
           if (include == "")
            {
                return context.Set<T>().Where(o => o.IsDeleted == false).ToList();

            }
           else
            {
                return context.Set<T>().Include(include).Where(o => o.IsDeleted == false).ToList();

            }
        }

        public async Task<T> GetAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return await context.Set<T>().Where(x => x.IsDeleted == false).FirstOrDefaultAsync(predicate);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return context.Set<T>()
                 .FirstOrDefault(s => s.Id == id && s.IsDeleted == false);
        }

        public async Task RemoveAsync(T obj)
        {
            context.Set<T>().Remove(obj);
        }
        

        public async Task saveAsync()
        {
            context.SaveChanges();
        }

        public async Task UpdateAsync(T obj)
        {
            context.Update(obj);
        }
        public async Task SoftDelete(T Obj  )
        {
            Obj.IsDeleted = true;
        }
    }
}
