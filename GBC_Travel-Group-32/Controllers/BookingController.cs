using GBC_Travel_Group_32.Data;
using GBC_Travel_Group_32.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace GBC_Travel_Group_32.Controllers {
    public class BookingController : Controller {

        private readonly ApplicationDBContext _context;

        private readonly UserManager<User> _userManager;

        public BookingController(ApplicationDBContext context, UserManager<User> userManager) {
            _context = context;
            _userManager = userManager;
        }


        [HttpGet]
        public IActionResult Index(int listingId) {


            var bookings = _context.Bookings.ToList();
           

            ViewBag.ListingId = listingId;

            return View(bookings);
        }


        [HttpGet]
        public IActionResult Details(int id) {

            var booking = _context.Bookings.Find(id);
            
                

            if(booking == null) {
                return NotFound();
            }

            var Listing = _context.Listings.Find(booking.ListingId);

            booking.listing = Listing;

            

            return View(booking);
        }


        [HttpGet]
        public IActionResult Create(int listingId) {

            var listing = _context.Listings.Find(listingId);
            if(listing == null) {
                return NotFound();
            }

            if(!listing.Available) {
                return RedirectToAction("Index");
            }

            var booking = new Booking {
                ListingId = listingId,
                listing = listing
            };



  
            return View(booking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ListingId", "UserId", "Email", "BookingDate", "BookingEndDate")]Booking booking) {


            var flightListing = _context.Flights.Find(booking.ListingId);
            var hotelListing = _context.Hotels.Find(booking.ListingId);
            var carListing = _context.CarRentals.Find(booking.ListingId);
            

            if(ModelState.IsValid) {

                int numBookings = countBookings(booking.ListingId);


                if (flightListing != null) {

                    if (flightListing.MaxPassengers < numBookings) {
                        flightListing.Available = false;
                        return View();
                    } else if (flightListing.MaxPassengers == numBookings) {
                        flightListing.Available = false;
                    }

                    booking.listing = flightListing;
                    
                }

                if (hotelListing != null) { 
                    if (hotelListing.Rooms < numBookings) {
                        hotelListing.Available = false;
                        return View();
                    } else if (hotelListing.Rooms == numBookings) {
                        hotelListing.Available = false;
                    }

                    booking.listing = hotelListing;
                }

                if (carListing != null) { 
                    carListing.Available = false;
                    booking.listing = carListing;
                }

                

                _context.Bookings.Add(booking);
                _context.SaveChanges();
                return View(nameof(Details), booking);
            }

            return View();



        }


        private int countBookings(int listingId) {

            
            var bookings = _context.Bookings
                .Where(b => b.ListingId == listingId)
                .ToList();
            

           return bookings.Count;

        }



    }
}
