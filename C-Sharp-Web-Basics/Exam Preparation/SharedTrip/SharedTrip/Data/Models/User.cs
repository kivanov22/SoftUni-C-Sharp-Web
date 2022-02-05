namespace SharedTrip.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;


    //we can use directly constants this way
    using static DataConstants;
    public class User
    {

        [Key]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        //[MaxLength(UsernameMaxLength)]
        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
       // [MaxLength(PasswordMaxLenght)]
        public string Password { get; set; }

        public IEnumerable<UserTrip> UserTrips { get; init; } = new List<UserTrip>();
    }
}
