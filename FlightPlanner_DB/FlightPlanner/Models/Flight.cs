using System.Security.Cryptography;

namespace FlightPlanner.Models
{
    public class Flight
    {
        public int Id { get; set; }
        public Airport From { get; set; } //object
        public Airport To { get; set; } //objects
        public string Carrier { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }

        public bool Equals(Flight flight)
        {
            if (flight == null)
            {
                return false;
            }

            var fromFlight = From.Equals(flight.From);
            var toFlight = To.Equals(flight.To);
            var carrier = Carrier == flight.Carrier;
            var departuteTime = DepartureTime == flight.DepartureTime;
            var arrivalTime = ArrivalTime == flight.ArrivalTime;
            return fromFlight && toFlight && carrier && departuteTime && arrivalTime;
        }
    }
}
