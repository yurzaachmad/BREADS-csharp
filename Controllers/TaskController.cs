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
						};
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
               task.ID_employeeID = employee.ID;
			}

            await employeeContext.Tasks.AddAsync(task);
            await employeeContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

		[HttpGet]
		public async Task<IActionResult> View (Guid id)
		{
			var duty = await employeeContext.Tasks.FirstOrDefaultAsync(x => x.ID == id);
			if (duty != null)
			{
                var employees = await employeeContext.Employees.ToListAsync();
                ViewData["employees"] = employees;
				var employee = await employeeContext.Employees.FirstOrDefaultAsync(x => x.ID == duty.ID_employeeID);
				ViewData["employee"] = employee.Name + ", " + employee.Department;
				ViewData["idEmployee"] = employee.ID;
                var viewModel = new UpdateTaskViewModel
				{
					ID = duty.ID,
					TaskName = duty.Task,
					StartDate = duty.StartDate,
					EndDate = duty.EndDate,
					Complete = duty.Complete,
					Employee = duty.ID_employeeID.Value
				};
			return await Task.Run(() => View("View", viewModel));
			}
			  return RedirectToAction("index");
		}

		[HttpPost]
        public async Task<IActionResult> View(UpdateTaskViewModel model)
		{
			var duty = await employeeContext.Tasks.FindAsync(model.ID);
			if(duty != null)
			{
				duty.Task = model.TaskName;
				duty.EndDate = model.EndDate;
				duty.Complete = model.Complete;
				duty.ID_employeeID = model.Employee;
				duty.StartDate = model.StartDate;

            await employeeContext.SaveChangesAsync();
            return RedirectToAction("index");
			}
            return RedirectToAction("index");
		}
		[HttpPost]
		public async Task<IActionResult> Delete(Guid id)
		{
            var duty = await employeeContext.Tasks.FirstOrDefaultAsync(x => x.ID == id);

            if (duty != null)
            {
                employeeContext.Tasks.Remove(duty);
                await employeeContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("index");
        }


    }
}
