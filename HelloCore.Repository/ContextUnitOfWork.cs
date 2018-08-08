using HelloCore.Interface;
using HelloCore.Interface.Manually;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace HelloCore.Repository
{

    /// <summary>
    /// can't be create by Container.
    /// only create by every thread would make sure it's ThreadStatic
    /// </summary>
    public class ContextUnitOfWork : IContextUnitOfWork
    {
        [ThreadStatic]
        private static Lazy<IContextUnitOfWork> current;

        private IDbContextTransaction transaction;
        private readonly IDbContext context;
        
        public static Lazy<IContextUnitOfWork> Current
        {
            get
            {
                return current;
            }
            set
            {
                current = value;
            }
        }
        
        public DbSet<TEntity> DataSet<TEntity> () where TEntity : class
        {
                return context.Set<TEntity>();
        }
        

        public ContextUnitOfWork(IDbContext context)
        {
            this.context = context;
        }

        public void BeginTransaction(IsolationLevel isolationLevel)
        {
            transaction = context.ContextDatabase.BeginTransaction(isolationLevel);
        }

        public void Commit()
        {
            try
            {
                context.ContextSaveChanges();
                if (transaction != null)
                    transaction.Commit();
            }
            catch (Exception ex)
            {
                //todo
                throw ex;
            }
        }

        public void Rollback()
        {
            transaction.Rollback();
        }

        public void SaveChanges()
        {
            try
            {
                context.ContextSaveChanges();
            }
            catch (Exception ex)
            {
                //todo
                throw ex;
            }
        }

        public void Dispose()
        {
            try
            {
                context.ContextDispose();
            }
            catch (Exception ex)
            {
                //todo
                throw ex;
            }
        }

        public IDbContext GetDbContext()
        {
            return context;
        }

        public EntityEntry Entry(object entity)
        {
            return context.Entry(entity);
        }
    }

}
