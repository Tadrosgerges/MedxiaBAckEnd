using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace Medexia.Domain.Interfaces
{
    public interface IUnitOfWork
    {


        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
