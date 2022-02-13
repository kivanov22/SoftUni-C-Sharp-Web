using MyWebServer.Controllers;
using MyWebServer.Http;
using SMS.Data;
using SMS.Data.Models;
using SMS.Models.Products;
using SMS.Services;
using System.Linq;

namespace SMS.Controllers
{
    using static Data.DataConstants;
    public class ProductsController:Controller
    {
        private readonly IValidator validator;
        private readonly IPasswordHasher passwordHasher;
        private readonly SMSDbContext data;

        public ProductsController(
            IValidator validator,
             IPasswordHasher passwordHasher,
             SMSDbContext data)
        {
            this.validator = validator;
            this.data = data;
            this.passwordHasher = passwordHasher;
        }

       

        [Authorize]
        public HttpResponse Create() => View();

        [Authorize]
        [HttpPost]
        public HttpResponse Create(CreateProductFormModel model)
        {
            var modelErrors = this.validator.ValidateProducts(model);

            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }

            var product = new Product()
            {
                Name = model.Name,
                Price = model.Price
            };

            data.Products.Add(product);
            data.SaveChanges();

            return Redirect("/");
        }
    }
}
