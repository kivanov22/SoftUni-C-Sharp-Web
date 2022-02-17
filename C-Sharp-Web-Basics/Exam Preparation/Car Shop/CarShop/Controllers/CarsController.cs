namespace CarShop.Controllers
{
    using CarShop.Data;
    using CarShop.Data.Models;
    using CarShop.ModelViews.Cars;
    using CarShop.Services;
    using MyWebServer.Controllers;
    using MyWebServer.Http;
    using System.Linq;

    public class CarsController : Controller
    {
        //method for adding and displaying all cars
        private readonly IUserService userService;
        private readonly IValidator validator;
        private readonly ApplicationDbContext data;

        public CarsController(
            IUserService userService,
            IValidator validator,
            ApplicationDbContext data)
        {
            this.data = data;
            this.userService = userService;
            this.validator = validator;
        }

        [Authorize]
        public HttpResponse All()
        {
            var carsQuery = this.data
                .Cars
                .AsQueryable();

            if (this.userService.IsMechanic(this.User.Id))
            {
                //if its mechanic
                carsQuery = carsQuery.Where(c => c.Issues.Any(i => !i.IsFixed));
            }
            else
            {
                //if not mechanic
                carsQuery = carsQuery.Where(c => c.OwnerId == this.User.Id);
            }
            //return query in both cases when mechanic and when not 

            var cars = carsQuery
                .Select(c => new CarListingViewModel
                {
                    Id=c.Id,
                    Model = c.Model,
                    Year = c.Year,
                    PlateNumber = c.PlateNumber,
                    Image = c.PictureUrl,
                    RemainingIssues = c.Issues.Count(i=>!i.IsFixed),
                    FixedIssues = c.Issues.Count(i=>i.IsFixed)
                })
                .ToList();


            return View(cars);
        }

        [Authorize]
        public HttpResponse Add()
        {
            if (this.userService.IsMechanic(this.User.Id))
            {
                return Error("Mechanics cannot add cars.");
            }

            return View();
        }


        [Authorize]
        [HttpPost]
        public HttpResponse Add(AddCarFormModel model)
        {
            var modelErrors = this.validator.ValidateCar(model);

            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }

            var car = new Car
            {
                Model = model.Model,
                Year = model.Year,
                PictureUrl = model.Image,
                PlateNumber = model.PlateNumber,
                OwnerId = this.User.Id
            };

            this.data.Cars.Add(car);
            this.data.SaveChanges();

            return Redirect("/Cars/All");
        }

    }
}
