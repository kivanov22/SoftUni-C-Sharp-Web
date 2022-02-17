namespace CarShop.Controllers
{
    using CarShop.Data;
    using CarShop.ModelViews.Issues;
    using CarShop.Services;
    using MyWebServer.Controllers;
    using MyWebServer.Http;
    using System.Linq;

    public class IssuesController : Controller
    {
        private readonly IUserService users;
        private readonly IValidator validator;
        private readonly ApplicationDbContext data;

        public IssuesController(
            IUserService users,
            IValidator validator,
            ApplicationDbContext data)
        {
            this.data = data;
            this.users = users;
            this.validator = validator;
        }
        //All Issues for a car
        //Add Issues
        //Fix Issue
        //Delete Issue

        [Authorize]
        public HttpResponse CarIssues(string carId)
        {
            var issues = this.data
                .Issues
                .AsQueryable();

            if (!this.UserCanAccessCar(carId))
            {
                return Unauthorized();
            }

            var carIssues = this.data
                .Cars
                .Where(c => c.Id == carId)
                .Select(x => new CarIssuesViewModel
                {
                    Id = x.Id,
                    Model = x.Model,
                    Year = x.Year,
                    UserIsMechanic = this.users.IsMechanic(this.User.Id),
                    Issues = x.Issues.Select(i=> new IssuesListingViewModel
                    {
                        Id= i.Id,
                        Description=i.Description,
                        IsFixed=i.IsFixed,
                        IsFixedInformation=i.IsFixed ? "Yes" : "Not Yet"
                    })
                })
                .FirstOrDefault();

            if (carIssues == null)
            {
                return NotFound();
            }

            return View(carIssues);
        }

        [Authorize]
        public HttpResponse Add(string carId) => View(new AddIssueViewModel
        {
            CarId = carId
        });



        private bool UserCanAccessCar(string carId)
        {
            var userIsMechanic = this.users.IsMechanic(this.User.Id);

            if (!userIsMechanic)
            {
                var userOwnsCar = this.users.OwnsCar(this.User.Id,carId);

                if (!userOwnsCar)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
