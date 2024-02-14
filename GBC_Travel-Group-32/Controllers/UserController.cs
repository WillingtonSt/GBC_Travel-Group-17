using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using GBC_Travel_Group_32.Models;

namespace GBC_Travel_Group_32.Controllers {
    public class UserController : Controller {


        private readonly UserManager<User> _userManager;

        public UserController(UserManager<User> userManager) {
            _userManager = userManager;
        }

        public IActionResult Index() {
            return View();
        }
    }
}
