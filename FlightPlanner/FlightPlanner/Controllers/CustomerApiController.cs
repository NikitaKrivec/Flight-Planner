using FlightPlanner.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;


namespace FlightPlanner.Controllers
{
    [Route("api")]
    [ApiController]

    public class CustomerApiController : ControllerBase
    {   
        [Route("airports")]
        [HttpGet]
        public IActionResult SearchAirports(string search)
        {
            var airports = FlightStorage.FindAirport(search);
            return Ok(airports);
        }

        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult SearchFlightsID(int id)
        {
            var flight = FlightStorage.GetFlight(id);
            if (flight == null)
            {
                return NotFound();
            }
            return Ok(flight);
        }

        [Route("flights/search")]
        [HttpPost]
        public IActionResult InvalidFlightRequest(SearchFlightRequest request)
        {
            if (!FlightStorage.IsValidFlight(request))
            {
                return BadRequest();
            }
            return Ok(FlightStorage.SearchFlights(request));
        }
    }
}
