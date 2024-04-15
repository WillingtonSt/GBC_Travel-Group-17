using GBC_Travel_Group_32.Data;
using GBC_Travel_Group_32.Logging;
using GBC_Travel_Group_32.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;

namespace GBC_Travel_Group_32.Controllers {

    [ServiceFilter(typeof(LoggingFilter))]
    public class ListingReviewController : Controller {


        private readonly ApplicationDBContext _context;


        public ListingReviewController(ApplicationDBContext context) {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetReviews(int listingId) {

            var listings = await _context.ListingReviews
                .Where(c => c.ListingId == listingId)
                .OrderByDescending(c => c.DatePosted)
                .ToListAsync();

            return Json(listings);


        }



        [Authorize(Roles = "Traveler, Admin")]
        [HttpPost]
        public async Task<IActionResult> AddReview([FromBody] ListingReview review) {
            

            if(ModelState.IsValid) {


                review.DatePosted = DateTime.Now;
                _context.ListingReviews.Add(review);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Review added successfully" });

            }

            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            return Json(new {success = false, message = "Invalid review data.", error = errors});


        }



    }

}
