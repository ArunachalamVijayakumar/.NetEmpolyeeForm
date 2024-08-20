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
    }
}
