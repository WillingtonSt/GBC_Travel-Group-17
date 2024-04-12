using GBC_Travel_Group_32.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Project {
  public class BookingModelTests {


        [Fact]
        public void Email_Should_Be_Valid_Email_Address() {
      
            var booking = new Booking { Email = "invalidemail" };

          
            Assert.Throws<ValidationException>(() => Validator.ValidateObject(booking, new ValidationContext(booking), true));
        }

        [Fact]
        public void BookingDate_Should_Be_Required() {
           
            var booking = new Booking();

            Assert.Throws<ValidationException>(() => Validator.ValidateObject(booking, new ValidationContext(booking), true));
        }

        [Fact]
        public void BookingEndDate_Can_Be_Null() {
          
            var booking = new Booking { BookingDate = DateTime.Now };

            
            Assert.Equal(default(DateTime), booking.BookingEndDate);
        }

        [Fact]
        public void Listing_Can_Be_Null() {
          
            var booking = new Booking { listing = null };

            Assert.Null(booking.listing);
        }

       
    }
}

