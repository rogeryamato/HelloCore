using HelloCore.DomainModel;
using HelloCore.Interface;
using HelloCore.Interface.Manually;
using HelloCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

namespace HelloCore.Repository
{


    public class HelloCoreDataContext : DbContext, IDbContext
    {
        public HelloCoreDataContext(DbContextOptions<HelloCoreDataContext> options) : base(options)
        {
            
        }
        
        public DbSet<Task> Task { get; set; }
        public DbSet<TaskItem> TaskItem { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddEntityConfigurationsFromAssembly(GetType().GetTypeInfo().Assembly);
        }

        public void ContextSaveChanges()
        {
            this.SaveChanges();
        }

        public void ContextDispose()
        {
            this.Dispose();
        }

        public DatabaseFacade ContextDatabase
        {
            get
            {
                return this.Database;
            }
        }
    }
    
}
