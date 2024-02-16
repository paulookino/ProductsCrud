using Business.Interfaces.Services;
using Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace ProductsCrud.Controllers
{
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
                return RedirectToAction("Index", "Home");
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
                return RedirectToAction("Product", "Index");
            }

            ModelState.AddModelError(string.Empty, "Invalid username or password");
            return View(user);
        }
    }
}
