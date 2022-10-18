using FlightPlanner.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlightPlanner.Controllers
{
    [Route("api")]
    [ApiController]

    public class CustomerApiController : ControllerBase
    {
        private readonly FlightPlannerDbContext _context;

        public CustomerApiController(FlightPlannerDbContext context)
        {
            _context = context;
        }

        [Route("airports")]
        [HttpGet]
        public IActionResult SearchAirports(string search)
        {
            search = search.ToLower().Trim();

            var airports = _context.Airports
                .Where(a => a.AirportName.ToLower().Trim().Contains(search)
                || a.Country.ToLower().Trim().Contains(search)
                || a.City.ToLower().Trim().Contains(search)).ToArray();
            return Ok(airports);
        }

        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult SearchFlightsID(int id)
        {
            var flight = _context.Flights
                .Include(f => f.From)
                .Include(f => f.To)
                .SingleOrDefault(f => f.Id == id);

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

            var flights = _context.Flights.Where(f => f.From.AirportName == request.From
            || f.To.AirportName == request.To
            || f.DepartureTime == request.DepartureDate).ToList();

            var result = new PageResult(flights);
            return Ok(result);
        }
    }
}
