using System;

namespace SharedTrip.Models.Trps
{
    public class TripListingViewModel
    {
        public string Id { get; init; }
        public string StartingPoint { get; init; }

        public string EndingPoint { get; init; }

        public DateTime DepartureTime { get; init; }

        public string Image { get; init; }

        public int Seats { get; init; }

        public string Description { get; init; }
    }
}
