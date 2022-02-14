namespace SMS.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Cart
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        //The INSERT statement conflicted with the FOREIGN KEY constraint "FK_Users_Carts_CartId".
        //The conflict occurred in database "SMS", table "dbo.Carts", column 'Id'.
        public string UserId { get; set; }

        public User User { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
