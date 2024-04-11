using GBC_Travel_Group_32.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Context;
using System.Diagnostics;

namespace GBC_Travel_Group_32.Controllers {
    public class HomeController : Controller {
      

        public HomeController() {
           
        }

        public IActionResult Index() {

           
            return View();
        }

        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



    }

   
}
