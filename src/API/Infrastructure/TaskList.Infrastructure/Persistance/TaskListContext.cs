using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TaskList.Application.Interfaces;
using TaskList.Domain.Entities;

namespace TaskList.Infrastructure.Persistance
{

    /// <summary>
    /// Represents the application context derived from <see cref="DbContext"/>.
    /// </summary>
    public class TaskListContext : DbContext, IApplicationContext
    {
        public TaskListContext(DbContextOptions<TaskListContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TaskListEntity>()
                .HasMany(p=>p.Tasks)
                .WithOne(c=>c.TaskList)
                .HasForeignKey(c=>c.TaskListId)
                .OnDelete(DeleteBehavior.Restrict)                ;
            modelBuilder.Entity<TaskListEntity>().HasData(SeedData.TaskLists);
            modelBuilder.Entity<TaskEntity>().HasData(SeedData.Tasks);
            modelBuilder.Entity<CommentEntity>().HasData(SeedData.Comments);
            modelBuilder.Entity<StatusTaskHistory>().HasData(SeedData.StatusTasks);
        }
    }
}
