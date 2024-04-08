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

     
    }
}
