using GBC_Travel_Group_32.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GBC_Travel_Group_32.Controllers {
    public class ErrorController : Controller {


        [Route("/error/404")]
        public IActionResult PageNotFound() {

          

            Response.Clear();
            Response.StatusCode = StatusCodes.Status404NotFound;

            

            return View();
        }


        [Route("/error/500")]
        public IActionResult InternalServerError() {

            Response.Clear();
            Response.StatusCode = StatusCodes.Status500InternalServerError;

            return View();

        }



        [Route("/error/{code}")]
        public IActionResult Index(int code) {

            

            Response.Clear();
            Response.StatusCode = code;
            ViewBag.Code = code;
            return View();
        }

        [Route("/error/handle-exception")]
        public IActionResult HandleException() {

            return StatusCode(StatusCodes.Status500InternalServerError);
        
        
        }

    }
}
