using GBC_Travel_Group_32.Models;
using System.ComponentModel.DataAnnotations;

namespace Test_Project {
    public class ListingModelTests {
        [Fact]
        public void CanChangeListingName() {


            var l = new Listing { Name = "Test", Price = 500, Description = "description", UserId = "5" };

            l.Name = "Edited";


            Assert.Equal("Edited", l.Name);


        }


        [Fact]
        public void Name_Should_Be_Required() {

            var listing = new Listing();


            Assert.Throws<ValidationException>(() => Validator.ValidateObject(listing, new ValidationContext(listing), true));
        }

        [Fact]
        public void Price_Should_Not_Be_Negative() {

            var listing = new Listing { Name = "Test Listing", Price = -100 };

            Assert.Throws<ValidationException>(() => Validator.ValidateObject(listing, new ValidationContext(listing), true));
        }





        [Fact]
        public void FlightDate_Should_Be_Required() {

            var flight = new Flight();




            Assert.Throws<ValidationException>(() => Validator.ValidateObject(flight, new ValidationContext(flight), true));
        }

        [Fact]
        public void MaxPassengers_Should_Be_At_Least_One() {

            var flight = new Flight { FlightDate = DateTime.Now, MaxPassengers = 0 };




            Assert.Throws<ValidationException>(() => Validator.ValidateObject(flight, new ValidationContext(flight), true));
        }

    }
}