using EmployeeDetails.Models;
using EmployeeDetails.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;

namespace EmployeeDetails.Controllers
{
    [Authorize]
    public class EmployeeController : BaseController
    {
        private readonly IEmpolyeeRepository _repository;

        public EmployeeController(IEmpolyeeRepository repository) : base(employeeRepository:repository) 
        {
            _repository = repository;
        }

        public ActionResult Create(EmployeeModel model)
        {
            if (ModelState.IsValid)
            {
               _repository.AddEmpolyee(model);
                // Redirect to the Employee action to show the updated list
                var employees = _repository.GetAll();
                return View("Employee", employees);
            }
            // If model state is not valid, return to the same view with the model data
            return View("Employee", model);
        }

        [HttpGet]
        public IActionResult Employee()
        {
            if (IsUserAuthenticated())
            {
                var employees = _repository.GetAll();
                return View("Employee", employees);
            }

            return RedirectToAction("Login", "Login");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            _repository.deleteEmpolyee(id);
            var employees = _repository.GetAll();
            return View("Employee", employees);
        }

        public IActionResult Export()
        {
            var employees = _repository.GetAll();

            if (!employees.Any())
            {
                TempData["NoDataMessage"] = "No employee data available to export.";
                return View("Employee",employees); 
            }
            else
            {
                var csvBuilder = new StringBuilder();
                csvBuilder.AppendLine("EmployeeID,FirstName,LastName,Email,Address,City,Designation,ProjectName,ProjectManagerName");

                foreach (var employee in employees)
                {
                    csvBuilder.AppendLine($"{employee.EmployeeID},{employee.FirstName},{employee.LastName},{employee.email},{employee.address},{employee.city},{employee.designation},{employee.Project?.ProjectName},{employee.Project?.ProjectManagerName}");
                }

                var csvBytes = Encoding.UTF8.GetBytes(csvBuilder.ToString());
                var result = new FileContentResult(csvBytes, "text/csv")
                {
                    FileDownloadName = "employees.csv",
                };

                return result;
            }
        }

        public async Task<IActionResult> Logout()
        {
            HttpContext.Response.Cookies.Delete("JwtToken");
            return RedirectToAction("Login", "Login");
        }
    }
}
