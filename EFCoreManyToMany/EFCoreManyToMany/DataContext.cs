using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EFCoreManyToMany
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=EFCoreManyToMany.db", options =>
            {
                options.MigrationsAssembly(this.GetType().Assembly.FullName);
            });
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
                .HasMany(p => p.Employees)
                .WithMany(p => p.Projects)
                .UsingEntity<EmployeeProject>(
                    j => j
                        .HasOne(pt => pt.Employee)
                        .WithMany(t => t.EmployeeProjects)
                        .HasForeignKey(pt => pt.EmployeesId),
                    j => j
                        .HasOne(pt => pt.Project)
                        .WithMany(p => p.EmployeeProjects)
                        .HasForeignKey(pt => pt.ProjectsId),
                    j =>
                    {
                        j.Property(pt => pt.Role).HasDefaultValueSql("'employee'");
                        j.HasKey(t => new { t.EmployeesId, t.ProjectsId });
                    });
        }
    }
}
