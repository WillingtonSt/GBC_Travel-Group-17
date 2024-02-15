using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using GBC_Travel_Group_32.Models;
using System.Reflection.Metadata.Ecma335;

namespace GBC_Travel_Group_32.Controllers {
    public class UserController : Controller {


        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager) {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login() {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(User user) {

            if (ModelState.IsValid) {

                var result = await _signInManager.PasswordSignInAsync(user.UserName, user.Password, true, lockoutOnFailure: false);
                if (result.Succeeded) {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Invalid login attempt");
            }
            return View(user);
        }


        [HttpGet]
        public IActionResult Register() {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User user) {

            if (ModelState.IsValid) {

                var result = await _userManager.CreateAsync(user, user.Password);

                if (result.Succeeded) {

                    return RedirectToAction(nameof(Login));
                }

                foreach (var error in result.Errors) {
                    ModelState.AddModelError(string.Empty, error.Description);
                }


            }
            return View(user);
        }

        
    }
}
