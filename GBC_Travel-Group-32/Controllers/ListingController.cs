using Microsoft.AspNetCore.Mvc;
using GBC_Travel_Group_32.Models;
using GBC_Travel_Group_32.Data;
using Microsoft.AspNetCore.Hosting;




namespace GBC_Travel_Group_32.Controllers {
    public class ListingController : Controller {

        private readonly ApplicationDBContext _context;

        private readonly IWebHostEnvironment _environment;

        public ListingController(ApplicationDBContext context, IWebHostEnvironment hostEnvironment) {
            _context = context;
            _environment = hostEnvironment;
        }

        


      
        [HttpGet]
        public IActionResult Index() {

            var listings = _context.Listings.ToList();


            return View(listings);
        }

        [HttpGet]
        public IActionResult Details(int id) {

            if (_context.Flights.Find(id) != null) {
            
                var flight = (from listing in _context.Listings
                              join f in _context.Flights on listing.ListingId equals f.ListingId
                              where f.ListingId == id
                              select f).FirstOrDefault();
                return View(flight);

            } else if (_context.Hotels.Find(id) != null) {

                var hotel = (from listing in _context.Listings
                             join h in _context.Hotels on listing.ListingId equals h.ListingId
                             where h.ListingId == id
                             select h).FirstOrDefault();
                return View(hotel);

            } else if (_context.CarRentals.Find(id) != null) {

                var car = (from listing in _context.Listings
                           join c in _context.CarRentals on listing.ListingId equals c.ListingId
                           where c.ListingId == id
                           select c).FirstOrDefault();
                return View(car);

            } else {
                return NotFound();            }
        }


        [HttpGet]
        public IActionResult Create() {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Listing listing, string listingType, IFormFile image, DateTime flightDate, int maxPassengers, string location, string destination, DateTime startPeriod, DateTime endPeriod, int rooms, string manufacturer, string model) {

            

            if (listing.Image != null && listing.Image.Length > 0) {

                string uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + listing.Image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create)) {

                    await listing.Image.CopyToAsync(fileStream);
                }

                listing.ImageUrl = uniqueFileName;
            }

            

            if (listingType == "Flight") {

                

                return CreateFlight(listing, flightDate, maxPassengers, location, destination);
            } else if (listingType == "Hotel") {
               
                return CreateHotel(listing,startPeriod,endPeriod, rooms);
            } else if (listingType == "CarRental") {

                return CreateCarRental(listing, manufacturer, model);
            }

