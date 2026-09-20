using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Medexia.Domain.Entities;

namespace Medexia.Domain.Interfaces
{
    public interface IRepo<T> where T : class
    {
        Task<List<T>> GetAllAsync(string include = "");
        Task<T> GetByIdAsync(int id);
        Task UpdateAsync(T obj);
        Task RemoveAsync(T obj);
        Task AddAsync(T obj);
        Task saveAsync();
        Task<T> GetAsync(Expression<Func<T, bool>> predicate);
        public Task SoftDelete(T obj);
    }
}
