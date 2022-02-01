namespace SharedTrip.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    //we can use directly constants this way
    using static DataConstants;

    public class Trip
    {
        [Key]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        public string StartPoint { get; set; }

        [Required]
        public string EndPoint { get; set; }

        [Required]
        public DateTime DepartureTime { get; set; }

        [MaxLength(SeatsMaxLength)]
        public int Seats { get; set; }

        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        public string Image { get; set; }

        public IEnumerable<UserTrip> UserTrips { get; init; } = new List<UserTrip>();
    }
}
