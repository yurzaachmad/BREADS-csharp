using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using myFirstWeb.Models;

namespace myFirstWeb.Data
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext (DbContextOptions<EmployeeContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<User> Users { get; set; }
       public DbSet<TaskClass> Tasks { get; set; }
	//	public DbSet<ProjectEmployee> Projectss { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable("Employee");
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<TaskClass>().ToTable("Tasks");
		//	modelBuilder.Entity<Project>().ToTable("Project");
		}

        public DbSet<myFirstWeb.Models.Employee> Employee { get; set; } = default!;
    }
}
