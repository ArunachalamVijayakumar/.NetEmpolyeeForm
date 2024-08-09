using EmployeeDetails.Models;
using EmployeeDetails.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EmployeeDetails.Controllers
{
    public class LoginController : BaseController
    {
        private readonly IUserRepository _userRepository;

        public LoginController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public IActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;

            if (claimUser.Identity.IsAuthenticated)
            {
                return RedirectToAction("Employee", "Employee");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if(_userRepository.ValidateUser(loginModel.Username, loginModel.Password))
            {
                List<Claim> claims = new List<Claim>() { new Claim(ClaimTypes.NameIdentifier,loginModel.Username),
                new Claim("Other properties", "Example Role")};

                ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = loginModel.keepLoggedIn,
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,new ClaimsPrincipal(identity),properties);

                return RedirectToAction("Employee", "Employee");
            };

            ViewData["Validate Message"] = "User not found";
            return View();
        }
    }
}
