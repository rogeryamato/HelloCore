using System;
using System.Collections.Generic;
using System.Text;
using HelloCore.DomainModel;
using HelloCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelloCore.Repository
{
    public class TaskMap : EntityMappingConfiguration<Task>
    {
        public override void Map(EntityTypeBuilder<Task> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);

            entityBuilder.ToTable("Task");

            entityBuilder.Property(t => t.Id).HasColumnName("Id");
            entityBuilder.Property(t => t.Name).HasColumnName("Name");
            entityBuilder.Property(t => t.Description).HasColumnName("Description");
        }
    }
    public class TaskItemMap : EntityMappingConfiguration<TaskItem>
    {
        public override void Map(EntityTypeBuilder<TaskItem> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);

            entityBuilder.ToTable("TaskItem");
            
            entityBuilder.Property(t => t.Id).HasColumnName("Id");
            entityBuilder.Property(t => t.Name).HasColumnName("Name");
            entityBuilder.Property(t => t.Priority).HasColumnName("Priority");
            entityBuilder.Property(t => t.Description).HasColumnName("Description");
            entityBuilder.Property(t => t.TaskId).HasColumnName("TaskId");
        }
    }
}
