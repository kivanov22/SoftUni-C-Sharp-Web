namespace SMS.Controllers
{
    using Microsoft.EntityFrameworkCore;
    using MyWebServer.Controllers;
    using MyWebServer.Http;
    using SMS.Data;
    using SMS.Models.Carts;
    using SMS.Services;
    using System.Linq;
    using static Data.DataConstants;
    public class CartsController:Controller
    {

        private readonly IValidator validator;
        private readonly IPasswordHasher passwordHasher;
        private readonly SMSDbContext data;

        public CartsController(
             IValidator validator,
             IPasswordHasher passwordHasher,
             SMSDbContext data)
        {
            this.validator = validator;
            this.data = data;
            this.passwordHasher = passwordHasher;
        }

        [Authorize]
        public HttpResponse Details()
        {
            var user = data.Users
                .Where(u => u.Id == User.Id)
                .FirstOrDefault();


            var cartDetails = this.data
                .Products
                .Where(i => i.CartId == user.CartId)
                .Select(c => new CartDetailsViewModel
                {
                    Name = c.Name,
                    Price = c.Price
                })
                .ToList();

            if (cartDetails == null)
            {
                return NotFound();
            }

            return View(cartDetails);
        }

        public HttpResponse AddProduct(string productId)
        {
            var product = data.Products
                .Where(p => p.Id == productId)
                .FirstOrDefault();

            var cart = data.Carts.Where(x => x.UserId == User.Id)//null
                .FirstOrDefault();

            product.CartId = cart.Id;

            data.SaveChanges();

            return Redirect("/Carts/Details");
        }

        public HttpResponse Buy()
        {
            var cart = data.Carts.Where(c => c.UserId == User.Id).FirstOrDefault();

            var products = data.Products.Where(p => p.CartId == cart.Id).ToList();

            if (products.Count == 0)
            {
                return NotFound();
            }

            var user = data.Users
                .Where(c => c.Id == User.Id)
                .Include(u => u.Cart)
                .ThenInclude(p => p.Products)
                .FirstOrDefault();

            user.Cart.Products.Clear();

            //foreach (var item in products)
            //{
                
            //    item.CartId = null;
            //}
            data.SaveChanges();

            return Redirect("/");
        }
    }
}
