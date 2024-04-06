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
            var query = from b in employeeContext.Set<Development>()
                        join p in employeeContext.Set<Client>() on b.ClientID equals p.ID
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
    }
}
