using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using myFirstWeb.Data;
using myFirstWeb.Models;
using Newtonsoft.Json;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace myFirstWeb.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly EmployeeContext employeeContext;

        public EmployeesController(EmployeeContext employeeContext)
        {
            this.employeeContext = employeeContext;
        }

        public ActionResult Login()
        {
            TempData["msg"] = "<div class=\"float-end mt-2 mb-2\"><p>Login</p></div>";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
             //   using (DB_Entities db = new DB_Entities())
                {
                    var obj = employeeContext.Users.Where(a => a.Email.Equals(user.Email) && a.Password.Equals(user.Password)).FirstOrDefault();
                    if (obj != null)
                    {
                        HttpContext.Session.SetString("UserID", obj.ID.ToString());
                        HttpContext.Session.SetString("UserName", obj.Email.ToString());
                       // Session["UserID"] = obj.ID.ToString();
                      //  Session["UserName"] = obj.Email.ToString();
                        return RedirectToAction("Index");
                    }
                }
            }

            TempData["msg"] = "<div class=\"float-end mt-2 mb-2\"><p>Login</p></div>";
            TempData["alert"] = "<div class=\"alert alert-danger text-center\" role=\"alert\">\r\n  Email or Password is wrong!\r\n</div>";
        //    TempData["msg"] = "You are not authorized.";
      
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Hapus semua data session
            return RedirectToAction("Login"); // Arahkan ke halaman Home/Index setelah logout
        }

        [HttpGet]
        public async Task <IActionResult> Index(string department, string name, string email, long? salary, DateTime? DateOfBirth, string sortBy, string sortOrder  )
        {
            if (HttpContext.Session.GetString("UserID") != null)
            {
                var query = employeeContext.Employees.AsQueryable();

                if (!string.IsNullOrEmpty(department))
                {
                    query = query.Where(e => e.Department.ToLower().Contains(department.ToLower()));
                }

                if (!string.IsNullOrEmpty(name))
                {
                    query = query.Where(e => e.Name.ToLower().Contains(name.ToLower()));
                }

                if (!string.IsNullOrEmpty(email))
                {
                    query = query.Where(e => e.Email.ToLower().Contains(email.ToLower()));
                }

                if (salary.HasValue && salary.Value > 0)
                {
                    query = query.Where(e => e.Salary == salary.Value);
                }

                if (DateOfBirth.HasValue && DateOfBirth.Value > DateTime.MinValue)
                {
                    query = query.Where(e => e.DateOfBirth == DateOfBirth.Value);
                }

                if (!string.IsNullOrEmpty(sortBy))
                {
                    switch (sortBy.ToLower())
                    {
                        case "name":
                            query = sortOrder.ToLower() == "desc"
                                ? query.OrderByDescending(e => e.Name)
                                : query.OrderBy(e => e.Name);
                            break;
                        case "department":
                            query = sortOrder.ToLower() == "desc"
                                ? query.OrderByDescending(e => e.Department)
                                : query.OrderBy(e => e.Department);
                            break;
                        case "salary":
                            query = sortOrder.ToLower() == "desc"
                                ? query.OrderByDescending(e => e.Salary)
                                : query.OrderBy(e => e.Salary);
                            break;
                        case "dateofbirth":
                            query = sortOrder.ToLower() == "desc"
                                ? query.OrderByDescending(e => e.DateOfBirth)
                                : query.OrderBy(e => e.DateOfBirth);
                            break;
                        default:
                            break;
                    }
                }

                var employees = await query.ToListAsync();

                var welcome = HttpContext.Session.GetString("UserName");

                string jsonString = JsonConvert.SerializeObject(welcome);

                jsonString = jsonString.Replace("\"", "");

                ViewBag.Message = jsonString;
                //	var employees = await employeeContext.Employees.ToListAsync();
                TempData["msg"] = "<a href=\"Employees/Logout\"><button class=\"btn btn-danger float-end mt-2 mb-2\">Logout</button></a>";
				//	return Content(jsonString);
				return View(employees);
            }
            else
            {
                return RedirectToAction("Login");
            }
           
		}

        [HttpPost]
        public IActionResult Index(SearchCriteria criteria)
        {
         return RedirectToAction("Index", criteria);
        }

        [HttpGet]
        public IActionResult Add()
        {
			if (HttpContext.Session.GetString("UserID") != null)
			{
				var welcome = HttpContext.Session.GetString("UserName");

				string jsonString = JsonConvert.SerializeObject(welcome);

				jsonString = jsonString.Replace("\"", "");

				ViewBag.Message = jsonString;
				//	var employees = await employeeContext.Employees.ToListAsync();
				TempData["msg"] = "<a href=\"/Employees/Logout\"><button class=\"btn btn-danger float-end mt-2 mb-2\">Logout</button></a>";

				return View();
			}
			else
			{
				return RedirectToAction("Login");
			}
		}

        [HttpPost]
        public async Task <IActionResult> Add(AddEmployeeViewModel addEmployeeRequest)
        {
            var employee = new Employee()
            {
                ID = Guid.NewGuid(),
                Name = addEmployeeRequest.Name,
                Email = addEmployeeRequest.Email,
                Salary = addEmployeeRequest.Salary,
                Department = addEmployeeRequest.Department,
                DateOfBirth = addEmployeeRequest.DateOfBirth,
            };


            await employeeContext.Employees.AddAsync(employee);
            await employeeContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
			if (HttpContext.Session.GetString("UserID") != null)
			{
				var employee =  await employeeContext.Employees.FirstOrDefaultAsync(x => x.ID == id);

            if (employee != null) {

                var viewModel = new UpdateEmployeeViewModel()
                {
                    ID = employee.ID,
                    Name = employee.Name,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    Department = employee.Department,
                    DateOfBirth = employee.DateOfBirth,
                };
					var welcome = HttpContext.Session.GetString("UserName");

					string jsonString = JsonConvert.SerializeObject(welcome);

					jsonString = jsonString.Replace("\"", "");

					ViewBag.Message = jsonString;
					//	var employees = await employeeContext.Employees.ToListAsync();
					TempData["msg"] = "<a href=\"../Logout\"><button class=\"btn btn-danger float-end mt-2 mb-2\">Logout</button></a>";

					return await Task.Run(() => View("View", viewModel));
            }

            return RedirectToAction("index");
			}
			else
			{
				return RedirectToAction("Login");
			}
		}

        [HttpPost]
        public async Task<IActionResult> View(UpdateEmployeeViewModel model)
        {
            var employee = await employeeContext.Employees.FindAsync(model.ID);

            if (employee != null)
            {
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Salary = model.Salary;
                employee.Department = model.Department;
                employee.DateOfBirth = model.DateOfBirth;

               await employeeContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
			//  var employee = await employeeContext.Employees.FindAsync(id);

			var employee = await employeeContext.Employees.FirstOrDefaultAsync(x => x.ID == id);

			if (employee != null)
            {
                employeeContext.Employees.Remove(employee);
                await employeeContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("index");
        }

	}

}
