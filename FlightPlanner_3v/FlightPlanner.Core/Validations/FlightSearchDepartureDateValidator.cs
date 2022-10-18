using FlightPlanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightPlanner.Core.Validations
{
    public class FlightSearchDepartureDateValidator : ISearchFlightRequest
    {
        public bool IsValid(SearchFlightRequest request)
        {
            return !string.IsNullOrEmpty(request.DepartureDate);
        }
    }
}
