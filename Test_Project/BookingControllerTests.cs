using GBC_Travel_Group_32.Controllers;
using GBC_Travel_Group_32.Data;
using GBC_Travel_Group_32.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Project {
    public class BookingControllerTests {

        private readonly TestDbContext _context;
        private readonly BookingController _controller;


        public BookingControllerTests() {

            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: "Test Database")
                .Options;


            _context = new TestDbContext(options);

            _controller = new BookingController(_context, null);
        }


        [Fact]
        public async Task Index_ReturnsViewWithBookings() {

            var booking = new Booking { ListingId = 1, Email = "test@gmail.com", BookingDate = DateTime.Now };
            await _context.Bookings.AddAsync(booking);
            await _context.SaveChangesAsync();


            var result = _controller.Index(1);


            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Booking>>(viewResult.Model);
            Assert.Single(model);


        }


        [Fact]
        public async Task UserBookings_ReturnsViewWithBookings() {
           
            var userId = "user1";
            var booking = new Booking { UserId = userId, Email = "test@gmail.com", BookingDate = DateTime.Now }; 
            await _context.Bookings.AddAsync(booking);
            await _context.SaveChangesAsync();

         
            var result = _controller.UserBookings(userId);

        
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Booking>>(viewResult.Model);
            Assert.Single(model); 
        }


    }
}
