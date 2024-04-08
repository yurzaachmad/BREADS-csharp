using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myFirstWeb.Data;
using myFirstWeb.Models.Domain;

namespace myFirstWeb.Controllers
{
    public class DevelopmentController : Controller
    {
        public readonly EmployeeContext employeeContext;
        public DevelopmentController(EmployeeContext employeeContext)
        {
            this.employeeContext = employeeContext;
        }
        public async Task<IActionResult> Index()
        {
            var query = from b in employeeContext.Developments
                        join p in employeeContext.Clients on b.ClientID equals p.ID
                        select new ProjectClient
                        {
                            ProjectName = b.DevelopmentName,
                            ProjectManager = b.ProjectManager,
                            Client = p.ClientName,
                            DevelopmentID = b.ID,
                        };
            var project = await query.ToListAsync();
            return View(project);
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var data = await employeeContext.Clients.ToListAsync();
            ViewBag.clients = data;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddDevelopmentViewModel RequestAdd)
        {
            var project = new Development()
            {
                ID = Guid.NewGuid(),
                DevelopmentName = RequestAdd.ProjectName,
                ProjectManager = RequestAdd.ProjectManager,
                ClientID = RequestAdd.SelectedID,
            };

            await employeeContext.Developments.AddAsync(project);
            await employeeContext.SaveChangesAsync();
            return RedirectToAction("index");
        }
        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var project = await employeeContext.Developments.FirstOrDefaultAsync(x => x.ID == id);
            if(project != null)
            {
                var clients = await employeeContext.Clients.ToListAsync();
                ViewBag.clients = clients;
                var viewModel = new UpdateDevelopmentViewModel
                {
                    ID = project.ID,
                    DevelopmentName = project.DevelopmentName,
                    ProjectManager = project.ProjectManager,
                    ClientID = project.ClientID.Value
                };
            return await Task.Run(() => View("View", viewModel));
            }
            return RedirectToAction("index");
        }
        [HttpPost]
        public async Task<IActionResult> View(UpdateDevelopmentViewModel RequestUpdate)
        {
            var project = await employeeContext.Developments.FindAsync(RequestUpdate.ID);
            if(project != null)
            {
                project.DevelopmentName = RequestUpdate.DevelopmentName;
                project.ProjectManager  = RequestUpdate.ProjectManager;
                project.ClientID = RequestUpdate.ClientID;

                await employeeContext.SaveChangesAsync();
                return RedirectToAction("index");
            }
            return View("index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var project = await employeeContext.Developments.FindAsync(id);
            if(project != null)
            {
                employeeContext.Developments.Remove(project);
                await employeeContext.SaveChangesAsync();
            }
            return RedirectToAction("index");
        }
    }
}
