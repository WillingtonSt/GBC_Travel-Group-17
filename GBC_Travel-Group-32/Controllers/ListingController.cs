using Microsoft.AspNetCore.Mvc;
using GBC_Travel_Group_32.Models;
using GBC_Travel_Group_32.Data;
using Microsoft.EntityFrameworkCore;

namespace GBC_Travel_Group_32.Controllers {
    public class ListingController : Controller {

        private readonly ApplicationDBContext _context;

        public ListingController(ApplicationDBContext context) {
            _context = context;
        }


        [HttpGet]
        public IActionResult Index() {

            var listings = _context.Listings.ToList();


            return View(listings);
        }

        [HttpGet]
        public IActionResult Details(int id) {

            var listing = _context.Listings.Find(id);

            if(listing == null) {
                return NotFound();
            }

            
            return View(listing); 
        }


        [HttpGet]
        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Listing listing) {

            if(ModelState.IsValid) {
                _context.Listings.Add(listing);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }



            return View(listing);
        }



        [HttpGet]
        public IActionResult Edit(int id) {

            var listing = _context.Listings.Find(id);

            if(listing == null) {
                return NotFound();
            }

            return View(listing);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Listing listing) {

            if (ModelState.IsValid) {
                _context.Entry(listing).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }


            return View(listing);
        }


        [HttpGet]
        public IActionResult Delete(int id) {

            var listing = _context.Listings.FirstOrDefault(p => p.ListingId == id);
            if(listing == null) {
                return NotFound();
            }

            return View(listing);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id) {


            var listing = _context.Listings.Find(id);
            if(listing == null) {

                _context.Listings.Remove(listing);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }


            return NotFound();
        }





    }
}
