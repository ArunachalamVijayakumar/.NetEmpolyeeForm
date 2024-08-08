using Microsoft.AspNetCore.Mvc;

namespace EmployeeDetails.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult loggedIn()
        {
          return RedirectToAction("Employee", "Employee");
        }
    }
}
