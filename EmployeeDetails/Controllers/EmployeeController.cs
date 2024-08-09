using EmployeeDetails.Models;
using EmployeeDetails.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EmployeeDetails.Controllers
{
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
            return RedirectToEmployeeListIfAuthenticated();
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            _repository.deleteEmpolyee(id);
            var employees = _repository.GetAll();
            return View("Employee", employees);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Login");
        }
    }
}
