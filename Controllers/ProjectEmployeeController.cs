using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myFirstWeb.Data;
using myFirstWeb.Models.Domain;

namespace myFirstWeb.Controllers
{
    public class ProjectEmployeeController : Controller
    {
        private readonly EmployeeContext employeeContext;
        public ProjectEmployeeController(EmployeeContext employeeContext)
        {
            this.employeeContext = employeeContext;
        }
        public async Task<IActionResult> Index()
        {
            var contract = from b in employeeContext.ProjectContracts
                           join p in employeeContext.Developments on b.Developments equals p.ID
                           join t in employeeContext.Employees on b.Employee equals t.ID
                           join a in employeeContext.Clients on p.ClientID equals a.ID
                           select new ProjectContractViewModel
                           { 
                               ID = b.ID,
                               ProjectName = p.DevelopmentName,
                               ProjectManager = p.ProjectManager,
                               StartDate = b.startDate,
                               EndDate = b.endDate,
                               EmployeeName = t.Name,
                               ClientName = a.ClientName,
                               isComplete = b.isComplete,
                           };
            var projectContract = await contract.ToListAsync();
            return View(projectContract);
        }
        [HttpGet]
        public async Task<IActionResult> Add() 
        {
            var employees = await employeeContext.Employees.ToListAsync();
            ViewBag.Employees = employees;
            var projects = await employeeContext.Developments.ToListAsync();
            ViewBag.Projects = projects;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddProjectContracViewModel addProjectEmployee)
        
             {
                 var projectEmployee = new ProjectContract
                 {
                     ID = Guid.NewGuid(),
                     startDate = addProjectEmployee.startDate,
                     endDate = addProjectEmployee.endDate,
                     isComplete = false,
                     Employee = addProjectEmployee.IdEmployee,
                     Developments = addProjectEmployee.IdProject,
                 };

                 await employeeContext.ProjectContracts.AddAsync(projectEmployee);
                 await employeeContext.SaveChangesAsync();
                 return RedirectToAction("index");
             }
        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var projectContracts = await employeeContext.ProjectContracts.FirstOrDefaultAsync(x => x.ID == id);
            if(projectContracts != null)
            {
                var projects = await employeeContext.Developments.ToListAsync();
                ViewBag.Projects = projects;
                var employees = await employeeContext.Employees.ToListAsync();
                ViewBag.Employees = employees;
                var viewModel = new UpdateEmployeeProjectViewModel
                {
                    ID = projectContracts.ID,
                    Developments = projectContracts.Developments,
                    Employee = projectContracts.Employee,
                    startDate = projectContracts.startDate,
                    endDate = projectContracts.endDate,
                    isComplete = projectContracts.isComplete,
                };
                return await Task.Run(() =>  View("View", viewModel));
            }
            return View("index");
        }
        [HttpPost]
        public async Task<IActionResult> View(UpdateEmployeeProjectViewModel viewModel)
        {
            var contract = await employeeContext.ProjectContracts.FindAsync(viewModel.ID);
            if (contract != null)
            {
                contract.Employee = viewModel.Employee;
                contract.Developments = viewModel.Developments;
                contract.startDate = viewModel.startDate;
                contract.endDate = viewModel.endDate;
                contract.isComplete = viewModel.isComplete;

                await employeeContext.SaveChangesAsync();
                return RedirectToAction("index");
            }

            return RedirectToAction("index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var duty = await employeeContext.ProjectContracts.FirstOrDefaultAsync(x => x.ID == id);

            if (duty != null)
            {
                employeeContext.ProjectContracts.Remove(duty);

                await employeeContext.SaveChangesAsync();

                return RedirectToAction("index");
            }
            return RedirectToAction("index");
        }
      
    }
}
