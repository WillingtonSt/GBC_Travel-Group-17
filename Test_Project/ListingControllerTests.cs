using GBC_Travel_Group_32.Controllers;
using GBC_Travel_Group_32.Data;
using GBC_Travel_Group_32.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Project {
    public class ListingControllerTests {


        private readonly ListingController _listingController;
        private readonly TestDbContext _dbContext;
        private readonly FilterViewModel _filterViewModel;

        public ListingControllerTests() {

            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _dbContext = new TestDbContext(options);
            _listingController = new ListingController(_dbContext, null);

            
         

        }


        [Fact]
        public void Index_ReturnsViewWithListings() {




            _dbContext.Listings.AddRange(
                new Listing { ListingId = 6, Name = "Listing 1", Description = "test description", UserId = "1B" },
                new Listing { ListingId = 7, Name = "Listing 2", Description = "test description", UserId = "1B" }

        );
            _dbContext.SaveChanges();


            var result = _listingController.Index() as ViewResult;
            var model = result?.Model as List<Listing>;


            Assert.NotNull(result);
            Assert.NotNull(model);
            Assert.Equal(2, model.Count);
        }




        [Fact]
        public void Details_ReturnsNotFoundForInvalidId() {





            _dbContext.Listings.Add(new Listing { ListingId = 4, Name = "Listing 1", Description = "test description", UserId = "1B" });
            _dbContext.SaveChanges();




            var result = _listingController.Details(2);

            Assert.IsType<NotFoundResult>(result);

        }


        [Fact]
        public void Delete_ReturnsViewTest() {





            _dbContext.Listings.Add(new Listing { ListingId = 1, Name = "Listing 1", Description = "test description", UserId = "1B" });
            _dbContext.SaveChanges();



                var result = _listingController.Delete(1);


                var viewResult = Assert.IsType<ViewResult>(result);
                var model = Assert.IsAssignableFrom<Listing>(viewResult.ViewData.Model);
                Assert.Equal("Listing 1", model.Name);
            
        }



        [Fact]
        public void DeleteConfirmed_Test() {

          

                _dbContext.Listings.Add(new Listing { ListingId = 1, Name = "Listing 1", Description = "test description", UserId = "1B" });
                _dbContext.SaveChanges();

                var result = _listingController.DeleteConfirmed(1);


                var redirectResult = Assert.IsType<RedirectToActionResult>(result);
                Assert.Equal("UserListing", redirectResult.ActionName);
            
           
                var deletedListing = _dbContext.Listings.FirstOrDefault(l => l.ListingId == 1);
                Assert.Null(deletedListing);
            
        }


    }
}
