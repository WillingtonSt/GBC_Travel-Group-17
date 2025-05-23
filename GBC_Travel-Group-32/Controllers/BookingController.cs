﻿using GBC_Travel_Group_32.Data;
using GBC_Travel_Group_32.Logging;
using GBC_Travel_Group_32.Migrations;
using GBC_Travel_Group_32.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Listing = GBC_Travel_Group_32.Models.Listing;
using User = GBC_Travel_Group_32.Models.User;


namespace GBC_Travel_Group_32.Controllers {

    [ServiceFilter(typeof(LoggingFilter))]
    [Route("[controller]/[action]")]
    public class BookingController : Controller {

        private readonly ApplicationDBContext _context;

        private readonly UserManager<User> _userManager;

        public BookingController(ApplicationDBContext context, UserManager<User> userManager) {
            _context = context;
            _userManager = userManager;
        }


        [HttpGet("Index/{listingId}")]
        public async Task<IActionResult> Index(int listingId) {


            var bookings = await _context.Bookings.Where(b => b.ListingId == listingId).ToListAsync();
           
            foreach (Booking booking in bookings) {
                var listing = await _context.Listings.FindAsync(booking.ListingId);
                if (listing != null) {
                    booking.listing = listing;
                }
            }
    

            return View(bookings);
        }

        [HttpGet("UserBookings")]
        public IActionResult UserBookings(string userId) {


            var bookings = _context.Bookings.Where(b => b.UserId == userId).ToList();

            foreach(var booking in bookings) {

                var listing = _context.Listings.Find(booking.ListingId);

                if (listing != null) {

                    booking.listing = listing;
                }
            }

            return View(bookings);

        }


        [HttpGet("Details/{id}")]
        public IActionResult Details(int id) {

            var booking = _context.Bookings.Find(id);
            
                

            if(booking == null) {
                return NotFound();
            }

            var Listing = _context.Listings.Find(booking.ListingId);

            booking.listing = Listing;

            

            return View(booking);
        }


        [HttpGet("Create")]
        public async Task<IActionResult> Create(int listingId) {

            var listing = await _context.Listings.FindAsync(listingId);
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

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ListingId", "UserId", "Email", "BookingDate", "BookingEndDate")]Booking booking) {


            var flightListing = await _context.Flights.FindAsync(booking.ListingId);
            var hotelListing = await _context.Hotels.FindAsync(booking.ListingId);
            var carListing = await _context.CarRentals.FindAsync(booking.ListingId);
            

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

                

               await _context.Bookings.AddAsync(booking);
               await _context.SaveChangesAsync();
                return View(nameof(Details), booking);
            }

            return View();



        }


        [HttpGet("Delete/{id:int}")]
        public IActionResult Delete(int id) {

            var booking = _context.Bookings
                .FirstOrDefault(b => b.BookingId == id);

            if (booking == null) {
                return NotFound();
            }

            var listing = _context.Listings.Find(booking.ListingId);

            booking.listing = listing;

            return View(booking);

        }

        [HttpPost("DeleteConfirmed"), ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int bookingId) {

            var booking = _context.Bookings.Find(bookingId);

           

            if (booking == null) {

                return NotFound();
            }

            var userId = booking.UserId;

            var flightListing = _context.Flights.Find(booking.ListingId);
            var hotelListing = _context.Hotels.Find(booking.ListingId);
            var carListing = _context.CarRentals.Find(booking.ListingId);

            int numBookings = countBookings(booking.ListingId);

            if (flightListing != null && numBookings < flightListing.MaxPassengers) {

                flightListing.Available = true;
            } else if(hotelListing != null && numBookings < hotelListing.Rooms) {

                hotelListing.Available = true;
            } else if (carListing != null) {
                
                carListing.Available = true;
            }
            
            _context.Bookings.Remove(booking);
            _context.SaveChanges();
            return RedirectToAction("UserBookings", new {userId = userId});

        }




        public int countBookings(int listingId) {

            
            var bookings = _context.Bookings
                .Where(b => b.ListingId == listingId)
                .ToList();
            

           return bookings.Count;

        }



    }
}
