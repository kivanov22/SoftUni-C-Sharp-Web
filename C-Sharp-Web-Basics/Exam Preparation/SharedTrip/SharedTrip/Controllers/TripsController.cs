
namespace SharedTrip.Controllers
{
    using MyWebServer.Controllers;
    using MyWebServer.Http;
    using SharedTrip.Data;
    using SharedTrip.Data.Models;
    using SharedTrip.Models.Trps;
    using SharedTrip.Services;
    using System.Linq;
    using static Data.DataConstants;

    public class TripsController : Controller
    {
        private readonly IValidator validator;
        private readonly IPasswordHasher passwordHasher;
        private readonly ApplicationDbContext data;

        public TripsController(
            IValidator validator,
            IPasswordHasher passwordHasher,
            ApplicationDbContext data)
        {
            this.validator = validator;
            this.data = data;
            this.passwordHasher = passwordHasher;
        }

        

        [Authorize]
        public HttpResponse All()
        {
            var tripsQuery = this.data
                .Trips
                .AsQueryable();


            var trips = tripsQuery
                .Select(t => new TripListingViewModel
                {
                    Id= t.Id,
                    StartingPoint = t.StartPoint,
                    EndingPoint = t.EndPoint,
                    DepartureTime = t.DepartureTime,
                    Seats = t.Seats,
                    Description = t.Description,
                    Image = t.Image
                })
                .ToList();

            return View(trips);
        }

        [Authorize]
        public HttpResponse Add() => View();


        [Authorize]
        [HttpPost]
        public HttpResponse Add(AddTripFormModel model)
        {
            var modelErrors = this.validator.ValidateTrip(model);

            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }

            var trip = new Trip
            {
                StartPoint = model.StartPoint,
                EndPoint = model.EndPoint,
                DepartureTime = model.DepartureTime,
                Seats = model.Seats,
                Description = model.Description,
                Image = model.ImagePath
            };

            data.Trips.Add(trip);

            data.SaveChanges();

            return Redirect("/Trips/All");
        }

        [Authorize]
        public HttpResponse Details(string tripId)
        {
            
            var tripDetails = this.data
                .Trips
                .Where(i => i.Id == tripId)
                .Select(c => new TripListingViewModel
                {
                    Id = c.Id,
                    StartingPoint = c.StartPoint,
                    EndingPoint = c.EndPoint,
                    DepartureTime = c.DepartureTime,
                    Image = c.Image,
                    Seats = c.Seats,
                    Description = c.Description
                })
                .FirstOrDefault();

            if (tripDetails == null)
            {
                return NotFound();
            }

            return View(tripDetails);
        }
    }
}
