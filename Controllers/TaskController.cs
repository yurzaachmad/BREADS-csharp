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
			var query = from b in employeeContext.Set<TaskClass>()
						join p in employeeContext.Set<Employee>() on b.ID_employeeID equals p.ID
						select new TaskEmployee
						{
							ID = b.ID,
							ProjectName = b.Task,
							StartDate = b.StartDate,
							EndDate = b.EndDate,
							EmployeeName = p.Name,
							Complete = b.Complete
						}; // Select desired properties
			var projects = await query.ToListAsync();
			return View(projects);
		}

		[HttpGet]
		public async Task<IActionResult> Add()
		{
            var employees = await employeeContext.Employees.ToListAsync();
            ViewData["employees"] = employees;
            return View();
		}

		[HttpPost]
		public async Task<IActionResult> Add(AddTask duty)
		{
			var task = new TaskClass()
			{
				ID = Guid.NewGuid(),
				Task = duty.ProjectName,
				StartDate = duty.StartDate,
				EndDate = duty.EndDate,
				Complete = false,
			};
           var employee = await employeeContext.Employees.FindAsync(duty.SelectedEmployeeId );
            if (employee != null)
           {
               task.ID_employeeID = employee.ID; // Set the actual Employee object (optional)
			}

            await employeeContext.Tasks.AddAsync(task);
            await employeeContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }


    }
}
