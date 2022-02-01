namespace SharedTrip.Data.Models
{
    using System;



    public class UserTrip
    {

        public string UserId { get; init; } = Guid.NewGuid().ToString();

        public User User { get; set; }

        public string TripId { get; init; } = Guid.NewGuid().ToString();

        public Trip Trip { get; set; }
    }
}
