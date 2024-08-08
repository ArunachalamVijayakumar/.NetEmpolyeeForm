using EmployeeDetails.Models;
using EmployeeDetails.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeDetails.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmpolyeeRepository _repository;

        public EmployeeController(IEmpolyeeRepository repository)
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
            var employees = _repository.GetAll();
            return View("Employee", employees);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            _repository.deleteEmpolyee(id);
            var employees = _repository.GetAll();
            return View("Employee", employees); // Redirect to the action that lists the employees
        }

        public IActionResult Logout()
        {
            return RedirectToAction("Login", "Login");
        }
    }
}
