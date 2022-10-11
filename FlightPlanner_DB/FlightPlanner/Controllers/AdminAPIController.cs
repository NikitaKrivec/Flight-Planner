using FlightPlanner.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FlightPlanner.Controllers
{
    [Route("admin-api")]
    [ApiController, Authorize]
    public class AdminApiController : ControllerBase
    {
        private readonly FlightPlannerDbContext _context;
        public AdminApiController(FlightPlannerDbContext context)
        {
            _context = context;
        }

        private static readonly object _locker = new object();
        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult GetFlight(int id)
        {
            lock (_locker)
            {
                var flight = _context.Flights
                    .Include(f => f.From)
                    .Include(f => f.To)
                    .FirstOrDefault(f => f.Id == id);

                if (flight == null)
                {
                    return NotFound();
                }
                return Ok(flight);
            }
        }

        [Route("flights")]
        [HttpPut]
        public IActionResult PutFlight(Flight flight)
        {
            lock (_locker)
            {
                if (flight == null)
                {
                    return NoContent();
                }

                if (!FlightStorage.ValidFormat(flight))
                {
                    return BadRequest();
                }

                if (FlightStorage.HasSameAirport(flight))
                {
                    return BadRequest();
                }

                if (_context.Flights.Any(f => f.From.City == flight.From.City &&
                    f.From.Country == flight.From.Country &&
                    f.From.AirportName == flight.From.AirportName &&
                    f.To.City == flight.To.City &&
                    f.To.Country == flight.To.Country &&
                    f.To.AirportName == flight.To.AirportName &&
                    f.Carrier == flight.Carrier && 
                    f.ArrivalTime == flight.ArrivalTime && 
                    f.DepartureTime == flight.DepartureTime
                    ))
                {
                    return Conflict();
                }

                _context.Flights.Add(flight);
                _context.SaveChanges();
                return Created("", flight);
            }       
        }

        [Route("flights/{id}")]
        [HttpDelete]
        public IActionResult DeleteFlights(int id)
        {
           lock (_locker)
            { 
                var flight = _context.Flights
                .Include(f => f.From)
                .Include(f => f.To)
                .FirstOrDefault(x => x.Id == id);

                if (flight != null)
                { 
                    _context.Flights.Remove(flight);
                    _context.SaveChanges();
                }
                return Ok();
            }
        }
    }
}
