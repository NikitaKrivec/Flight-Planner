using FlightPlanner.Core.Models;
using System.Security.Cryptography;

namespace FlightPlanner.Models
{
    public class Flight : Entity
    {
        public Airport From { get; set; } //object
        public Airport To { get; set; } //objects
        public string Carrier { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }

    }
}
