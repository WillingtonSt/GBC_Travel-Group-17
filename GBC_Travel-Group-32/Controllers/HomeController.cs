using GBC_Travel_Group_32.Logging;
using GBC_Travel_Group_32.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Context;
using System.Diagnostics;

namespace GBC_Travel_Group_32.Controllers {
   
    [ServiceFilter(typeof(LoggingFilter))]
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

          


            return View(nameof(Error), new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        public IActionResult TestErrorLogging() {

            throw new InvalidOperationException();

        }


        public IActionResult TestGenericError() {

           return StatusCode(400);

        }

        public IActionResult TestErrorRedirect404() {

            return StatusCode(404);

        }

        public IActionResult TestErrorRedirect500() {

            return StatusCode(500);

        }



    }

   
}
