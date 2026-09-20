using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medexia.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Medexia.Infrastructure.Repos
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly MedexiaContext context;

        public UnitOfWork(MedexiaContext context)
        {
            this.context = context;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken)
        {
           var trans =  await context.Database.BeginTransactionAsync(cancellationToken);
            return trans;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
          var trans =   await context.SaveChangesAsync(cancellationToken);
            return trans;
        }
    }
}