            return View(nameof(Create));
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateFlight(Listing listing, DateTime flightDate, int maxPassengers, string location, string destination) {

            var flight = new Flight {

                Name = listing.Name,
                Description = listing.Description,
                Price = listing.Price,
                ImageUrl = listing.ImageUrl,
                UserId = listing.UserId,
                Available = true,
                FlightDate = flightDate,
                MaxPassengers = maxPassengers,
                Location = location,
                Destination = destination


            };


            ModelState.Clear();
            TryValidateModel(flight);

            if(ModelState.IsValid) {
                _context.Listings.Add(flight);
                _context.Flights.Add(flight);
                _context.SaveChanges();
                return View(nameof(Details), flight);
            }



            return View(nameof(Create));
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateHotel(Listing listing, DateTime startPeriod, DateTime endPeriod, int rooms) {

            var hotel = new Hotel {

                Name = listing.Name,
                Description = listing.Description,
                Price = listing.Price,
                ImageUrl = listing.ImageUrl,
                UserId = listing.UserId,
                Available = true,
                StartPeriod = startPeriod,
                EndPeriod = endPeriod,
                Rooms = rooms

            };


            ModelState.Clear();
            TryValidateModel(hotel);

            if (ModelState.IsValid) {
                _context.Listings.Add(hotel);
                _context.Hotels.Add(hotel);
                _context.SaveChanges();
                return View(nameof(Details), hotel);
            }



            return View(nameof(Create));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCarRental(Listing listing, string manufacturer, string model) {

            var carRental = new CarRental {

                Name = listing.Name,
                Description = listing.Description,
                Price = listing.Price,
                ImageUrl = listing.ImageUrl,
                UserId = listing.UserId,
                Available = true,
                Manufacturer = manufacturer,
                Model = model

            };


            ModelState.Clear();
            TryValidateModel(carRental);

            if (ModelState.IsValid) {
                _context.Listings.Add(carRental);
                _context.CarRentals.Add(carRental);
                _context.SaveChanges();
                return View(nameof(Details), carRental);
            }



            return View(nameof(Create));
        }


        [HttpGet]
        public IActionResult Edit(int id) {

            if (_context.Flights.Find(id) != null) {

                var flight = (from listing in _context.Listings
                              join f in _context.Flights on listing.ListingId equals f.ListingId
                              where f.ListingId == id
                              select f).FirstOrDefault();
                return View("EditFlight", flight);

            } else if (_context.Hotels.Find(id) != null) {

                var hotel = (from listing in _context.Listings
                             join h in _context.Hotels on listing.ListingId equals h.ListingId
                             where h.ListingId == id
                             select h).FirstOrDefault();
                return View("EditHotel", hotel);

            } else if (_context.CarRentals.Find(id) != null) {

                var car = (from listing in _context.Listings
                           join c in _context.CarRentals on listing.ListingId equals c.ListingId
                           where c.ListingId == id
                           select c).FirstOrDefault();
                return View("EditCarRental", car);

            } else {
                return NotFound();
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditFlight(Flight flight) {

           
            if (!ModelState.IsValid) {
               
                return View(flight);

            }


            int numBookings = countBookings(flight.ListingId);

            if (numBookings >= flight.MaxPassengers) {
                flight.Available = false;
            } else if (numBookings < flight.MaxPassengers) {
                flight.Available = true;
            }

            _context.Listings.Update(flight);
            _context.Flights.Update(flight);
            _context.SaveChanges();
            return View(nameof(Details), flight);
           

        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditHotel(Hotel hotel) {


            if (ModelState.IsValid) {


                int numBookings = countBookings(hotel.ListingId);

                if (numBookings >= hotel.Rooms) {
                    hotel.Available = false;
                } else if (numBookings < hotel.Rooms) {
                    hotel.Available = true;
                }

                _context.Listings.Update(hotel);
                _context.Hotels.Update(hotel);
                _context.SaveChanges();
                return View(nameof(Details), hotel);

            }
            return View(hotel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditCarRental(CarRental car) {

           

            if (ModelState.IsValid) {

                _context.Listings.Update(car);
                _context.CarRentals.Update(car);
                _context.SaveChanges();
                return View(nameof(Details), car);

            }
            return View(car);

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
            var flightListing = _context.Flights.Find(id);
            var hotelListing = _context.Hotels.Find(id);
            var carListing = _context.CarRentals.Find(id);
            if(listing != null && listing.ImageUrl != null) {


                string uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
                string filePath = Path.Combine(uploadsFolder, listing.ImageUrl);

                if (System.IO.File.Exists(filePath)) {
                    System.IO.File.Delete(filePath);
                }


              _context.Listings.Remove(listing);

                var bookings = _context.Bookings.Where(b => b.ListingId == id);
                _context.Bookings.RemoveRange(bookings);


              


                if(flightListing != null) {
                    _context.Flights.Remove(flightListing);
                } else if(hotelListing != null) {
                    _context.Hotels.Remove(hotelListing);
                } else if (carListing != null) {
                    _context.CarRentals.Remove(carListing);
                }

                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }


            return NotFound();
        }

        public IActionResult Search() {

            var model = new FilterViewModel();

            return View(model);
        }



        [HttpGet]
        public IActionResult SearchResults(string searchTerm, FilterViewModel filters) {


            var listings = from listing in _context.Listings
                           join flight in _context.Flights on listing.ListingId equals flight.ListingId into flightGroup
                           from flight in flightGroup.DefaultIfEmpty()
                           join hotel in _context.Hotels on listing.ListingId equals hotel.ListingId into hotelGroup
                           from hotel in hotelGroup.DefaultIfEmpty()
                           join carRental in _context.CarRentals on listing.ListingId equals carRental.ListingId into carRentalGroup
                           from carRental in carRentalGroup.DefaultIfEmpty()
                           select new {
                               ListingId = listing.ListingId,
                               ListingName = listing.Name,
                               ListingDesc = listing.Description,
                               ListingAvailable = listing.Available,
                               FlightDate = flight.FlightDate,
                               FlightLocation = flight.Location,
                               FlightDestination = flight.Destination,
                               CarModel = carRental.Model,
                               CarManufacturer = carRental.Manufacturer,
                               ListingPrice = listing.Price
                           };


            if (!string.IsNullOrEmpty(searchTerm)) {
                var filteredListings = listings.Where(l => l.ListingName.Contains(searchTerm) || l.ListingDesc.Contains(searchTerm));
            }

            if (filters != null) {

                if (filters.Available) {
                    listings = listings.Where(l => l.ListingAvailable);
                }  
                
                if (filters.Unavailable) {

                    listings = listings.Where(l => !l.ListingAvailable);
                }
                
                if (filters.FlightDate) {
                      
                    listings = listings.Where(l => l.FlightDate >= DateTime.Now);
                        
                }
                
                if(filters.Location) {
                    listings = listings.Where(l => l.FlightLocation.Contains(searchTerm));

                }
                
                if(filters.Destination) {

                    listings = listings.Where(l => l.FlightDestination.Contains(searchTerm));
                }

                if (filters.Model) {

                    listings = listings.Where(l => l.CarModel.Contains(searchTerm));
                }

                if (filters.Manufacturer) {

                    listings = listings.Where(l => l.CarManufacturer.Contains(searchTerm));

                }

                if (filters.MinPrice && searchTerm != null) {

                    float minPrice = float.Parse(searchTerm);
                    listings = listings.Where(l => l.ListingPrice >= minPrice);
                }
                
                if (filters.MaxPrice && searchTerm != null) {

                    float maxPrice = float.Parse(searchTerm); 
                    listings = listings.Where(l => l.ListingPrice <= maxPrice);
                }        

            }

            var uniqueListingIds = listings.Select(item => item.ListingId).Distinct();

            List<Listing> matchingListings = new List<Listing>();

            foreach (var  listingId in uniqueListingIds) {

                var matchingLIsting = _context.Listings.FirstOrDefault(listing => listing.ListingId == listingId);

                if (matchingLIsting != null) {

                    matchingListings.Add(matchingLIsting);
                }

            }
            


            return View("SearchResults", matchingListings);
        }



        public int countBookings(int listingId) {


            var bookings = _context.Bookings
                .Where(b => b.ListingId == listingId)
                .ToList();


            return bookings.Count;

        }


    }


    }

