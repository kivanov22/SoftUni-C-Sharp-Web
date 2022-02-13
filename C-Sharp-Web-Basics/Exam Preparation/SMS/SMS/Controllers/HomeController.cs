namespace SMS.Controllers
{
    using MyWebServer.Controllers;
    using MyWebServer.Http;
    using SMS.Data;
    using SMS.Data.Models;
    using SMS.Models;
    using SMS.Models.Products;
    using System.Linq;
    using static Data.DataConstants;
    public class HomeController : Controller
    {
        private readonly SMSDbContext data;
        public HomeController(SMSDbContext data)
        {
            this.data = data;
        }

        public HttpResponse Index()
        {
            if (!this.User.IsAuthenticated)
            {
                return View("Index");
            }
            else
            {
                var user = data.Users.Where(x=>x.Id == this.User.Id).FirstOrDefault();

                var products = this.data.Products
                    .Select(x => new ProductListViewModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Price = x.Price
                    })
                    .ToList();

                var model = new UserProductsFormView
                {
                    Username = user.Username,
                    Products = products
                };

                return View("IndexLoggedIn", model);
            }
        }
    }
}