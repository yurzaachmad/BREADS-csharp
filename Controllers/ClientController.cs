using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myFirstWeb.Data;
using myFirstWeb.Models.Domain;

namespace myFirstWeb.Controllers
{
    public class ClientController : Controller
    {
        private readonly EmployeeContext employeeContext;
        public ClientController(EmployeeContext employeeContext)
        {
            this.employeeContext = employeeContext;
        }
        public async Task<IActionResult> Index()
        {
            var clients = await employeeContext.Clients.ToListAsync();
            return View(clients);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddClientViewModel AddRequest)
        {
            var client = new Client()
            {
                ID = Guid.NewGuid(),
                ClientName = AddRequest.ClientName,
                NumberPhone = AddRequest.NumberPhone,
                Address = AddRequest.Address,
            };
            await employeeContext.Clients.AddAsync(client);
            await employeeContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var client = await employeeContext.Clients.FirstOrDefaultAsync(x => x.ID == id);
            if (client != null)
            {
                var viewModel = new UpdateClientViewModel()
                {
                    ClientName = client.ClientName,
                    NumberPhone = client.NumberPhone,
                    Address = client.Address,
                };

                return await Task.Run(() => View("View", viewModel));
            }
            return RedirectToAction("index");
        }
        [HttpPost]
        public async Task<IActionResult> View(UpdateClientViewModel UpdateRequest)
        {
            var client = await employeeContext.Clients.FirstOrDefaultAsync(x => x.ID == UpdateRequest.ID);
            if (client != null)
            {
                client.ClientName = UpdateRequest.ClientName;
                client.NumberPhone = UpdateRequest.NumberPhone;
                client.Address = UpdateRequest.Address;
				await employeeContext.SaveChangesAsync();
				return RedirectToAction("index");
            }
            return RedirectToAction("index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var client = await employeeContext.Clients.FirstOrDefaultAsync(x => x.ID == id);
            if (client != null)
            {
                employeeContext.Clients.Remove(client);
                await employeeContext.SaveChangesAsync();
            }
            return RedirectToAction("index");
        }
    }
}
