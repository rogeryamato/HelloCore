using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelloCore.EF.Context
{
    public interface IDbContext
    {
        void ContextSaveChanges();
        void ContextDispose();
        DatabaseFacade ContextDatabase { get; }
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        EntityEntry Entry(object entity);
    }
}
