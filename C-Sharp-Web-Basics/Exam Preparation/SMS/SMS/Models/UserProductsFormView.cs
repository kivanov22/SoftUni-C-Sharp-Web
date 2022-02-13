using SMS.Models.Products;
using System.Collections.Generic;

namespace SMS.Models
{
    public class UserProductsFormView
    {
        public string Username { get; set; }

        public List<ProductListViewModel> Products { get; set; } = new List<ProductListViewModel>();
    }
}
