using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace HelloCore.Interface.Manually
{
    public interface IContextUnitOfWork
    {

        void BeginTransaction(IsolationLevel isolationLevel);

        void Commit();

        void SaveChanges();

        void Rollback();

        void Dispose();

        //IDbContext DbContext { get; }

        DbSet<TEntity> DataSet<TEntity>() where TEntity : class;

        EntityEntry Entry(object entity);
    }
}
