using System;
using System.Collections.Generic;
using System.Linq;
using FlightPlanner.Models;
using PageResult = FlightPlanner.Models.PageResult;

namespace FlightPlanner
{
    public class FlightStorage
    {
        private static List<Flight> _flights = new List<Flight>();
        private static int _id = 1;

        //public static Airport[] FindAirport(string phrase)
        //{
        //    var airport = new List<Airport>();
        //    var formattedPhrase = phrase.ToLower().Trim();
        //    foreach (var flight in _flights)
        //    {
        //        if (flight.From.City.ToLower().Contains(formattedPhrase) ||
        //           flight.From.Country.ToLower().Contains(formattedPhrase) ||
        //           flight.From.AirportName.ToLower().Contains(formattedPhrase))
        //        {
        //            airport.Add(flight.From);
        //        }
        //        if (flight.To.City.ToLower().Contains(formattedPhrase) ||
        //           flight.To.Country.ToLower().Contains(formattedPhrase) ||
        //            flight.To.AirportName.ToLower().Contains(formattedPhrase))
        //        {
        //            airport.Add(flight.To);
        //        }
        //    }
        //    return airport.ToArray();
        //}

        public static bool ValidFormat(Flight flight)
        {
                if (flight == null)
                {
                    return false;
                }

                if (flight.To == null || flight.From == null)
                {
                    return false;
                }

                if ((string.IsNullOrEmpty(flight.Carrier) || string.IsNullOrEmpty(flight.ArrivalTime) ||
                    string.IsNullOrEmpty(flight.DepartureTime) || flight.To == null || flight.From == null) ||
                        string.IsNullOrEmpty(flight.To.AirportName) || string.IsNullOrEmpty(flight.To.City) ||
                    string.IsNullOrEmpty(flight.To.Country) || string.IsNullOrEmpty(flight.From.AirportName) || string.IsNullOrEmpty(flight.From.City) ||
                    string.IsNullOrEmpty(flight.From.Country))
                {
                    return false;
                }

                var departureTime = DateTime.Parse(flight.DepartureTime);
                var arrivalTime = DateTime.Parse(flight.ArrivalTime);

                if (arrivalTime <= departureTime)
                {
                    return false;
                }

                return true;
        }

        public static bool HasSameAirport(Flight flight)
        {
                if (flight.From.City.ToUpper().Trim() == flight.To.City.ToUpper().Trim() && flight.From.Country.ToUpper().Trim() == flight.To.Country.ToUpper().Trim() &&
                    flight.From.AirportName.ToUpper().Trim() == flight.To.AirportName.ToUpper().Trim())
                {
                    return true;
                }
                return false;
        }

        public static PageResult SearchFlights(SearchFlightRequest request)
        {
                return new PageResult(_flights);
        }

        public static bool IsValidFlight(SearchFlightRequest request)
        {
                if (request.From == request.To)
                {
                    return false;
                }
                if (request.From == null || request.To == null || request.DepartureDate == null)
                {
                    return false;
                }
                return true;
        }
    }
}
