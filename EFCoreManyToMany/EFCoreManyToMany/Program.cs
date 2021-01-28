using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EFCoreManyToMany
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await Configure();

            await AddData();

            await AddDataWithRole();

            await ShowData();

            await ShowDataWithRole();
        }

        private static async Task ShowData()
        {
            using (var db = new DataContext())
            {
                var employee = await db.Employees
                    .Include(e => e.Projects)
                    .FirstOrDefaultAsync();

                Console.WriteLine($"{employee.FirstName} {employee.LastName}:");
                foreach (var project in employee.Projects)
                {
                    Console.WriteLine($"\t{project.Name}");
                }
            }
        }

        private static async Task ShowDataWithRole()
        {
            using (var db = new DataContext())
            {
                var employee = await db.Employees
                    .Include(e => e.EmployeeProjects)
                    .ThenInclude(p => p.Project)
                    .FirstOrDefaultAsync();

                Console.WriteLine($"{employee.FirstName} {employee.LastName}:");
                foreach (var item in employee.EmployeeProjects)
                {
                    Console.WriteLine($"\t{item.Project.Name} - {item.Role}");
                }
            }
        }

        private static async Task AddData()
        {
            using (var db = new DataContext())
            {
                var project1 = new Project()
                {
                    Name = "project 1"
                };

                await db.Projects.AddAsync(project1);

                var project2 = new Project()
                {
                    Name = "project 2"
                };

                await db.Projects.AddAsync(project1);

                var employee = new Employee()
                {
                    FirstName = "Daniel",
                    LastName = "Plawgo",
                    Projects = new List<Project>()
                    {
                        project1,
                        project2
                    }
                };

                await db.Employees.AddAsync(employee);

                await db.SaveChangesAsync();
            }
        }

        private static async Task AddDataWithRole()
        {
            using (var db = new DataContext())
            {
                var project3 = new Project()
                {
                    Name = "project 3"
                };

                await db.Projects.AddAsync(project3);

                var employee = await db.Employees
                    .FirstOrDefaultAsync();

                var employeeProject = new EmployeeProject()
                {
                    Project = project3,
                    Employee = employee,
                    Role = "Owner"
                };

                employee.EmployeeProjects.Add(employeeProject);

                await db.SaveChangesAsync();
            }
        }

        private static async Task Configure()
        {
            var fileName = "EFCoreManyToMany.db";
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            using (var db = new DataContext())
            {
                await db.Database.MigrateAsync();
            }
        }
    }
}
