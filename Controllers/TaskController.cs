using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using myFirstWeb.Data;
using myFirstWeb.Models;
using System.Reflection.Metadata;
using System;

namespace myFirstWeb.Controllers
{
    public class TaskController : Controller
    {
        private readonly EmployeeContext employeeContext;
        public TaskController(EmployeeContext employeeContext)
        {
            this.employeeContext = employeeContext;
		}
		public async Task <IActionResult> Index()
		{
			/*
			var query = from b in employeeContext.Set<Project>()
						from p in employeeContext.Set<Employee>()
						select new { b, p };
			var projects = await query.ToListAsync();
            return View(projects);
            */
			
			var query = from b in employeeContext.Set<ProjectEmployee>()
						join p in employeeContext.Set<Employee>() on b.ID_employeeID equals p.ID
						select new ProjectEmployee
						{
							ID = b.ID,
							ProjectName = b.ProjectName,
							StartDate = b.StartDate,
							EndDate = b.EndDate,
							EmployeeName = p.Name,
							Complete = b.Complete
						}; // Select desired properties
			var projects = await query.ToListAsync();
			return View(projects);
			
			

		//	var taskEmployee = await employeeContext.Projects.ToListAsync();
		//	return View(taskEmployee);
		}

    }
}
