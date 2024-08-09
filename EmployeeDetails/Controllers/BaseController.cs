using EmployeeDetails.Repository;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using System.Security.Claims;

namespace EmployeeDetails.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IEmpolyeeRepository _employeeRepository;

        // Constructor for both repositories
        public BaseController(IEmpolyeeRepository employeeRepository = null)
        {
            _employeeRepository = employeeRepository;
        }

        protected bool IsUserAuthenticated()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            return claimUser.Identity.IsAuthenticated;
        }

        protected ActionResult RedirectToEmployeeListIfAuthenticated()
        {
            if (IsUserAuthenticated() && _employeeRepository != null)
            {
                var employees = _employeeRepository.GetAll();
                return View("Employee", employees);
            }

            return RedirectToAction("Login", "Login");
        }
    }
}
