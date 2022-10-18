using FlightPlanner.Core.Services;
using FlightPlanner.Data;
using FlightPlannerServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Route("testing-api")]
    [ApiController]
    public class TestingApiController : ControllerBase
    {
        private readonly IFlightService _flightService;

        public TestingApiController(IFlightService flightService)
        {
            _flightService = flightService;
        }

        [HttpPost]
        [Route("clear")]
        public IActionResult Clear()
        {
            _flightService.ClearAll();
            return Ok();
        }
    }
}

