using Business.Interfaces.Services;
using Business.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ProductsCrud.Controllers
{
    [AllowAnonymous]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateUser(User user)
        {
            if (ModelState.IsValid)
            {
                _userService.CreateUser(user);
                Auth(user.Username);
                return RedirectToAction("Index", "Product");
            }

            return View(user);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            if (_userService.ValidateUser(user))
            {
                Auth(user.Username);
                return RedirectToAction("Index", "Product");
            }

            ModelState.AddModelError(string.Empty, "Invalid username or password");
            return View(user);
        }

        private void Auth(string username)
        {
            List<Claim> claims =
            [
                new Claim(ClaimTypes.NameIdentifier, username),
                new Claim(ClaimTypes.Name, username)
            ];
            var authScheme = CookieAuthenticationDefaults.AuthenticationScheme;

            var identity = new ClaimsIdentity(claims, authScheme);

            var principal = new ClaimsPrincipal(identity);

             HttpContext.SignInAsync(authScheme, principal,
                new AuthenticationProperties
                {
                });

        }

    }
}
